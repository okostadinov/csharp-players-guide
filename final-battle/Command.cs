namespace FinalBattle;

public interface ICommand
{
    public void Execute();
}

public class InvalidCommand : ICommand
{
    public void Execute() => ColoredText.Write("\nNo such command. Please try again:", ConsoleColor.Yellow);
}

public class SkipCommand(Character character) : ICommand
{
    public void Execute() => ColoredText.WriteLine($"{character.Name} is skipping the turn..");
}

public interface IAttack
{
    public int Damage { get; }
    public string Name { get; }
}

public class Punch : IAttack
{
    public int Damage => 1;
    public string Name => "PUNCH";
}

public class BoneCrunch : IAttack
{
    private static readonly Random _random = new();
    public int Damage => _random.Next(2);
    public string Name => "BONE CRUNCH";
}

public enum Command { Invalid, Skip, Attack }