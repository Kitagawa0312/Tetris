using UnityEngine;

public class BoardPresenter
{
    private readonly BoardModel model;

    public BoardPresenter(BoardModel model)
    {
        this.model = model;
    }

    public bool CanPlace(Vector2Int[] cells, Vector2Int pos)
    {
        foreach (var c in cells)
        {
            var p = c + pos;

            if (p.x < 0 || p.x >= BoardModel.Width ||
                p.y < 0 || p.y >= BoardModel.Height)
            {
                return false;
            }

            if (model.IsOccupied(p.x, p.y))
            {
                return false;
            }
        }

        return true;
    }

    public void Fix(Vector2Int[] cells, Vector2Int pos)
    {
        foreach (var c in cells)
        {
            var p = c + pos;
            model.SetOccupied(p.x, p.y, true);
        }
    }
}