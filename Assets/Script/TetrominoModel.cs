using UnityEngine;

/// <summary>
/// ミノのmodel
/// </summary>
public class TetrominoModel
{
    #region プロパティ

    public TetrominoData Data { get; }

    public Vector2Int Position { get; set; }

    public int RotationIndex { get; set; }

    #endregion

    #region コンストラクタ

    /// <summary>
    /// TetrominoModelの生成
    /// </summary>
    /// <param name="data">ミノの情報</param>
    /// <param name="position">生成位置</param>
    public TetrominoModel(TetrominoData data, Vector2Int position)
    {
        Data = data;
        Position = position;
        RotationIndex = 0;
    }

    #endregion

    #region メソッド
    
    /// <summary>
    /// ミノの回転取得
    /// </summary>
    public RotationData CurrentRotation
    {
        get
        {
            return Data.Rotations[RotationIndex];
        }
    }

    #endregion

}