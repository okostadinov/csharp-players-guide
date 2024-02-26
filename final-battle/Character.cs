namespace FinalBattle;

public abstract class Character
{
    public abstract string Name { get; }
    public abstract bool IsPlayer { get; }
    public bool IsTurn { get; set; } = false;
    public abstract IAttack Attack { get; }

    public Action PromptAction()
    {
        Console.WriteLine($"It's {Name}'s turn. What to do ('a' to attack, SPACE to skip turn)? ");

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

    public void ExecuteAction(Battle battle, Action action)
    {
        switch (action)
        {
            case Action.Attack:
                Attack.Execute(this, battle.GetOppositeParty().GetRandomCharacter());
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

public class TrueProgrammer(string name) : Character
{
    public override string Name => name;
    public override bool IsPlayer => true;
    public override IAttack Attack => new Punch();
}

public class Skeleton : Character
{
    public override string Name => "SKELETON";
    public override bool IsPlayer => false;
    public override IAttack Attack => new BoneCrunch();
}