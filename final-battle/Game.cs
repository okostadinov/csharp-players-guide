namespace FinalBattle;

public static class Game
{
    public static TrueProgrammer GeneratePlayerCharacter()
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
}
