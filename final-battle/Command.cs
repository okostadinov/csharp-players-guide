namespace FinalBattle;

public interface ICommand
{
    public void Execute();
}

public interface IAttack
{
    string Name { get; }
    public void Execute(Character character, Character target);
}

public class InvalidCommand : ICommand
{
    public void Execute() => Console.Write("\nNo such command. Please try again:");
}

public class SkipCommand(Character character) : ICommand
{
    public void Execute() => Console.WriteLine($"{character.Name} is skipping the turn..");
}

// public abstract class AttackCommand() : IAttack
// {
//     public string Name { get; set; }
//     public void Execute(Character character, Character target) => Console.WriteLine($"\n{character.Name} used {Name} on {target.Name}!");
// }

public class Punch : IAttack
{
    public string Name => "PUNCH";
    public void Execute(Character character, Character target) => Console.WriteLine($"{character.Name} used {Name} on {target.Name}!");
}

public class BoneCrunch : IAttack
{
    public string Name => "BONE CRUNCH";
    public void Execute(Character character, Character target) => Console.WriteLine($"{character.Name} used {Name} on {target.Name}!");
}

public enum Action { Invalid, Skip, Attack }