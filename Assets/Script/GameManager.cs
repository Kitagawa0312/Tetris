using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の進行
/// </summary>
public class GameManager : MonoBehaviour
{
    #region 変数

    [SerializeField] private BoardView _boardView = default;
    [SerializeField] private TetrominoView _tetrominoView = default;
    [SerializeField] private TetrominoDatabase _database = default;
    [SerializeField] private NextView _nextView = default;
    [SerializeField] private ScoreView _scoreView = default;
    [SerializeField] private TetrominoView _ghostView = default; 
    [SerializeField] private HoldView _holdView = default;
    [SerializeField] private GameOverView _gameOverView = default;

    #region 定数

    private const int ONE_LINE_Delete = 100;
    private const int SECOND_LINE_Delete = 300;
    private const int THIRD_LINE_Delete = 500;
    private const int FOUR_LINE_Delete = 800;


    private const int SOFT_TIME = 50;
    private const int SCORE_DROP_TIME = 50;
    private const int MIN_DROP_TIME = 100;
    private const int MAX_DROP_TIME = 500;
    
    #endregion

    // 現在のレベル
    private int _level = 1;

    // 消したライン数
    private int _totalClearedLines = 0;

    // 現在のスコア
    private int _score = 0;

    // ゲームオーバー判定
    private bool _isGameOver = false;

    // ポーズ判定
    private bool _isPause = false;

    // ホールド判定
    private bool _hasHoldMino = false;
    
    // ホールドできるかどうか
    private bool _canHold = true;
   
    private MinoType _holdMinoType;
    private BoardModel _boardModel = default;
    private BoardPresenter _boardPresenter = default;
    private TetrominoPresenter _tetrominoPresenter = default;
    private TetrominoModel _tetrominoModel = default;

    private readonly Queue<MinoType> _minoQueue = new Queue<MinoType>();

    #endregion

    #region メソッド

    /// <summary>
    /// 初期設定
    /// </summary>
    private void Start()
    {
        _boardModel = new BoardModel();
        _boardPresenter = new BoardPresenter(_boardModel);

        _boardView.Initialize();
        _tetrominoView.Initialize();
        _ghostView.Initialize();
        _nextView.Initialize();
        _holdView.Initialize();
        _gameOverView.Initialize();

        _scoreView.Refresh(_score);

        CreateBag();
        GenerateMino();

        DropLoop().Forget();
    }

