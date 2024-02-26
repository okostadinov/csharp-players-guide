namespace FinalBattle;

public class Battle(Party partyA, Party partyB)
{
    public Party PartyA { get; } = partyA;
    public Party PartyB { get; } = partyB;

    public void Start()
    {
        Party partyInTurn = PartyA.ContainsPlayerCharacter() ? PartyA : PartyB;

        while (true)
        {
            Round(partyInTurn);
            partyInTurn = partyInTurn == PartyA ? PartyB : PartyA;
        }
    }

    public static void Round(Party party)
    {
        Character? character = party.GetCharacterInTurn();
        if (character != null)
        {
            Action action = character.PromptAction();
            character.ExecuteAction(action);
            party.UpdateCharacterInTurn();
        }
    }

}