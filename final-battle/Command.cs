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

public class ViewInventoryCommand(Battle battle) : ICommand
{
    private Party Party { get; } = battle.PartyInTurn;
    public void Execute()
    {
        if (Party.Inventory.Count == 0)
        {
            ColoredText.WriteLine("The intentory is empty...\n");
            new BackCommand(battle).Execute();
        }
        else
        {
            ColoredText.WriteLine("Current available gear:");

            for (int i = 0; i < Party.Inventory.Count; i++)
                ColoredText.WriteLine($"  {i + 1}\t- {Party.Inventory[i].Name}");

            ColoredText.WriteLine("  'esc'\t- go back\n");

            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Escape)
                {
                    new BackCommand(battle).Execute();
                    break;
                };

                try
                {
                    Gear gear = Party.Inventory[(int)key - 49];
                    new EquipCommand(Party, gear).Execute();
                    break;
                }
                catch (ArgumentOutOfRangeException)
                {
                    ColoredText.WriteLine("No such item. Try again.", ConsoleColor.Yellow);
                }
            }
        }
    }
}

public class EquipCommand(Party party, Gear gear) : ICommand
{
    public void Execute()
    {
        Character character = party.GetCharacterInTurn();
        if (character.Gear != null)
        {
            party.Inventory.Add(character.Gear);
        }

        character.Gear = gear;
        party.Inventory.Remove(gear);
        ColoredText.WriteLine($"{character.Name} has equipped {gear.Name}");
    }
}

public class BackCommand(Battle battle) : ICommand
{
    public void Execute() => battle.Round(battle.PartyInTurn);
}

public interface IAttack
{
    public int Damage { get; }
    public string Name { get; }
    public int SuccessProbability { get; }
    public AttackData Generate() => new(Damage, SuccessProbability);
}

public record AttackData
{
    private readonly Random _random = new();
    public int Damage { get; }
    public bool Success { get; }
    public AttackData(int damage, int successProbability)
    {
        Damage = damage;
        Success = _random.Next(100) < successProbability;
    }
}

public class Punch : IAttack
{
    public int Damage => 1;
    public string Name => "PUNCH";
    public int SuccessProbability => 75;
}

public class BoneCrunch : IAttack
{
    private static readonly Random _random = new();
    public int Damage => _random.Next(2);
    public string Name => "BONE CRUNCH";
    public int SuccessProbability => 75;
}

public class Unraveling : IAttack
{
    private static readonly Random _random = new();
    public int Damage => _random.Next(3);
    public string Name => "UNRAVELING ATTACK";
    public int SuccessProbability => 100;
}

public class Slash : IAttack
{
    public int Damage => 2;
    public string Name => "SLASH";
    public int SuccessProbability => 66;
}

public class Stab : IAttack
{
    public int Damage => 1;
    public string Name => "STAB";
    public int SuccessProbability => 80;
}

public class QuickShot : IAttack
{
    public int Damage => 3;
    public string Name => "QUICKSHOT";
    public int SuccessProbability => 50;
}


public enum Command { Invalid, Skip, Attack, SpecialAttack, ViewInventory }