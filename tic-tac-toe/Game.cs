namespace TicTacToe;

public class Game
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
        Console.WriteLine($"\n***The game is over***\n");
        switch (_gameState)
        {
            case Result.PlayerO:
            case Result.PlayerX:
                Console.WriteLine($"The winner is {_gameState}!");
                break;
            default:
                Console.WriteLine("The game is a draw!");
                break;
        }
    }
}


public enum Result { PlayerO, PlayerX, Draw, Continue };
