Game game = new();
game.Play();

class Game
{
    private readonly Player playerA = new();
    private readonly Player playerB = new();
    private int draws = 0;

    public void Play()
    {
        bool playing = true;

        System.Console.WriteLine("***Starting the game***\n");
        do
        {
            PlayRound();
            System.Console.Write("Continue playing? (y/n): ");
            playing = PromptToContinue();
        }
        while (playing);

        System.Console.WriteLine("***Ending the game***\n");

        DisplayFinalResult();
    }

    private static bool PromptToContinue()
    {
        while (true)
        {
            string? input = Console.ReadLine();
            if (input != null)
            {
                if (input.Equals("y", StringComparison.CurrentCultureIgnoreCase))
                    return true;
                else if (input.Equals("n", StringComparison.CurrentCultureIgnoreCase))
                    return false;
            }

            System.Console.Write("Invalid input, please enter (y/n): ");
        }
    }

    private void PlayRound()
    {
        PickPlayerSigns();
        int result = CompareSigns();
        DisplayRoundResult(result);
    }

    private void PickPlayerSigns()
    {
        playerA.PickSign();
        playerB.PickSign();
        System.Console.WriteLine($"Player A has chosen {playerA.Sign}");
        System.Console.WriteLine($"Player B has chosen {playerB.Sign}");
    }

    private void DisplayRoundResult(int result)
    {
        switch (result)
        {
            case 1:
                playerA.AddScore();
                System.Console.WriteLine("Player A takes the round!\n");
                break;
            case -1:
                playerB.AddScore();
                System.Console.WriteLine("Player B takes the round!\n");
                break;
            default:
                draws++;
                System.Console.WriteLine("This round is a draw!\n");
                break;
        }
    }

    private int CompareSigns()
    {
        if (playerA.Sign == Sign.Rock)
        {
            if (playerB.Sign == Sign.Scissors)
                return 1;
            else if (playerB.Sign == Sign.Paper)
                return -1;
        }

        if (playerA.Sign == Sign.Paper)
        {
            if (playerB.Sign == Sign.Rock)
                return 1;
            else if (playerB.Sign == Sign.Scissors)
                return -1;
        }

        if (playerA.Sign == Sign.Scissors)
        {
            if (playerB.Sign == Sign.Paper)
                return 1;
            else if (playerB.Sign == Sign.Rock)
                return -1;
        }

        return 0;
    }

    void DisplayFinalResult()
    {
        System.Console.WriteLine($"Player A victories: {playerA.Score}");
        System.Console.WriteLine($"Player B victories: {playerB.Score}");
        System.Console.WriteLine($"Total draws: {draws}");
    }
}

class Player
{
    private int _score = 0;
    private readonly Random _randomSignPicker = new();
    private Sign _currentSign;

    public int Score => _score;

    public void AddScore() { _score++; }

    public Sign Sign => _currentSign;

    public void PickSign()
    {
        int choice = _randomSignPicker.Next(0, 3);
        _currentSign = (Sign)choice;
    }
};

enum Sign { Rock, Paper, Scissors };