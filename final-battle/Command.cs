namespace FinalBattle;

public interface ICommand
{
    public void Execute();
}

public interface IAttack
{
    public int Damage { get; }
    public string Name { get; }
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
    public int Damage => 1;
    public string Name => "PUNCH";
    public void Execute(Character character, Character target)
    {
        Console.WriteLine($"{character.Name} used {Name} on {target.Name}!");
        Console.WriteLine($"{Name} dealt {Damage} damage to {target.Name}!");
        target.TakeDamage(Damage);
    }
}

public class BoneCrunch : IAttack
{
    private Random Random { get; } = new();
    public int Damage => Random.Next(2);
    public string Name => "BONE CRUNCH";
    public void Execute(Character character, Character target)
    {
        int damage = Damage;
        Console.WriteLine($"{character.Name} used {Name} on {target.Name}!");
        Console.WriteLine($"{Name} dealt {damage} damage to {target.Name}!");
        target.TakeDamage(damage);
    }
}

public enum Command { Invalid, Skip, Attack }