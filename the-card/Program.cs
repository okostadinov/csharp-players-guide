var deck = Card.CreateDeck();
foreach (Card card in deck) {
    System.Console.WriteLine($"The {card.Value}");
}

class Card(Color color, Rank rank)
{
    private readonly Color Color = color;
    private readonly Rank Rank = rank;

    public string Value => $"{Color} {Rank}";

    public static List<Card> CreateDeck()
    {
        List<Card> deck = [];
        foreach (Color color in Enum.GetValues(typeof(Color)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                Card card = new(color, rank);
                deck.Add(card);
            }
        }

        return deck;
    }
}

enum Color { Red, Green, Blue, Yellow };

enum Rank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Dollar, Percent, Power, Ampersand };