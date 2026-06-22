using UnityEngine;

public class BlockView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = default;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetPosition(Vector2Int position)
    {
        transform.localPosition =  new Vector3(position.x, position.y, 0);
    }

    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
    }
}