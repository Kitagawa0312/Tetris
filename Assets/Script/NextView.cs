using UnityEngine;

public class NextView : MonoBehaviour
{
    #region •Ï”

    [SerializeField]
    private TetrominoView[] _nextTetrominoViews = default;

    #endregion

    #region •Ï”

    /// <summary>
    /// ‰Šú‰»İ’è
    /// </summary>
    public void Initialize()
    {
        for (int i = 0; i < _nextTetrominoViews.Length; i++)
        {
            _nextTetrominoViews[i].Initialize();
        }
    }

    /// <summary>
    /// XVˆ—
    /// </summary>
    public void Refresh(TetrominoModel[] models)
    {
        for (int i = 0; i < models.Length; i++)
        {
            _nextTetrominoViews[i].Refresh(models[i]);
        }
    }

    #endregion
}