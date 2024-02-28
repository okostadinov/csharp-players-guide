namespace FinalBattle;

public class Game
{
    private GameMode GameMode { get; set; }
    private Party HeroParty { get; }
    private List<Party> EnemyParties { get; } = [];
    private bool IsDeadHeroParty { get; set; } = false;

    public Game()
    {
        GameMode = PickGameMode();

        HeroParty = new(PartyType.Hero);
        HeroParty.Add(GeneratePlayerCharacter());
        HeroParty.Add(new VinFletcher());
        HeroParty.AddGear(new Dagger());

        Party partyA = new(PartyType.Enemy);
        partyA.Add(new Skeleton(true));

        Party partyB = new(PartyType.Enemy);
        partyB.Add(new Skeleton(), new Skeleton());
        partyB.AddGear(new Dagger());

        Party partyC = new(PartyType.Enemy);
        partyC.Add(new UncodedOne());

        EnemyParties.Add(partyA);
        EnemyParties.Add(partyB);
        EnemyParties.Add(partyC);
    }

    public static GameMode PickGameMode()
    {

        ColoredText.WriteLine("Pick a game mode among the following options:");
        ColoredText.WriteLine("1 - Player versus Computer");
        ColoredText.WriteLine("2 - Computer versus Computer");
        ColoredText.WriteLine("3 - Player versus Player");

        while (true)
        {
            string? input = Console.ReadLine();

            if (input != null)
            {
                if (int.TryParse(input, out int option))
                {
                    switch (option)
                    {
                        case 1:
                        case 2:
                        case 3:
                            return (GameMode)option;
                        default:
                            break;
                    }
                }
            }

            ColoredText.WriteLine("Invalid option. Please try again.", ConsoleColor.Yellow);
        }
    }

    public void Start()
    {
        foreach (Party enemyParty in EnemyParties)
        {
            if (!IsDeadHeroParty)
            {
                Battle battle = new(HeroParty, enemyParty);
                battle.HeroPartyDeath += HandleHeroPartyDeath;
                battle.Start(GameMode);
                battle.HeroPartyDeath -= HandleHeroPartyDeath;
            }
            else
            {
                break;
            }

        }

        if (!IsDeadHeroParty)
            ColoredText.WriteLine("The armies of the Uncoded One have been destroyed! You are victorous!", ConsoleColor.Cyan);
    }

    private static TrueProgrammer GeneratePlayerCharacter()
    {
        Console.Write("Enter a name for your character: ");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input != null && input != "")
                return new TrueProgrammer(input);
            else
                Console.Write("Invalid input. Please try again: ");
        }
    }

    private void HandleHeroPartyDeath(Battle battle)
    {
        battle.HeroPartyDeath -= HandleHeroPartyDeath;
        ColoredText.WriteLine("\nYou have been defeated. The Uncoded One reigns supreme...", ConsoleColor.Red);
        IsDeadHeroParty = true;
    }
}

public enum GameMode { Invalid, PlayerVsComputer, ComputerVsComputer, PlayerVsPlayer };