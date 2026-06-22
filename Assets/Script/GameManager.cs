using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BoardView boardView = default;
    [SerializeField] private TetrominoView tetrominoView = default;
    [SerializeField] private TetrominoDatabase database = default;
    
    private BoardModel boardModel = default;
    private BoardPresenter boardPresenter = default;

    private TetrominoPresenter tetrominoPresenter = default;
    private TetrominoModel tetrominoModel = default;

    private void Start()
    {
        // Modelê∂ê¨
        boardModel = new BoardModel();

        // Presenterê∂ê¨
        boardPresenter = new BoardPresenter(boardModel);

        // Viewèâä˙âª
        boardView.Initialize();

        //GenerateMino();
        tetrominoPresenter = new TetrominoPresenter(tetrominoModel, tetrominoView, boardPresenter);
    }

    public void GenerateMino()
    {
        tetrominoModel = new TetrominoModel(database.Get(MinoType.I), new Vector2Int(4,20));
        tetrominoView.Initialize();
        tetrominoView.Refresh(tetrominoModel);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tetrominoPresenter.MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tetrominoPresenter.MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tetrominoPresenter.MoveDown();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            tetrominoPresenter.RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            tetrominoPresenter.RotateLeft();
        }
    }

    public async UniTask DropLoop()
    {

    }


}