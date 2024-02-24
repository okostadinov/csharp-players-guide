RecentNumbers recentNumbers = new();

Thread thread1 = new(GenerateNumberEverySecond);
thread1.Start(recentNumbers);

Thread thread2 = new(ExpectUserInputAndDisplayRecentNumbers);
thread2.Start(recentNumbers);

void GenerateNumberEverySecond(object? obj)
{
    if (obj == null) return;

    RecentNumbers recentNumbers = (RecentNumbers)obj;
    Random random = new();
    int randomNumber;

    while (true)
    {
        randomNumber = random.Next(10);
        recentNumbers.UpdateNumbers(randomNumber);
        Console.WriteLine();
        Console.WriteLine(recentNumbers.Previous + " " + recentNumbers.Last);
        Thread.Sleep(1000);
    }
}

void ExpectUserInputAndDisplayRecentNumbers(object? obj)
{
    if (obj == null) return;

    RecentNumbers recentNumbers = (RecentNumbers)obj;

    while (true)
    {
        Console.ReadKey(true);
        if (recentNumbers.Previous == recentNumbers.Last)
            Console.WriteLine("Same numbers!");
        else
            Console.WriteLine("Different numbers.");
    }
}

public class RecentNumbers
{
    private readonly object _numbersLock = new object();
    private int _last;

    private int _previous;

    public int Last {
        get {
            lock (_numbersLock) {
                return _last;
            }
        }
    }
    public int Previous {
        get {
            lock (_numbersLock) {
                return _previous;
            }
        }
    }
    public void UpdateNumbers(int number)
    {
        lock (_numbersLock) {
            _previous = _last;
            _last = number;
        }
    }
}