    /// <summary>
    /// キー入力
    /// </summary>
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            _isPause = !_isPause;
        }

        if (_isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
            }

            return;
        }


        if (_isGameOver || _isPause)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _tetrominoPresenter.MoveLeft();
            UpdateGhostView();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _tetrominoPresenter.MoveRight();
            UpdateGhostView();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _tetrominoPresenter.RotateRight();
            UpdateGhostView();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            _tetrominoPresenter.RotateLeft();
            UpdateGhostView();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Hold();
        }
    }

    /// <summary>
    /// ミノの生成
    /// </summary>
    public void GenerateMino()
    {
        if (_minoQueue.Count < 7)
        {
            CreateBag();
        }

        MinoType nextType = _minoQueue.Dequeue();

        SpawnMino(nextType);

        UpdateNextView();
        UpdateGhostView();
    }

    /// <summary>
    /// 指定されたミノの生成
    /// </summary>
    private void SpawnMino(MinoType type)
    {
        _tetrominoModel = new TetrominoModel(_database.Get(type),new Vector2Int(4, 21));

        if (!_boardPresenter.CanPlace(_tetrominoModel.CurrentRotation.Cells,_tetrominoModel.Position))
        {
            GameOver();
            return;
        }

        _tetrominoPresenter = new TetrominoPresenter(_tetrominoModel,_tetrominoView,_boardPresenter);

        _tetrominoView.Refresh(_tetrominoModel);

        _canHold = true;
    }

    /// <summary>
    /// ミノのホールド、入れ替え
    /// </summary>
    private void Hold()
    {
        if (!_canHold)
        {
            return;
        }

        MinoType currentType = _tetrominoModel.Data.Type;

        if (!_hasHoldMino)
        {
            _holdMinoType = currentType;
            _hasHoldMino = true;

            GenerateMino();
        }
        else
        {
            MinoType temp = _holdMinoType;
            _holdMinoType = currentType;

            SpawnMino(temp);
        }

        _canHold = false;
        TetrominoModel holdModel = new TetrominoModel(_database.Get(_holdMinoType),new Vector2Int(0, 0));
        _holdView.Refresh(holdModel);

    }

    /// <summary>
    /// ミノの落下
    /// </summary>
    public async UniTask DropLoop()
    {
        while (!_isGameOver)
        {
            if (_isPause)
            {
                await UniTask.Delay(100);
                continue;
            }

            int waitTime = Input.GetKey(KeyCode.DownArrow)? SOFT_TIME : GetDropTime();
            await UniTask.Delay(waitTime);
            bool moved = _tetrominoPresenter.MoveDown();

            if (moved)
            {
                UpdateGhostView();
            }
            else
            {
                FixCurrentMino();
            }
        }
    }

    /// <summary>
    /// ミノの固定、行消去、スコア加算
    /// </summary>
    private void FixCurrentMino()
    {
        if (IsCurrentMinoAboveBoard())
        {
            GameOver();
            return;
        }

        _boardPresenter.Fix(_tetrominoModel.CurrentRotation.Cells,_tetrominoModel.Position);

        _boardView.CreateFixedBlocks(_tetrominoModel.CurrentRotation.Cells,_tetrominoModel.Position,_tetrominoModel.Data.MinoSprite);

        List<int> fullLines = _boardModel.GetFullLines();

        foreach (int line in fullLines)
        {
            _boardModel.DeleteLine(line);
            _boardView.DeleteLine(line);
        }

        AddScore(fullLines.Count);

        GenerateMino();
    }

    /// <summary>
    /// ミノのドロップ
    /// </summary>
    private void HardDrop()
    {
        while (_tetrominoPresenter.MoveDown())
        {
        }

        FixCurrentMino();
    }

    /// <summary>
    /// Nextの更新
    /// </summary>
    private void UpdateNextView()
    {
        MinoType[] nextArray = _minoQueue.ToArray();

        TetrominoModel[] models = new TetrominoModel[6];

        for (int i = 0; i < 6; i++)
        {
            models[i] =
                new TetrominoModel(_database.Get(nextArray[i]),new Vector2Int(0, 0));
        }

        _nextView.Refresh(models);
    }

    /// <summary>
    /// ミノの7Bagの生成
    /// </summary>
    private void CreateBag()
    {
        List<MinoType> bag = new List<MinoType>()
        {
            MinoType.I,
            MinoType.O,
            MinoType.T,
            MinoType.S,
            MinoType.Z,
            MinoType.J,
            MinoType.L
        };

        for (int i = 0; i < bag.Count; i++)
        {
            int randomIndex = Random.Range(i, bag.Count);
            (bag[i], bag[randomIndex]) = (bag[randomIndex], bag[i]);
        }

        foreach (var mino in bag)
        {
            _minoQueue.Enqueue(mino);
        }
    }

    /// <summary>
    /// ゴーストミノの更新
    /// </summary>
    private void UpdateGhostView()
    {
        Vector2Int ghostPos = _tetrominoPresenter.GetGhostPosition();

        TetrominoModel ghostModel =new TetrominoModel(_tetrominoModel.Data,ghostPos);

        ghostModel.RotationIndex = _tetrominoModel.RotationIndex;

        _ghostView.Refresh(ghostModel);
    }

    /// <summary>
    /// 落下速度の計算
    /// </summary>
    /// <returns>現在の落下速度</returns>
    private int GetDropTime()
    {
        int dropTime = MAX_DROP_TIME - ((_level - 1) * SCORE_DROP_TIME);

        return Mathf.Max(dropTime, MIN_DROP_TIME);
    }

    /// <summary>
    /// 現在ミノの一部が盤面上端より上にあるか判定
    /// </summary>
    /// <returns>
    /// true : 盤面上端より上  false : 盤面上端より下  
    /// </returns>
    private bool IsCurrentMinoAboveBoard()
    {
        Vector2Int[] cells = _tetrominoModel.CurrentRotation.Cells;

        foreach (Vector2Int cell in cells)
        {
            Vector2Int boardPosition = cell + _tetrominoModel.Position;

            if (boardPosition.y >= BoardModel.HEIGHT)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// スコアの加算
    /// </summary>
    /// <param name="lineCount">消したライン数</param>
    private void AddScore(int lineCount)
    {
        if (lineCount <= 0)
        {
            return;
        }

        _totalClearedLines += lineCount;
        _level = _totalClearedLines / 10 + 1;

        switch (lineCount)
        {
            case 1:
                _score += ONE_LINE_Delete;
                break;

            case 2:
                _score += SECOND_LINE_Delete;
                break;

            case 3:
                _score += THIRD_LINE_Delete;
                break;

            case 4:
                _score += FOUR_LINE_Delete;
                break;
        }

        _scoreView.Refresh(_score);
    }

    /// <summary>
    /// リスタート
    /// </summary>
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /// <summary>
    /// ゲームオーバー
    /// </summary>
    private void GameOver()
    {
        _isGameOver = true;

        _gameOverView.Show();
        _ghostView.gameObject.SetActive(false);
    }

    #endregion
}