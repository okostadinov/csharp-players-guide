int score = 0;
Console.Write("Please enter your name: ");
string? name = Console.ReadLine();

string filename = name + ".txt";
if (File.Exists(filename)) {
    if (int.TryParse(File.ReadAllText(filename), out int oldScore)) {
        score += oldScore;
    }
}

Console.WriteLine($"Current score: {score}. You can start typing.");
while (Console.ReadKey().Key != ConsoleKey.Enter) {
    score++;
    Console.WriteLine(score);
}

Console.WriteLine($"Final score: {score}.");
File.WriteAllText(filename, score.ToString());