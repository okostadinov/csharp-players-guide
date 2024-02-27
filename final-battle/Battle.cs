namespace FinalBattle;

public class Battle
{
    private Random Random { get; } = new();
    private Party PartyA { get; }
    private Party PartyB { get; }
    public Party PartyInTurn { get; private set; }
    private bool IsBattleOver { get; set; } = false;

    public Battle(Party partyA, Party partyB)
    {
        PartyA = partyA;
        PartyB = partyB;
        PartyInTurn = PartyA.ContainsPlayerCharacter() ? PartyA : PartyB;
        PartyA.PartyDeath += HandlePartyDeath;
        PartyB.PartyDeath += HandlePartyDeath;
    }

    public Party GetOppositeParty() => PartyInTurn == PartyA ? PartyB : PartyA;

    public void Start()
    {
        while (!IsBattleOver)
        {
            Round(PartyInTurn);
            PartyInTurn = PartyInTurn == PartyA ? PartyB : PartyA;
        }
    }

    public void Round(Party party)
    {
        switch (party.PartyType)
        {
            case PartyType.Player:
                PlayerMakeTurn(party);
                break;
            case PartyType.Computer:
                ComputerMakeTurn(party);
                break;
            default:
                break;
        }

    }

    private void PlayerMakeTurn(Party party)
    {
        Character? character = party.GetCharacterInTurn();

        if (character != null)
        {
            Command action = character.PromptCommand();
            character.ExecuteCommand(this, action);
            party.UpdateCharacterInTurn();
        }
    }
    private void ComputerMakeTurn(Party party)
    {
        Character? character = party.GetCharacterInTurn();

        if (character != null)
        {
            Thread.Sleep(750);
            int actionsAmount = Enum.GetNames(typeof(Command)).Length;
            if (actionsAmount < 2) return;

            int randomCommandIndex = Random.Next(1, actionsAmount);
            // character.ExecuteCommand(this, (Command)randomCommandIndex);
            character.ExecuteCommand(this, Command.Attack);
            party.UpdateCharacterInTurn();
        }
    }

    private void HandlePartyDeath(Party party)
    {
        if (party.PartyType == PartyType.Computer)
            ColoredText.WriteLine("\nThe Uncoded One's party has been defeated! You are victorious!", ConsoleColor.Green);
        else
            ColoredText.WriteLine("\nYou have been defeated. The Uncoded One reigns supreme...", ConsoleColor.Red);

        IsBattleOver = true;
    }
}