using UnityEngine;

/// <summary>
/// ミノのView
/// </summary>
public class TetrominoView : MonoBehaviour
{

    #region 変数

    [SerializeField]
    private BlockView _blockPrefab = default;

    [SerializeField]
    private BoardView _boardView = default;

    [SerializeField]
    private bool _useBoardPosition = true;

    private readonly BlockView[] _blockViews = new BlockView[4];

    #endregion

    #region メソッド

    /// <summary>
    /// 初期化設定
    /// </summary>
    public void Initialize()
    {
        GenerateMino();
    }

    /// <summary>
    /// ミノの生成
    /// </summary>
    private void GenerateMino()
    {
        for (int i = 0; i < _blockViews.Length; i++)
        {
            BlockView block = Instantiate(_blockPrefab,transform);
            block.SetVisible(true);
            _blockViews[i] = block;
        }
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Refresh(TetrominoModel model)
    {
        Vector2Int[] cells = model.CurrentRotation.Cells;

        for (int i = 0; i < _blockViews.Length; i++)
        {
            _blockViews[i].SetSprite(model.Data.MinoSprite);
            _blockViews[i].SetPosition(cells[i]);
        }

        if (_useBoardPosition)
        {
            transform.position = _boardView.ToWorldPosition(model.Position);
        }
    }

    #endregion
}