namespace FinalBattle;

public interface ICommand {
    public void Execute();
}

public class InvalidCommand : ICommand {
    public void Execute() => Console.Write("\nNo such command. Please try again:");
}

public class SkipCommand(Character character) : ICommand {
    public void Execute() => Console.WriteLine($"\n{character.Name} is skipping the turn..");
}

public class AttackCommand(Character character) : ICommand {
    public void Execute() => Console.WriteLine($"\n{character.Name} is attacking!");
}

public enum Action { Invalid, Skip, Attack }