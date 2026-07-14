using System.Collections.Generic;

/// <summary>
/// 盤面の管理Model
/// </summary>
public class BoardModel
{

    #region 定数

    // 横の大きさ
    public const int WIDTH = 10;
    
    // 縦の大きさ
    public const int HEIGHT = 20;

    // ミノの有無の判定
    private readonly bool[,] _cells = new bool[WIDTH, HEIGHT];

    #endregion

    #region メソッド

    /// <summary>
    /// マスにミノがあるかの判定
    /// </summary>
    /// <param name="x">X座標</param>
    /// <param name="y">Y座標</param>
    /// <returns>ture : ミノが配置されている false : ミノが配置されていない</returns>
    public bool IsOccupied(int x, int y)
    {
        return _cells[x, y];
    }

    /// <summary>
    /// 指定したマスのミノの更新
    /// </summary>
    /// <param name="x">X座標</param>
    /// <param name="y">Y座標</param>
    /// <param name="value">ture : ミノの配置  false : 空にする</param>
    public void SetOccupied(int x, int y, bool value)
    {
        _cells[x, y] = value;
    }

    /// <summary>
    /// 行のミノ判定
    /// </summary>
    /// <param name="y">判定する行</param>
    /// <returns>ture : ミノで埋まっている  false : ミノが埋まっていない</returns>
    public bool IsLineFull(int y)
    {
        for(int i = 0; i < _cells.GetLength(0); i++)
        {
            if (_cells[i,y] == false)
            {  
                return false; 
            }
        }
        return true;
    }

    /// <summary>
    /// ミノがそろっている行の取得
    /// </summary>
    /// <returns>揃っている行</returns>
    public List<int> GetFullLines()
    {
        List<int> fullLines = new List<int>();

        for (int y = 0; y < HEIGHT; y++)
        {
            if (IsLineFull(y))
            {
                fullLines.Add(y);
            }
        }

        return fullLines;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="y"></param>
    public void DeleteLine(int y)
    {
        for (int row = y; row < HEIGHT - 1; row++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                _cells[x, row] = _cells[x, row + 1];
            }
        }

        for (int x = 0; x < WIDTH; x++)
        {
            _cells[x, HEIGHT - 1] = false;
        }
    }

    /// <summary>
    /// 揃っている行の削除
    /// </summary>
    /// <returns>削除した行数</returns>
    public int DeleteFullLines()
    {
        int deletedCount = 0;

        for (int y = 0; y < HEIGHT; y++)
        {
            if (IsLineFull(y))
            {
                DeleteLine(y);
                deletedCount++;

                y--;
            }
        }

        return deletedCount;
    }

    #endregion

}
