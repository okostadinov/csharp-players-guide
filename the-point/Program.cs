Point first = new Point(2, 3);
Point second = new Point(-4, 0);
Point third = new Point();
System.Console.WriteLine($"First Point:\t{first.Coordinates}");
System.Console.WriteLine($"Second Point:\t{second.Coordinates}");
System.Console.WriteLine($"Third Point:\t{third.Coordinates}");

class Point
{
    public int X { get; }
    public int Y { get; }

    public string Coordinates => $"({X}, {Y})";
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point()
    {
        X = 0;
        Y = 0;
    }
}