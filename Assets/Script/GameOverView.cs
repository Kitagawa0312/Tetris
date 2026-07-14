using UnityEngine;

/// <summary>
/// ゲームオーバーView
/// </summary>
public class GameOverView : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverPanel = default;

    /// <summary>
    /// 初期設定
    /// </summary>
    public void Initialize()
    {
        _gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// ゲームオーバーUIを表示する
    /// </summary>
    public void Show()
    {
        _gameOverPanel.SetActive(true);
    }

    /// <summary>
    /// ゲームオーバーUIを非表示にする
    /// </summary>
    public void Hide()
    {
        _gameOverPanel.SetActive(false);
    }
}