namespace FinalBattle;

public static class ColoredText
{
    public static void Write(string text, ConsoleColor consoleColor = ConsoleColor.Gray)
    {
        Console.ForegroundColor = consoleColor;
        Console.Write(text);
        Console.ResetColor();
    }

    public static void WriteLine(string text, ConsoleColor consoleColor = ConsoleColor.Gray)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
