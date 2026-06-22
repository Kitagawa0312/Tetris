using UnityEngine;

public class TetrominoView : MonoBehaviour
{
    [SerializeField]
    private BlockView _blockPrefab = default;

    [SerializeField]
    private BoardView _boardView = default;

    private readonly BlockView[] _blockViews = new BlockView[4];

    public void Initialize()
    {
        GenerateMino();
    }

    private void GenerateMino()
    {
        for (int i = 0; i < _blockViews.Length; i++)
        {
            BlockView block = Instantiate(
                _blockPrefab,
                transform
            );

            block.SetVisible(true);

            _blockViews[i] = block;
        }
    }

    public void Refresh(TetrominoModel model)
    {
        Vector2Int[] cells = model.CurrentRotation.Cells;

        for (int i = 0; i < _blockViews.Length; i++)
        {
            _blockViews[i].SetSprite(model.Data._minoSprite);
            _blockViews[i].SetPosition(cells[i]);
        }

        transform.position =
            _boardView.ToWorldPosition(model.Position);
    }
}