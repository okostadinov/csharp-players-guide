namespace FinalBattle;

public abstract class Character(int hp)
{
    public abstract string Name { get; }
    public bool IsTurn { get; set; } = false;
    public abstract IAttack Attack { get; }
    private int _hp = hp;
    public int HP
    {
        get => _hp;
        set => _hp = Math.Clamp(value, 0, MaxHP);
    }
    public int MaxHP { get; } = hp;
    public event Action<Character>? CharacterDeath;
    public Gear? Gear { get; set; }

    public Command PromptCommand()
    {
        DisplayMenu();

        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;

            Command command = key switch
            {
                ConsoleKey.Spacebar => Command.Skip,
                ConsoleKey.A => Command.Attack,
                ConsoleKey.S => Gear != null ? Command.SpecialAttack : Command.Invalid,
                ConsoleKey.I => Command.ViewInventory,
                _ => Command.Invalid,
            };

            if (command == Command.Invalid) new InvalidCommand().Execute();
            else return command;
        }
    }

    private void DisplayMenu()
    {
        ColoredText.WriteLine($"It's {Name}'s turn...");
        ColoredText.WriteLine($"'a'\t- standard attack ({Attack.Name})");
        if (Gear != null)
            ColoredText.WriteLine($"'s'\t- special attack ({Gear.Attack.Name})");
        ColoredText.WriteLine($"'i'\t- check inventory");
        ColoredText.WriteLine($"SPACE\t- skip turn");
        ColoredText.WriteLine($"What to do? ");
    }

    public void ExecuteCommand(Battle battle, Command action)
    {
        switch (action)
        {
            case Command.Attack:
                PerformAttack(battle);
                break;
            case Command.SpecialAttack:
                PerformAttack(battle, true);
                break;
            case Command.ViewInventory:
                new ViewInventoryCommand(battle).Execute();
                break;
            case Command.Skip:
                new SkipCommand(this).Execute();
                break;
            default:
                break;
        }

        Console.WriteLine();
    }

    public void PerformAttack(Battle battle, bool special = false)
    {
        Party oppositeParty = battle.GetOppositeParty();
        Character target = oppositeParty.GetRandomCharacter();
        IAttack attack = special && Gear != null ? Gear.Attack : Attack;
        AttackData attackData = attack.Generate();

        ConsoleColor color = oppositeParty.PartyType == PartyType.Enemy ? ConsoleColor.Green : ConsoleColor.Red;

        if (attackData.Success)
        {
            ColoredText.WriteLine($"{Name} used {attack.Name} on {target.Name}!", color);
            ColoredText.WriteLine($"{Name} dealt {attackData.Damage} damage to {target.Name}!", color);
            target.TakeDamage(attackData.Damage);
        }
        else
        {
            color = color == ConsoleColor.Green ? ConsoleColor.Red : ConsoleColor.Green;
            ColoredText.WriteLine($"{Name} missed {target.Name} using {attack.Name}!", color);
        }
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
    public override IAttack Attack => new Punch();

    public TrueProgrammer(string name) : base(25)
    {
        Name = name;
        Gear = new Sword();
    }
}

public class Skeleton : Character
{
    public override string Name => "SKELETON";
    public override IAttack Attack => new BoneCrunch();

    public Skeleton(bool startWithDagger = false) : base(5)
    {
        if (startWithDagger) Gear = new Dagger();
    }
}

public class UncodedOne : Character
{
    public override string Name => "The Uncoded One";
    public override IAttack Attack => new Unraveling();

    public UncodedOne() : base(40) { }
}

public class VinFletcher : Character
{
    public override string Name => "Vin Fletcher";
    public override IAttack Attack => new Punch();

    public VinFletcher() : base(15)
    {
        Gear = new Bow();
    }
}