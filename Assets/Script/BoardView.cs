using UnityEngine;

/// <summary>
/// 盤面の表示View
/// </summary>
public class BoardView : MonoBehaviour
{
    #region 変数

    [SerializeField] private CellView _cellPrefab;
    [SerializeField] private BlockView _blockPrefab;

    // 背景のセル
    private CellView[,] _cells;
    BlockView[,] fixedBlocks = default;

    // 中央寄せのX方向のオフセット
    private float _offsetX = default;

    // 中央寄せのY方向のオフセット
    private float _offsetY = default;

    #endregion

    #region メソッド

    /// <summary>
    /// 初期設定
    /// </summary>
    public void Initialize()
    {
        _cells = new CellView[BoardModel.WIDTH, BoardModel.HEIGHT];
        fixedBlocks = new BlockView[BoardModel.WIDTH,BoardModel.HEIGHT];
        _offsetX = -(BoardModel.WIDTH - 1) / 2f;
        _offsetY = -(BoardModel.HEIGHT - 1) / 2f;

        GenerateBoard();
    }

    /// <summary>
    /// 盤面の座標をワールド座標に変換
    /// </summary>
    /// <param name="boardPosition">盤面の座標</param>
    /// <returns>ワールド座標</returns>
    public Vector3 ToWorldPosition(Vector2Int boardPosition)
    {
        return new Vector3(
            boardPosition.x + _offsetX,
            boardPosition.y + _offsetY,
            0);
    }

    /// <summary>
    /// 盤面の生成
    /// </summary>
    private void GenerateBoard()
    {
        for (int y = 0; y < BoardModel.HEIGHT; y++)
        {
            for (int x = 0; x < BoardModel.WIDTH; x++)
            {
                CellView cell = Instantiate(
                    _cellPrefab,
                    new Vector3(
                        x + _offsetX,
                        y + _offsetY,
                        0),
                    Quaternion.identity,
                    transform);

                _cells[x, y] = cell;
            }
        }
    }

    /// <summary>
    /// ミノを固定したものとして表示する
    /// </summary>
    /// <param name="cells">ミノのセルの相対座標</param>
    /// <param name="pos">ミノの配置位置</param>
    /// <param name="sprite">ミノの画像</param>
    public void CreateFixedBlocks(Vector2Int[] cells,Vector2Int pos,Sprite sprite)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector2Int boardPos = cells[i] + pos;

            if (boardPos.x < 0 || boardPos.x >= BoardModel.WIDTH || boardPos.y < 0 || boardPos.y >= BoardModel.HEIGHT)
            {
                continue;
            }

            BlockView block = Instantiate(_blockPrefab,ToWorldPosition(boardPos),Quaternion.identity,transform);

            block.SetSprite(sprite);
            block.SetVisible(true);
            fixedBlocks[boardPos.x, boardPos.y] = block;
        }
    }

    /// <summary>
    /// 行の削除、全体を下に下げる
    /// </summary>
    /// <param name="y"></param>
    public void DeleteLine(int y)
    {
        for (int x = 0; x < BoardModel.WIDTH; x++)
        {
            if (fixedBlocks[x, y] != null)
            {
                Destroy(fixedBlocks[x, y].gameObject);
                fixedBlocks[x, y] = null;
            }
        }

        for (int row = y; row < BoardModel.HEIGHT - 1; row++)
        {
            for (int x = 0; x < BoardModel.WIDTH; x++)
            {
                fixedBlocks[x, row] = fixedBlocks[x, row + 1];

                if (fixedBlocks[x, row] != null)
                {
                    fixedBlocks[x, row].transform.position =
                        ToWorldPosition(new Vector2Int(x, row));
                }
            }
        }

        for (int x = 0; x < BoardModel.WIDTH; x++)
        {
            fixedBlocks[x, BoardModel.HEIGHT - 1] = null;
        }
    }

    #endregion
}