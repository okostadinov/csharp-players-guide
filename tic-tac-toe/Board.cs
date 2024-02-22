namespace TicTacToe;

class Board
{
    private readonly string[] _tiles = new string[9];

    public Board()
    {
        InitBoard();
    }

    private void InitBoard()
    {
        for (int i = 0; i < _tiles.Length; i++)
        {
            _tiles[i] = " ";
        }
    }
    public void DisplayBoard()
    {
        Console.WriteLine($" {_tiles[6]} | {_tiles[7]} | {_tiles[8]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {_tiles[3]} | {_tiles[4]} | {_tiles[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {_tiles[0]} | {_tiles[1]} | {_tiles[2]} ");
    }

    public bool CheckAvailableTile(int index)
    {
        return _tiles[index - 1] == " ";
    }

    public void UpdateTile(int index, string sign)
    {
        _tiles[index] = sign;
    }

    public Result CheckResult()
    {
        if (!_tiles.Contains(" "))
            return Result.Draw;

        if (_tiles[0] == _tiles[1] && _tiles[1] == _tiles[2] && _tiles[0] != " ")
            return _tiles[0] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[3] == _tiles[4] && _tiles[4] == _tiles[5] && _tiles[3] != " ")
            return _tiles[3] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[6] == _tiles[7] && _tiles[7] == _tiles[8] && _tiles[6] != " ")
            return _tiles[6] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[0] == _tiles[3] && _tiles[3] == _tiles[6] && _tiles[0] != " ")
            return _tiles[0] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[1] == _tiles[4] && _tiles[4] == _tiles[7] && _tiles[1] != " ")
            return _tiles[1] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[2] == _tiles[5] && _tiles[5] == _tiles[8] && _tiles[2] != " ")
            return _tiles[2] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[0] == _tiles[4] && _tiles[4] == _tiles[8] && _tiles[0] != " ")
            return _tiles[0] == "O" ? Result.PlayerO : Result.PlayerX;
        else if (_tiles[2] == _tiles[4] && _tiles[4] == _tiles[6] && _tiles[2] != " ")
            return _tiles[2] == "O" ? Result.PlayerO : Result.PlayerX;

        return Result.Continue;
    }
}
