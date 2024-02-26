namespace FinalBattle;

public class Party(PartyType partyType)
{
    private Random Random { get; } = new();
    public PartyType PartyType { get; } = partyType;
    private readonly List<Character> characters = [];

    public void Add(Character character)
    {
        characters.Add(character);
        if (characters[0] == character) character.IsTurn = true;
    }

    public bool ContainsPlayerCharacter() => characters.Any(c => c.IsPlayer);

    public Character? GetCharacterInTurn() => characters.Find(c => c.IsTurn);

    public void UpdateCharacterInTurn()
    {
        int current = characters.FindIndex(c => c.IsTurn);
        characters[current].IsTurn = false;

        if (characters[^1] == characters[current])
            characters[0].IsTurn = true;
        else
            characters[current + 1].IsTurn = true;
    }

    public Character GetRandomCharacter() => characters[Random.Next(characters.Count)];
}

public enum PartyType { Player, Computer };