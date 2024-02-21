Player player = new();
Game game = new(player);
game.Play();

public class Map
{
    private readonly RoomType[,] _grid;
    public int Rows { get; }
    public int Columns { get; }

    public Map(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _grid = new RoomType[Rows, Columns];

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                _grid[i, j] = RoomType.EmptyRoom;
            }
        }

        _grid[0, 0] = RoomType.EntranceRoom;
        _grid[Rows - (Rows / 2), Columns - (Columns / 3)] = RoomType.FountainRoom;
    }

    public bool IsWithinMap(Coordinate coordinate)
    {
        return coordinate.Row >= 0 && coordinate.Column >= 0 && coordinate.Row < Rows && coordinate.Column < Columns;
    }

    public RoomType GetRoomTypeByCoordinate(Coordinate coordinate)
    {
        return _grid[coordinate.Row, coordinate.Column];
    }
}

public record Coordinate(int Row, int Column);

public class Player
{
    public Coordinate CurrentLocation { get; set; } = new(0, 0);

    public override string ToString() => $"(Row={CurrentLocation.Row}, Column={CurrentLocation.Column})";
}

public class Game
{
    public Player Player { get; }
    public Map Map { get; }
    public bool IsFountainActive { get; set; } = false;

    public Game(Player player)
    {
        Player = player;
        int mapSize = GetMapSize();
        Map = new(mapSize, mapSize);
    }

    public void Play()
    {
        while (true)
        {
            DescribeSetting();

            if (!CheckVictoryConditions())
            {
                GetCommand();
            }
            else
            {
                DeclareVictory();
                break;
            }
        }
    }

    private int GetMapSize()
    {
        bool validInput = false;
        int mapSize = 0;
        Console.Write("Which map size would like to play? (small/medium/large)? ");

        do
        {
            string? input = Console.ReadLine();

            if (input != null)
            {
                mapSize = input switch
                {
                    "small" => 4,
                    "medium" => 6,
                    "large" => 8,
                    _ => 0
                };

                if (mapSize != 0)
                    validInput = true;
                else
                    Console.Write("Invalid input. Please try again (small/medium/large) ");
            }
        } while (!validInput);

        return mapSize;
    }

    private bool CheckVictoryConditions() => Map.GetRoomTypeByCoordinate(Player.CurrentLocation) == RoomType.EntranceRoom && IsFountainActive;

    public void DeclareVictory()
    {
        ColoredText.Display(ConsoleColor.Green, "The Fountain of Objects has been reactivated, and you have escaped with your life!");
        ColoredText.Display(ConsoleColor.Green, "You win!");
    }

    public void DescribeSetting()
    {
        Console.WriteLine("----------------------------------------------------------------------------------");
        Console.WriteLine($"You are in the room at {Player}");

        RoomType currentRoom = Map.GetRoomTypeByCoordinate(Player.CurrentLocation);

        switch (currentRoom)
        {
            case RoomType.EntranceRoom:
                ColoredText.Display(ConsoleColor.White, "You see light coming from the cavern entrance.");
                break;
            case RoomType.FountainRoom:
                if (!IsFountainActive)
                    ColoredText.Display(ConsoleColor.DarkBlue, "You hear water dripping in this room. The Fountain of Objects is here!");
                else
                    ColoredText.Display(ConsoleColor.Cyan, "You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
                break;
            default:
                break;
        }
    }
    public void GetCommand()
    {
        Console.Write("What do you want to do? ");
        string? input = Console.ReadLine();
        ICommand command = input switch
        {
            "move north" => new MoveCommand(Direction.North),
            "move south" => new MoveCommand(Direction.South),
            "move east" => new MoveCommand(Direction.East),
            "move west" => new MoveCommand(Direction.West),
            "enable fountain" => new EnableFountainCommand(),
            _ => new InvalidCommand()
        };

        command.Execute(this);
    }
}



public interface ICommand
{
    public void Execute(Game game);
}

public class MoveCommand(Direction direction) : ICommand
{
    public Direction Direction { get; } = direction;

    public void Execute(Game game)
    {
        Coordinate playerLocation = game.Player.CurrentLocation;
        Coordinate newLocation = Direction switch
        {
            Direction.North => playerLocation with { Row = playerLocation.Row - 1 },
            Direction.South => playerLocation with { Row = playerLocation.Row + 1 },
            Direction.East => playerLocation with { Column = playerLocation.Column + 1 },
            Direction.West => playerLocation with { Column = playerLocation.Column - 1 },
            _ => playerLocation,
        };

        if (!game.Map.IsWithinMap(newLocation))
            ColoredText.Display(ConsoleColor.Yellow, "There's a wall there.");
        else
            game.Player.CurrentLocation = newLocation;
    }
}

public class EnableFountainCommand : ICommand
{
    public void Execute(Game game)
    {
        if (game.Map.GetRoomTypeByCoordinate(game.Player.CurrentLocation) == RoomType.FountainRoom)
            game.IsFountainActive = true;
        else
            ColoredText.Display(ConsoleColor.Yellow, "The fountain isn't here.");
    }
}

public class InvalidCommand : ICommand
{
    public void Execute(Game game) => ColoredText.Display(ConsoleColor.Red, "Invalid command.");
}

public abstract class ColoredText()
{
    public static void Display(ConsoleColor textColor, string text)
    {
        Console.ForegroundColor = textColor;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}

public enum Direction { North, South, East, West };

public enum RoomType { EntranceRoom, FountainRoom, EmptyRoom };