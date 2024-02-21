using System.Configuration.Assemblies;
using System.Drawing;

Player player = new();
Game game = new(player);
game.Play();

public class Map
{
    private readonly RoomType[,] _grid;
    private readonly MapSize _mapSize;
    public int Rows { get; }
    public int Columns { get; }

    Random Random = new();

    public Map(MapSize mapSize)
    {
        _mapSize = mapSize;
        Rows = Columns = _mapSize switch
        {
            MapSize.Small => 4,
            MapSize.Medium => 6,
            MapSize.Large => 8,
            _ => 0
        };
        _grid = new RoomType[Rows, Columns];

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                _grid[i, j] = RoomType.EmptyRoom;
            }
        }

        AddSpecialtyRooms();
    }

    private void AddSpecialtyRooms()
    {
        _grid[0, 0] = RoomType.EntranceRoom;

        Coordinate fountainRoom = GenerateRandomRoomCoordinate();
        _grid[fountainRoom.Row, fountainRoom.Column] = RoomType.FountainRoom;

        switch (_mapSize)
        {
            case MapSize.Small:
                Coordinate pitRoom = GenerateRandomRoomCoordinate();
                _grid[pitRoom.Row, pitRoom.Column] = RoomType.PitRoom;
                break;
            case MapSize.Medium:
                for (int i = 0; i < 2; i++)
                {
                    pitRoom = GenerateRandomRoomCoordinate();
                    _grid[pitRoom.Row, pitRoom.Column] = RoomType.PitRoom;
                }
                break;
            case MapSize.Large:
                for (int i = 0; i < 4; i++)
                {
                    pitRoom = GenerateRandomRoomCoordinate();
                    _grid[pitRoom.Row, pitRoom.Column] = RoomType.PitRoom;
                }
                break;
            default:
                break;
        }
    }

    private Coordinate GenerateRandomRoomCoordinate()
    {
        while (true)
        {
            Coordinate coordinate = new(Random.Next(1, Rows), Random.Next(1, Columns));
            if (_grid[coordinate.Row, coordinate.Column] == RoomType.EmptyRoom) return coordinate;
        }
    }

    public bool IsWithinMap(Coordinate coordinate)
    {
        return coordinate.Row >= 0 && coordinate.Column >= 0 && coordinate.Row < Rows && coordinate.Column < Columns;
    }

    public RoomType GetRoomTypeByCoordinate(Coordinate coordinate)
    {
        return _grid[coordinate.Row, coordinate.Column];
    }

    public bool IsAdjacentPitRoom(Coordinate coordinate)
    {
        if (coordinate.Row == 0)
        {
            if (_grid[coordinate.Row + 1, coordinate.Column] == RoomType.PitRoom) return true;

            if (coordinate.Column == 0)
            {
                if (_grid[coordinate.Row, coordinate.Column + 1] == RoomType.PitRoom) return true;
            }
            else if (coordinate.Column == Columns - 1)
            {
                if (_grid[coordinate.Row, coordinate.Column - 1] == RoomType.PitRoom) return true;
            }
            else
            {
                if (_grid[coordinate.Row, coordinate.Column + 1] == RoomType.PitRoom ||
                    _grid[coordinate.Row, coordinate.Column - 1] == RoomType.PitRoom) return true;
            }
        }
        else if (coordinate.Row == Rows - 1)
        {
            if (_grid[coordinate.Row - 1, coordinate.Column] == RoomType.PitRoom) return true;

            if (coordinate.Column == 0)
            {
                if (_grid[coordinate.Row, coordinate.Column + 1] == RoomType.PitRoom) return true;
            }
            else if (coordinate.Column == Columns - 1)
            {
                if (_grid[coordinate.Row, coordinate.Column - 1] == RoomType.PitRoom) return true;
            }
            else
            {
                if (_grid[coordinate.Row, coordinate.Column + 1] == RoomType.PitRoom ||
                _grid[coordinate.Row, coordinate.Column - 1] == RoomType.PitRoom) return true;
            }
        }
        else
        {
            if (_grid[coordinate.Row + 1, coordinate.Column] == RoomType.PitRoom ||
                _grid[coordinate.Row - 1, coordinate.Column] == RoomType.PitRoom) return true;

            if (coordinate.Column == 0)
            {
                if (_grid[coordinate.Row, coordinate.Column + 1] == RoomType.PitRoom) return true;
            }
            else if (coordinate.Column == Columns - 1)
            {
                if (_grid[coordinate.Row, coordinate.Column - 1] == RoomType.PitRoom) return true;
            }
            else
            {
                if (_grid[coordinate.Row, coordinate.Column + 1] == RoomType.PitRoom ||
                    _grid[coordinate.Row, coordinate.Column - 1] == RoomType.PitRoom) return true;
            }
        }

        return false;

    }
}

