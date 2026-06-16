using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private CellView _cellPrefab;

    private CellView[,] _cells;

    public void Initialize()
    {
        _cells = new CellView[BoardModel.Width, BoardModel.Height];

        GenerateBoard();
    }

    private void GenerateBoard()
    {
        float offsetX = -(BoardModel.Width - 1) / 2f;
        float offsetY = -(BoardModel.Height - 1) / 2f;

        for (int y = 0; y < BoardModel.Height; y++)
        {
            for (int x = 0; x < BoardModel.Width; x++)
            {
                Instantiate(_cellPrefab,new Vector3(x + offsetX,y + offsetY,0),Quaternion.identity,transform);
            }
        }
    }
}