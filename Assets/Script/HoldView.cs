using UnityEngine;

/// <summary>
/// ホールドミノの表示を管理するView
/// </summary>
public class HoldView : MonoBehaviour
{
    #region 変数

    [SerializeField]
    private TetrominoView _holdTetrominoView = default;

    #endregion

    #region メソッド

    /// <summary>
    /// 初期化設定
    /// </summary>
    public void Initialize()
    {
        _holdTetrominoView.Initialize();
        _holdTetrominoView.gameObject.SetActive(false);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    /// <param name="model"></param>
    public void Refresh(TetrominoModel model)
    {
        _holdTetrominoView.gameObject.SetActive(true);
        _holdTetrominoView.Refresh(model);
    }

    #endregion
}