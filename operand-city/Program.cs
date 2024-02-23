BlockCoordinate coordinate = new(1, 2);
BlockOffset offset = new(1, -1);

Console.WriteLine(coordinate + offset);
Console.WriteLine(offset + coordinate);

Console.WriteLine(coordinate + Direction.East);
Console.WriteLine(Direction.South + coordinate);

Console.WriteLine(coordinate[0]);
Console.WriteLine(coordinate[1]);

Console.WriteLine(offset + Direction.North);
Console.WriteLine(Direction.West + offset);

public record BlockCoordinate(int Row, int Column)
{
    public int this[int index]
    {
        get
        {
            if (index == 0) return Row;
            else return Column;
        }
    }

    public static BlockCoordinate operator +(BlockCoordinate coordinate, BlockOffset offset) =>
        new BlockCoordinate(coordinate.Row + offset.RowOffset, coordinate.Column + offset.ColumnOffset);

    public static BlockCoordinate operator +(BlockOffset offset, BlockCoordinate coordinate) =>
        new BlockCoordinate(coordinate + offset);

    public static BlockCoordinate operator +(BlockCoordinate coordinate, Direction direction)
    {
        return direction switch
        {
            Direction.North => new BlockCoordinate(coordinate.Row - 1, coordinate.Column),
            Direction.South => new BlockCoordinate(coordinate.Row + 1, coordinate.Column),
            Direction.East => new BlockCoordinate(coordinate.Row, coordinate.Column + 1),
            Direction.West => new BlockCoordinate(coordinate.Row - 1, coordinate.Column - 1),
            _ => coordinate
        };
    }

    public static BlockCoordinate operator +(Direction direction, BlockCoordinate coordinate) =>
        new BlockCoordinate(coordinate + direction);
}

public record BlockOffset(int RowOffset, int ColumnOffset)
{
    public static BlockOffset operator +(BlockOffset offset, Direction direction)
    {
        return direction switch
        {
            Direction.North => new BlockOffset(offset.RowOffset - 1, offset.ColumnOffset),
            Direction.South => new BlockOffset(offset.RowOffset + 1, offset.ColumnOffset),
            Direction.East => new BlockOffset(offset.RowOffset, offset.ColumnOffset + 1),
            Direction.West => new BlockOffset(offset.RowOffset, offset.ColumnOffset - 1),
            _ => offset
        };
    }

    public static BlockOffset operator +(Direction direction, BlockOffset offset) =>
        new BlockOffset(offset + direction);
}
public enum Direction { North, East, South, West }