public record Coordinate(int Row, int Column);

public class Player
{
    public bool IsAlive { get; set; } = true;
    public Coordinate CurrentLocation { get; set; } = new(0, 0);

    public override string ToString() => $"(Row={CurrentLocation.Row}, Column={CurrentLocation.Column})";
}

public class Game
{
    public Player Player { get; }
    public Map Map { get; }
    public bool IsFountainActive { get; set; } = false;

    public bool IsPlaying { get; set; } = true;

    public Game(Player player)
    {
        Player = player;
        MapSize mapSize = GetMapSize();
        Map = new(mapSize);
    }

    public void Play()
    {
        DisplayStartText();

        while (true)
        {
            if (!IsPlaying) {
                ColoredText.Display(ConsoleColor.White,"\n'til next time!");
                break;
            }

            DescribeSetting();

            if (!Player.IsAlive)
            {
                DeclareLoss();
                break;
            }

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

    private void DisplayStartText()
    {
        System.Console.WriteLine("\nYou enter the Cavern of Objects, a maze of rooms filled with dangerous pits in search of the Fountain of Objects.");
        System.Console.WriteLine("Light is visible only in the entrance, and no other light is seen anywhere in the caverns.");
        System.Console.WriteLine("You must navigate the Caverns with your other senses.");
        System.Console.WriteLine("Find the Fountain of Objects, activate it, and return to the entrance.");
        System.Console.WriteLine("Look out for pits. You will feel a breeze if a pit is in an adjacent room. If you enter a room with a pit, you will die.\n");
    }

    private MapSize GetMapSize()
    {
        bool validInput = false;
        MapSize mapSize = 0;
        Console.Write("Which map size would like to play? (small/medium/large)? ");

        do
        {
            string? input = Console.ReadLine();

            if (input != null)
            {
                mapSize = input switch
                {
                    "small" => MapSize.Small,
                    "medium" => MapSize.Medium,
                    "large" => MapSize.Large,
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

    public void DeclareLoss()
    {
        ColoredText.Display(ConsoleColor.DarkRed, "Game over. You have lost.");
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
            case RoomType.PitRoom:
                Player.IsAlive = false;
                ColoredText.Display(ConsoleColor.DarkRed, "Oh no, it's a pit! You've fallen, there's no way out...");
                break;
            default:
                break;
        }

        if (Map.IsAdjacentPitRoom(Player.CurrentLocation))
            ColoredText.Display(ConsoleColor.Yellow, "You feel a draft. There is a pit in a nearby room.");
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
            "help" => new DisplayHelpCommand(),
            "exit" => new QuitCommand(),
            _ => new InvalidCommand()
        };

        command.Execute(this);
    }
}



public interface ICommand
{
    public void Execute(Game game);
}

public class DisplayHelpCommand() : ICommand
{
    public void Execute(Game game)
    {
        System.Console.WriteLine("You can perform the following commands:");
        ColoredText.Display(ConsoleColor.Magenta, "\tnorth => move to the room above");
        ColoredText.Display(ConsoleColor.Magenta, "\tsouth => move to the room bellow");
        ColoredText.Display(ConsoleColor.Magenta, "\teast => move to the room to the right");
        ColoredText.Display(ConsoleColor.Magenta, "\twest => move to the room to the left");
        ColoredText.Display(ConsoleColor.Magenta, "\tenable fountain => activate the fountain when located");
        ColoredText.Display(ConsoleColor.Magenta, "\texit => quit the game");
    }
}

public class QuitCommand() : ICommand
{
    public void Execute(Game game) => game.IsPlaying = false;
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

public enum RoomType { EntranceRoom, FountainRoom, PitRoom, EmptyRoom };

public enum MapSize { Small = 1, Medium, Large };