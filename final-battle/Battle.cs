namespace FinalBattle;

public class Battle
{
    private Random Random { get; } = new();
    private Party PartyA { get; }
    private Party PartyB { get; }
    public Party PartyInTurn { get; private set; }

    public Battle(Party partyA, Party partyB)
    {
        PartyA = partyA;
        PartyB = partyB;
        PartyInTurn = PartyA.ContainsPlayerCharacter() ? PartyA : PartyB;
    }

    public Party GetOppositeParty() => PartyInTurn == PartyA ? PartyB : PartyA;

    public void Start()
    {
        while (true)
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
            Action action = character.PromptAction();
            character.ExecuteAction(this, action);
            party.UpdateCharacterInTurn();
        }
    }
    private void ComputerMakeTurn(Party party)
    {
        Character? character = party.GetCharacterInTurn();

        if (character != null)
        {
            Thread.Sleep(750);
            int actionsAmount = Enum.GetNames(typeof(Action)).Length;
            if (actionsAmount < 2) return;

            int randomActionIndex = Random.Next(1, actionsAmount);
            character.ExecuteAction(this, (Action)randomActionIndex);
            party.UpdateCharacterInTurn();
        }
    }
}