using UnityEngine;

/// <summary>
/// ゴーストミノの表示を管理するView
/// </summary>
public class GhostView : MonoBehaviour
{

    #region 変数

    [SerializeField]
    private TetrominoView _ghostTetrominoView = default;

    #endregion

    #region メソッド

    /// <summary>
    /// ゴーストミノを初期化する
    /// </summary>
    public void Initialize()
    {
        _ghostTetrominoView.Initialize();
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Refresh(TetrominoModel model)
    {
        _ghostTetrominoView.Refresh(model);
    }

    /// <summary>
    /// ゴーストミノの表示・非表示を切り替える
    /// </summary>
    /// <param name="visible">
    /// true: 表示する
    /// false: 非表示にする
    /// </param>
    public void SetVisible(bool visible)
    {
        _ghostTetrominoView.gameObject.SetActive(visible);
    }

    #endregion
}