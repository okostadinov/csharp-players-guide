namespace FinalBattle;

public class Battle
{
    private GameMode GameMode { get; set; }
    private Random Random { get; } = new();
    private Party PartyA { get; }
    private Party PartyB { get; }
    public Party PartyInTurn { get; private set; }
    private bool IsBattleOver { get; set; } = false;
    public event Action<Battle>? HeroPartyDeath;

    public Battle(Party partyA, Party partyB)
    {
        PartyA = partyA;
        PartyB = partyB;
        PartyInTurn = PartyA;
        PartyA.PartyDeath += HandlePartyDeath;
        PartyB.PartyDeath += HandlePartyDeath;
    }

    public Party GetOppositeParty() => PartyInTurn == PartyA ? PartyB : PartyA;

    public void Start(GameMode gameMode)
    {
        GameMode = gameMode;
        ColoredText.WriteLine("*** A new battle is starting! ***\n", ConsoleColor.Magenta);

        while (!IsBattleOver)
        {
            Round(PartyInTurn);
            PartyInTurn = GetOppositeParty();
        }
    }

    public void Round(Party party)
    {
        switch (GameMode)
        {
            case GameMode.PlayerVsComputer:
                if (party.PartyType == PartyType.Hero)
                    PlayerMakeTurn(party);
                else
                    ComputerMakeTurn(party);
                break;
            case GameMode.ComputerVsComputer:
                ComputerMakeTurn(party);
                break;
            case GameMode.PlayerVsPlayer:
                PlayerMakeTurn(party);
                break;
            default:
                break;
        }
    }

    private void PlayerMakeTurn(Party party)
    {
        Character character = party.GetCharacterInTurn();
        Command action = character.PromptCommand();
        character.ExecuteCommand(this, action);
        party.UpdateCharacterInTurn();
    }

    private void ComputerMakeTurn(Party party)
    {
        Thread.Sleep(750);
        Character character = party.GetCharacterInTurn();
        int actionsAmount = Enum.GetNames(typeof(Command)).Length;
        if (actionsAmount < 2) return;

        int randomNumber = Random.Next(1, 101);

        if (character.Gear != null)
        {
            if (randomNumber < 26)
                character.ExecuteCommand(this, Command.Skip);
            else if (randomNumber < 51)
                character.ExecuteCommand(this, Command.Attack);
            else
                character.ExecuteCommand(this, Command.SpecialAttack);
        }
        else
        {
            if (party.Inventory.Count > 0)
            {
                if (randomNumber > 50)
                    new EquipCommand(party, party.Inventory[Random.Next(party.Inventory.Count)]).Execute();
                else
                    character.ExecuteCommand(this, Command.Attack);
            }
            else
            {
                character.ExecuteCommand(this, Command.Attack);
            }
        }

        party.UpdateCharacterInTurn();
    }

    private void HandlePartyDeath(Party party)
    {
        if (party.PartyType == PartyType.Enemy)
            ColoredText.WriteLine("\nThe enemy party has been defeat!", ConsoleColor.Green);
        else
            HeroPartyDeath?.Invoke(this);

        party.PartyDeath -= HandlePartyDeath;
        IsBattleOver = true;
    }
}