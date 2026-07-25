using Csharp.Client;

namespace Csharp.Moving;

public class Moving
{
    public static int ManhattanDist(Position p1, Position p2)
    {
        return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
    }

    public static Position GoTo(Position p1, Position p2)
    {
        if (p1.X == p2.X)
        {
            return p1.Y > p2.Y ? new Position(p1.X, p1.Y - 1) : new Position(p1.X, p1.Y + 1);
        }
        else
        {
            return p1.X > p2.X ? new Position(p1.X - 1, p1.Y) : new Position(p1.X + 1, p1.Y);
        }
    }
}
