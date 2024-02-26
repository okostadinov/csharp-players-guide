namespace FinalBattle;

public class Character(string name)
{
    public string Name { get; } = name;

    public bool IsPlayer { get; private set; } = false;
    public bool IsTurn { get; set; } = false;

    public void SetPlayerCharacter() => IsPlayer = true;

    public Action PromptAction()
    {
        Console.Write($"It's {Name}'s turn. What to do ('a' to attack, SPACE to skip turn)? ");

        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            Action action = key switch
            {
                ConsoleKey.Spacebar => Action.Skip,
                ConsoleKey.A => Action.Attack,
                _ => Action.Invalid,
            };

            if (action == Action.Invalid) new InvalidCommand().Execute();
            else return action;
        }
    }

    public void ExecuteAction(Action action)
    {
        switch (action)
        {
            case Action.Attack:
                new AttackCommand(this).Execute();
                break;
            case Action.Skip:
                new SkipCommand(this).Execute();
                break;
            default:
                break;
        }

        Console.WriteLine();
    }
}
