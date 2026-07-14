using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミノのデータベース
/// </summary>
public class TetrominoDatabase : MonoBehaviour
{
    #region 変数

    [SerializeField] private TetrominoData[] _tetrominoDatas = default;

    Dictionary<MinoType,TetrominoData> _minoDictionary = new Dictionary<MinoType, TetrominoData>();

    #endregion

    #region メソッド
    
    /// <summary>
    /// 初期設定
    /// </summary>
    private void Awake()
    {
        _minoDictionary.Clear();

        for (int i = 0; i < _tetrominoDatas.Length; i++)
        {
            if (_tetrominoDatas[i] == null)
            {
                Debug.LogError($"TetrominoData が未設定です index:{i}");
                continue;
            }

            _minoDictionary.Add(_tetrominoDatas[i].Type,_tetrominoDatas[i]);
        }
    }

    /// <summary>
    /// 指定したミノの取得
    /// </summary>
    /// <param name="type">ミノの形</param>
    /// <returns>ミノのデータ</returns>
    public TetrominoData Get(MinoType type)
    {
        if (_minoDictionary.TryGetValue(type, out TetrominoData data))
        {
            return data;
        }

        Debug.LogError($"{type} の TetrominoData が登録されていません");
        return null;
    }

    #endregion
}
