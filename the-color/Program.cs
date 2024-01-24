Color teal = new(0, 128, 128);
Color red = Color.Red;
System.Console.WriteLine($"Teal:\t{teal.RGB}");
System.Console.WriteLine($"Red:\t{red.RGB}");

class Color
{
    public byte R { get; }
    public byte G { get; }
    public byte B { get; }

    public string RGB => $"({R}, {G}, {B})";

    public static Color White => new(255, 255, 255);
    public static Color Black => new();
    public static Color Red => new(255, 0, 0);
    public static Color Orange => new(255, 165, 0);
    public static Color Yellow => new(255, 255, 0);
    public static Color Green => new(0, 128, 0);
    public static Color Blue => new(0, 0, 255);
    public static Color Purple => new(128, 0, 128);

    public Color()
    {
        R = 0;
        G = 0;
        B = 0;
    }

    public Color(byte red, byte green, byte blue)
    {
        R = red;
        G = green;
        B = blue;
    }
}