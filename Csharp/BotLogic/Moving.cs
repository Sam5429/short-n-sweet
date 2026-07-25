using Csharp.Client;

namespace Csharp.Moving;

public class Moving
{
    public static int ManhattanDist(Position p1, Position p2)
    {
        return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
    }

    public static Position GoTo(Position start, Position end, Dictionary<(int, int), Tile> visibleTiles)
    {
        if (start.X == end.X && start.Y == end.Y)
        {
            return start;
        }
        var directions = new (int idx, int idy)[]
        {
            (0, -1), // Up
            (0, 1),  // Down
            (-1, 0), // Left
            (1, 0)   // Right
        };

        var visited = new HashSet<(int, int)> { (start.X, start.Y) };
        var cameFrom = new Dictionary<(int, int), (int, int)>();
        var queue = new Queue<(int x, int y)>();
        queue.Enqueue((start.X, start.Y));

        bool found = false;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current == (end.X, end.Y))
            {
                found = true;
                break;
            }

            foreach (var (dx, dy) in directions)
            {
                var next = (current.Item1 + dx, current.Item2 + dy);

                if (visited.Contains(next))
                    continue;

                if (visibleTiles.TryGetValue(next, out Tile tile) && tile.TerrainCategory == "Liquid")
                    continue;

                visited.Add(next);
                cameFrom[next] = current;
                queue.Enqueue(next);
            }
        }

        if (!found)
        {
            return GoToNaive(start, end);
        }

        var step = (end.X, end.Y);
        while (cameFrom.ContainsKey(step) && cameFrom[step] != (start.X, start.Y))
        {
            step = cameFrom[step];
        }

        return new Position(step.Item1, step.Item2);
    }
    private static Position GoToNaive(Position p1, Position p2)
    {
        if (p1.X == p2.X)
            return p1.Y > p2.Y ? new Position(p1.X, p1.Y - 1) : new Position(p1.X, p1.Y + 1);
        else
            return p1.X > p2.X ? new Position(p1.X - 1, p1.Y) : new Position(p1.X + 1, p1.Y);
    }

}