while (true)
{
    Console.Write("Enter a word to recreate: ");
    string? input = Console.ReadLine();
    HandleInput(input);
}

async Task HandleInput(string? input)
{
    DateTime start = DateTime.Now;
    int attempts = await RandomlyRecreate(input);
    TimeSpan duration = DateTime.Now - start;

    Console.WriteLine($"\nRecreated '{input}' in {attempts} total attempts taking {duration.Seconds}s{duration.Milliseconds}ms.");
}

Task<int> RandomlyRecreate(string? word)
{
    return Task.Run(() =>
    {
        if (word == null) return 0;

        Random random = new();
        int length = word.Length;
        int attempts = 0;
        string generatedWord = "";

        while (true)
        {
            attempts++;

            for (int i = 0; i < length; i++)
            {
                char randomLetter = (char)('a' + random.Next(26));
                generatedWord += randomLetter;
            }

            if (generatedWord == word) break;
            else generatedWord = "";
        }

        return attempts;
    });
}