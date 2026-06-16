public class BoardModel
{
    public const int Width = 10;
    public const int Height = 20;

    private readonly bool[,] cells = new bool[Width, Height];

    public bool IsOccupied(int x, int y)
    {
        return cells[x, y];
    }

    public void SetOccupied(int x, int y, bool value)
    {
        cells[x, y] = value;
    }
}
