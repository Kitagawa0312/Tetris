using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private CellView _cellPrefab;

    private CellView[,] _cells;

    private float offsetX;
    private float offsetY;

    public void Initialize()
    {
        _cells = new CellView[BoardModel.Width, BoardModel.Height];

        offsetX = -(BoardModel.Width - 1) / 2f;
        offsetY = -(BoardModel.Height - 1) / 2f;

        GenerateBoard();
    }

    public Vector3 ToWorldPosition(Vector2Int boardPosition)
    {
        return new Vector3(
            boardPosition.x + offsetX,
            boardPosition.y + offsetY,
            0);
    }

    private void GenerateBoard()
    {
        for (int y = 0; y < BoardModel.Height; y++)
        {
            for (int x = 0; x < BoardModel.Width; x++)
            {
                CellView cell = Instantiate(
                    _cellPrefab,
                    new Vector3(
                        x + offsetX,
                        y + offsetY,
                        0),
                    Quaternion.identity,
                    transform);

                _cells[x, y] = cell;
            }
        }
    }
}