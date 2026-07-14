using TMPro;
using UnityEngine;

/// <summary>
/// スコア表示
/// </summary>
public class ScoreView : MonoBehaviour
{
    #region 変数

    [SerializeField] private TextMeshProUGUI _score = default;

    #endregion

    #region メソッド

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Refresh(int score)
    {
        _score.text = score.ToString();
    }

    #endregion
}
