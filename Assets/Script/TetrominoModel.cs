using UnityEngine;

public class TetrominoModel
{
    public TetrominoData Data { get; }

    public Vector2Int Position { get; set; }

    public int RotationIndex { get; set; }

    public TetrominoModel(TetrominoData data, Vector2Int position)
    {
        Data = data;
        Position = position;
        RotationIndex = 0;
    }

    public RotationData CurrentRotation
    {
        get
        {
            return Data.Rotations[RotationIndex];
        }
    }
}