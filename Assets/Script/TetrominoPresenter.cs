using UnityEngine;

public class TetrominoPresenter
{
    private readonly TetrominoModel model;
    private readonly TetrominoView view;
    private readonly BoardPresenter boardPresenter;

    public TetrominoPresenter(TetrominoModel model,TetrominoView view,BoardPresenter boardPresenter)
    {
        this.model = model;
        this.view = view;
        this.boardPresenter = boardPresenter;
    }

    public bool TryMove(Vector2Int direction)
    {
        Vector2Int nextPos = model.Position + direction;

        if (!boardPresenter.CanPlace(model.CurrentRotation.Cells,nextPos))
        {
            return false;
        }

        model.Position = nextPos;
        view.Refresh(model);

        return true;
    }

    public bool TryRotate(int nextRotationIndex)
    {
        if (!boardPresenter.CanPlace(model.Data.Rotations[nextRotationIndex].Cells,model.Position))
        {
            return false;
        }
        model.RotationIndex = nextRotationIndex;
        view.Refresh(model);
        return true;
    }

    public void MoveLeft()
    {
        TryMove(Vector2Int.left);
    }

    public void MoveRight()
    {
        TryMove(Vector2Int.right);
    }

    public bool MoveDown()
    {
        return TryMove(Vector2Int.down);
    }

    public void RotateRight()
    {
        int nextRotate = (model.RotationIndex + 1) % 4;
        TryRotate(nextRotate);
    }

    public void RotateLeft()
    {
        int nextRotate = (model.RotationIndex + 3) % 4;
        TryRotate(nextRotate);
    }
}