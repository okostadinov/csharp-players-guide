namespace FinalBattle;

public abstract class Character
{
    public abstract string Name { get; }
    public abstract bool IsPlayer { get; }
    public bool IsTurn { get; set; } = false;
    public abstract IAttack Attack { get; }
    private int _hp;
    public int HP
    {
        get => _hp;
        set => _hp = Math.Clamp(value, 0, MaxHP);
    }
    public int MaxHP { get; }
    public event Action<Character>? CharacterDeath;

    public Character(int hp)
    {
        _hp = hp;
        MaxHP = hp;
    }

    public Command PromptCommand()
    {
        Console.WriteLine($"It's {Name}'s turn. What to do ('a' to attack, SPACE to skip turn)? ");

        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            Command command = key switch
            {
                ConsoleKey.Spacebar => Command.Skip,
                ConsoleKey.A => Command.Attack,
                _ => Command.Invalid,
            };

            if (command == Command.Invalid) new InvalidCommand().Execute();
            else return command;
        }
    }

    public void ExecuteCommand(Battle battle, Command action)
    {
        switch (action)
        {
            case Command.Attack:
                PerformAttack(battle);
                break;
            case Command.Skip:
                new SkipCommand(this).Execute();
                break;
            default:
                break;
        }

        Console.WriteLine();
    }

    public void PerformAttack(Battle battle)
    {
        Character target = battle.GetOppositeParty().GetRandomCharacter();
        int damage = Attack.Damage;
        ConsoleColor color = IsPlayer ? ConsoleColor.Green : target.IsPlayer ? ConsoleColor.Red : ConsoleColor.Gray;
        ColoredText.WriteLine($"{Name} used {Name} on {target.Name}!", color);
        ColoredText.WriteLine($"{Name} dealt {damage} damage to {target.Name}!", color);
        target.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        ColoredText.WriteLine($"{Name} is now at {HP}/{MaxHP} HP.", ConsoleColor.Yellow);
        if (HP == 0) CharacterDeath?.Invoke(this);
    }
}

public class TrueProgrammer : Character
{
    public override string Name { get; }
    public override bool IsPlayer => true;
    public override IAttack Attack => new Punch();

    public TrueProgrammer(string name) : base(25) => Name = name;
}

public class Skeleton : Character
{
    public override string Name => "SKELETON";
    public override bool IsPlayer => false;
    public override IAttack Attack => new BoneCrunch();

    public Skeleton() : base(5) { }
}