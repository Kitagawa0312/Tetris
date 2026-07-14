using UnityEngine;

/// <summary>
/// 盤面のPresenter
/// </summary>
public class BoardPresenter
{
    #region 変数

    private readonly BoardModel _model;

    #endregion

    #region コンストラクタ

    /// <summary>
    /// BoardPresenterの生成
    /// </summary>
    /// <param name="model">盤面の管理Model</param>
    public BoardPresenter(BoardModel model)
    {
        this._model = model;
    }

    #endregion


    #region メソッド

    /// <summary>
    /// ミノの設置判定
    /// </summary>
    /// <param name="cells">ミノの相対座標</param>
    /// <param name="pos">ミノの配置位置</param>
    /// <returns>ture : 設置できる  false : 設置できない</returns>
    public bool CanPlace(Vector2Int[] cells, Vector2Int pos)
    {
        foreach (var c in cells)
        {
            var p = c + pos;

            if (p.x < 0 || p.x >= BoardModel.WIDTH)
            {
                return false;
            }

            if (p.y < 0)
            {
                return false;
            }

            if (p.y >= BoardModel.HEIGHT)
            {
                continue;
            }

            if (_model.IsOccupied(p.x, p.y))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// ミノの固定
    /// </summary>
    /// <param name="cells">ミノの相対座標</param>
    /// <param name="pos">ミノの配置位置</param>
    public void Fix(Vector2Int[] cells, Vector2Int pos)
    {
        foreach (var c in cells)
        {
            var p = c + pos;

            if (p.x < 0 || p.x >= BoardModel.WIDTH || p.y < 0 || p.y >= BoardModel.HEIGHT)
            {
                continue;
            }

            _model.SetOccupied(p.x, p.y, true);
        }
    }

    #endregion

}