Console.Write("Choose a filter (even/positive/multiple): ");
string? input = Console.ReadLine();

Func<int, bool> filter = input switch
{
    "even" => n => n % 2 == 0,
    "positive" => n => n > 0,
    "multiple" => n => n % 10 == 0,
    _ => n => false
};

Sieve sieve = new(filter);

while (true)
{
    Console.Write("Try the filter: ");
    input = Console.ReadLine();

    if (int.TryParse(input, out int number))
    {
        string result = sieve.IsGood(number) ? "good" : "bad";
        Console.WriteLine($"The number is {result}!");
    }
    else
    {
        Console.WriteLine("Invalid input. Try again.");
    }
}

public class Sieve(Func<int, bool> operation)
{
    private readonly Func<int, bool> operation = operation;
    public bool IsGood(int number)
    {
        return operation(number);
    }
}

