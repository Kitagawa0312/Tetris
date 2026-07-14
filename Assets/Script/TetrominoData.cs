using UnityEngine;

/// <summary>
/// ミノのデータ
/// </summary>

// ミノの形
public enum MinoType
{
    I,
    O,
    T,
    S,
    Z,
    J,
    L
}

[CreateAssetMenu(fileName = "TetrominoData",menuName = "Tetris/Tetromino Data")]
public class TetrominoData : ScriptableObject
{
    public Sprite MinoSprite = default;

    public RotationData[] Rotations;

    public MinoType Type;
}

[System.Serializable]
public class RotationData
{
    public Vector2Int[] Cells;
}
