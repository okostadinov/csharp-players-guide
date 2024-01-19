int manticoreHealth = 10;
int cityHealth = 15;
int round = 1;
int manticoreDistanceFromCity = 0;
int expectedCannonDamage = 1;

System.Console.Write("Player 1, how far away from the city do you want to station the manticore? ");
manticoreDistanceFromCity = GetDistance();

Console.Clear();
System.Console.WriteLine("Player 2, it is your turn.");

do
{
    System.Console.WriteLine("------------------------------");
    CalculateCannonDamage();
    DisplayStatus();
    FireCannon();
    UpdateStates();
} while (manticoreHealth > 0 && cityHealth > 0);

DisplayResult();

void DisplayStatus()
{
    System.Console.WriteLine($"STATUS: Round: {round}  City: {cityHealth}/15  Manticore: {manticoreHealth}/10");
    System.Console.WriteLine($"The cannon is expected to deal {expectedCannonDamage} damage this round.");
}

void CalculateCannonDamage()
{
    if (round % 3 == 0 && round % 5 == 0)
        expectedCannonDamage = 10;
    else if (round % 3 == 0 || round % 5 == 0)
        expectedCannonDamage = 3;
    else
        expectedCannonDamage = 1;
}

int GetDistance()
{
    bool validInput = false;

    while (!validInput)
    {
        string? input = Console.ReadLine();
        if (input != null)
        {
            validInput = int.TryParse(input, out int distance);

            if (validInput && distance > 0 && distance <= 100)
                return distance;
            else
                validInput = false;
        }

        if (!validInput)
            System.Console.Write("Invalid input: Please enter a number between 0 and 100: ");
    }

    return 0;
}

void FireCannon()
{
    System.Console.Write("Enter desired cannon range: ");
    int distance = GetDistance();

    if (distance > manticoreDistanceFromCity)
    {
        System.Console.WriteLine("That round OVERSHOT the target.");
    }
    else if (distance < manticoreDistanceFromCity)
    {
        System.Console.WriteLine("That round FELL SHORT of the target.");
    }
    else
    {
        System.Console.WriteLine("That round was a DIRECT HIT!");
        manticoreHealth -= expectedCannonDamage;
    }
}

void UpdateStates()
{
    round++;
    cityHealth--;
}

void DisplayResult()
{
    if (manticoreHealth <= 0)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        System.Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
    }
    else {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        System.Console.WriteLine("The city of Consolas is in ruins.. Tremble before the Manticore!");
    }
}