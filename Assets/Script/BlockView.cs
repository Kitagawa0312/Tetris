using UnityEngine;

/// <summary>
/// ブロックのView
/// </summary>
public class BlockView : MonoBehaviour
{
    #region 変数

    [SerializeField]
    private SpriteRenderer _spriteRenderer = default;

    #endregion

    #region メソッド

    /// <summary>
    /// ブロックのsprite変更
    /// </summary>
    /// <param name="sprite"></param>
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }

    /// <summary>
    /// ブロックの位置
    /// </summary>
    /// <param name="position"></param>
    public void SetPosition(Vector2Int position)
    {
        transform.localPosition =  new Vector3(position.x, position.y, 0);
    }

    /// <summary>
    /// ミノの表示
    /// </summary>
    /// <param name="visible"></param>
    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }

    #endregion

}