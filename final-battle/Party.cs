namespace FinalBattle;

public class Party(PartyType partyType)
{
    private Random Random { get; } = new();
    public PartyType PartyType { get; } = partyType;
    private readonly List<Character> characters = [];
    public event Action<Party>? PartyDeath;

    public void Add(params Character[] charactersToAdd)
    {
        foreach (Character character in charactersToAdd)
        {
            characters.Add(character);
            character.CharacterDeath += HandleCharacterDeath;
        }

        characters[0].IsTurn = true;
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

    private void HandleCharacterDeath(Character character)
    {
        if (character.IsTurn) UpdateCharacterInTurn();

        ColoredText.WriteLine($"{character.Name} has died and has been removed from the party!", ConsoleColor.Yellow);
        characters.Remove(character);
        character.CharacterDeath -= HandleCharacterDeath;

        if (characters.Count == 0) PartyDeath?.Invoke(this);
    }
}

public enum PartyType { Hero, Enemy };