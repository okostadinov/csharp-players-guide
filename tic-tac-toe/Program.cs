new Game().Play();
class Game
{
    private readonly Board _board = new();
    private readonly Player _playerO = new("O");
    private readonly Player _playerX = new("X");
    private Player? _currentPlayer;
    private Result _gameState = Result.Continue;

    private void ChangePlayerTurn()
    {
        _currentPlayer = _currentPlayer == _playerO ? _playerX : _playerO;
    }

    public void Play()
    {
        bool playing = true;
        _currentPlayer = _playerO;

        while (playing)
        {
            int index = _currentPlayer.ChooseTile(_board);
            _board.UpdateTile(index, _currentPlayer.Sign);
            _board.DisplayBoard();
            _gameState = _board.CheckResult();
            if (_gameState == Result.Continue)
                ChangePlayerTurn();
            else
                playing = false;

        }

        DisplayResult();
    }

    private void DisplayResult()
    {
        System.Console.WriteLine($"***The game is over***\n");
        switch (_gameState)
        {
            case Result.PlayerO:
            case Result.PlayerX:
                System.Console.WriteLine($"The winner is {_gameState}!");
                break;
            default:
                System.Console.WriteLine("The game is a draw!");
                break;
        }
    }
}

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
        System.Console.WriteLine($" {_tiles[6]} | {_tiles[7]} | {_tiles[8]} ");
        System.Console.WriteLine("---+---+---");
        System.Console.WriteLine($" {_tiles[3]} | {_tiles[4]} | {_tiles[5]} ");
        System.Console.WriteLine("---+---+---");
        System.Console.WriteLine($" {_tiles[0]} | {_tiles[1]} | {_tiles[2]} ");
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

class Player(string sign)
{
    public string Sign { get; } = sign;

    public int ChooseTile(Board board)
    {
        bool validInput = false;
        int index = 0;
        System.Console.WriteLine($"Player {Sign}'s turn.");

        do
        {
            System.Console.Write($"Please choose a tile (1-9): ");
            string? input = Console.ReadLine();
            if (input != null)
            {
                if (int.TryParse(input, out index))
                {
                    if (index > 0 && index < 10)
                    {
                        if (board.CheckAvailableTile(index))
                        {
                            validInput = true;
                        }
                        else
                        {
                            System.Console.WriteLine("Tile already occupied. Try again.");
                            continue;
                        }
                    }
                }
            }

            if (!validInput)
                System.Console.WriteLine("Invalid tile index. Try again.");
        } while (!validInput);

        return index - 1;
    }
}

enum Result { PlayerO, PlayerX, Draw, Continue };