namespace TicTacToe;

class Player(string sign)
{
    public string Sign { get; } = sign;

    public int ChooseTile(Board board)
    {
        bool validInput = false;
        int index = 0;
        Console.WriteLine($"Player {Sign}'s turn.");

        do
        {
            Console.Write($"Please choose a tile (1-9): ");
            string? input = Console.ReadLine();
            if (input != null)
            {
                if (int.TryParse(input, out index))
                {
                    if (index > 0 && index < 10)
                    {
                        if (board.CheckAvailableTile(index))
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Tile already occupied. Try again.");
                            continue;
                        }
                    }
                }
            }

            if (!validInput) Console.WriteLine("Invalid tile index. Try again.");
        } while (!validInput);

        return index - 1;
    }
}
