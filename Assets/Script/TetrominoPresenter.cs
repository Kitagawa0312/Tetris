using UnityEngine;

/// <summary>
/// ミノのPresenter
/// </summary>
public class TetrominoPresenter
{
    #region 変数

    private readonly TetrominoModel _model;
    private readonly TetrominoView _view;
    private readonly BoardPresenter _boardPresenter;

    #endregion

    #region コンストラクタ

    /// <summary>
    /// TetrominoPresenterの生成
    /// </summary>
    /// <param name="model">ミノの管理Model</param>
    /// <param name="view">盤面の表示View</param>
    /// <param name="boardPresenter">盤面の管理Model</param>
    public TetrominoPresenter(TetrominoModel model,TetrominoView view,BoardPresenter boardPresenter)
    {
        this._model = model;
        this._view = view;
        this._boardPresenter = boardPresenter;
    }

    #endregion

    #region メソッド

    /// <summary>
    /// ミノが動けるかの判定
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool TryMove(Vector2Int direction)
    {
        Vector2Int nextPos = _model.Position + direction;

        if (!_boardPresenter.CanPlace(_model.CurrentRotation.Cells,nextPos))
        {
            return false;
        }

        _model.Position = nextPos;
        _view.Refresh(_model);

        return true;
    }

    /// <summary>
    /// 回転できるかの判定
    /// </summary>
    /// <param name="nextRotationIndex"></param>
    /// <returns></returns>
    public bool TryRotate(int nextRotationIndex)
    {
        if (!_boardPresenter.CanPlace(_model.Data.Rotations[nextRotationIndex].Cells,_model.Position))
        {
            return false;
        }
        _model.RotationIndex = nextRotationIndex;
        _view.Refresh(_model);
        return true;
    }
    
    /// <summary>
    /// 左方向への移動
    /// </summary>
    public void MoveLeft()
    {
        TryMove(Vector2Int.left);
    }


    /// <summary>
    /// 右方向への移動
    /// </summary>
    public void MoveRight()
    {
        TryMove(Vector2Int.right);
    }


    /// <summary>
    /// 下方向への移動
    /// </summary>
    public bool MoveDown()
    {
        return TryMove(Vector2Int.down);
    }

    /// <summary>
    /// 右回転
    /// </summary>
    public void RotateRight()
    {
        int nextRotate = (_model.RotationIndex + 1) % 4;
        TryRotate(nextRotate);
    }

    /// <summary>
    /// 左方向への移動
    /// </summary>
    public void RotateLeft()
    {
        int nextRotate = (_model.RotationIndex + 3) % 4;
        TryRotate(nextRotate);
    }


    /// <summary>
    /// ゴーストミノの位置変更
    /// </summary>
    public Vector2Int GetGhostPosition()
    {
        Vector2Int ghostPos = _model.Position;

        while (_boardPresenter.CanPlace(
                   _model.CurrentRotation.Cells,
                   ghostPos + Vector2Int.down))
        {
            ghostPos += Vector2Int.down;
        }

        return ghostPos;
    }

    #endregion
}