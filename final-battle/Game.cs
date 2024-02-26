using System.Configuration.Assemblies;

namespace FinalBattle;

public class Game
{
    public Character Player { get; }

    // public List<Party> Parties = [];

    public Game()
    {
        Player = GeneratePlayerCharacter();
        Player.SetPlayerCharacter();
    }
    private static Character GeneratePlayerCharacter()
    {
        Console.Write("Enter a name for your character: ");

        while (true)
        {
            string? input = Console.ReadLine();
            if (input != null && input != "")
                return new Character(input);
            else
                Console.Write("Invalid input. Please try again: ");
        }
    }

    public static void Battle(Party a, Party b)
    {


        while (true)
        {

        }
    }
    // public void AddParty(Party party) => Parties.Add(party);
}