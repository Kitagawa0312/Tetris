using System.Collections.Generic;
using UnityEngine;

public class TetrominoDatabase : MonoBehaviour
{
    [SerializeField] private TetrominoData[] _tetrominoDatas = default;

    Dictionary<MinoType,TetrominoData> _minoDictionary = new Dictionary<MinoType, TetrominoData>();

    private void Awake()
    {
        _minoDictionary.Clear();

        for (int i = 0; i < _tetrominoDatas.Length; i++)
        {
            if (_tetrominoDatas[i] == null)
            {
                Debug.LogError($"TetrominoData ‚ª–¢Ý’è‚Å‚· index:{i}");
                continue;
            }

            _minoDictionary.Add(
                _tetrominoDatas[i].Type,
                _tetrominoDatas[i]);
        }
    }

    public TetrominoData Get(MinoType type)
    {
        if (_minoDictionary.TryGetValue(type, out TetrominoData data))
        {
            return data;
        }

        Debug.LogError($"{type} ‚Ì TetrominoData ‚ª“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñ");
        return null;
    }
}
