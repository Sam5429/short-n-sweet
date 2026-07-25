using Csharp.Client;

namespace Csharp.Moving;

public class Moving
{

    private static readonly string LogPath = "bot_log.txt";

    private static List<Resource> ReadLoggedResources()
    {
        var resources = new List<Resource>();

        if (!File.Exists(LogPath))
        {
            return resources;
        }

        var lines = File.ReadAllLines(LogPath);

        foreach (var rawLine in lines)
        {
            if (string.IsNullOrWhiteSpace(rawLine))
            {
                continue;
            }

            var parts = rawLine.Split(',');
            if (parts.Length < 3)
            {
                continue; // ligne mal formée, on l'ignore
            }

            string name = parts[0].Trim();

            if (!int.TryParse(parts[1].Trim(), out int x))
            {
                continue;
            }
            if (!int.TryParse(parts[2].Trim(), out int y))
            {
                continue;
            }

            var resource = new Resource
            {
                Name = name,
                Position = new Position(x, y)
            };

            resources.Add(resource);
        }

        return resources;
    }

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

        bool foundExact = false;
        (int x, int y) bestReachable = (start.X, start.Y);
        double bestDist = double.MaxValue;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            // Distance de cette tuile atteignable par rapport à la cible réelle
            double dist = Math.Sqrt(Math.Pow(current.x - end.X, 2) + Math.Pow(current.y - end.Y, 2));
            if (dist < bestDist)
            {
                bestDist = dist;
                bestReachable = current;
            }

            if (current == (end.X, end.Y))
            {
                foundExact = true;
                break;
            }

            foreach (var (dx, dy) in directions)
            {
                var next = (current.x + dx, current.y + dy);
                if (visited.Contains(next))
                    continue;

                // On ne peut explorer QUE ce qu'on voit ; hors zone visible = frontière, on n'avance pas plus loin
                if (!visibleTiles.TryGetValue(next, out Tile tile))
                    continue;

                if (tile.TerrainCategory == "Liquid" || tile.HasStructure)
                    continue;

                visited.Add(next);
                cameFrom[next] = current;
                queue.Enqueue(next);
            }
        }

        // Rien d'atteignable autour de start (même pas un pas) -> on reste sur place plutôt que de foncer dans un mur
        if (bestReachable == (start.X, start.Y) && !foundExact && cameFrom.Count == 0)
        {
            return start;
        }

        // Cible = soit end (si trouvée), soit la tuile atteignable la plus proche de end
        var target = foundExact ? (end.X, end.Y) : bestReachable;

        if (target == (start.X, start.Y))
        {
            return start;
        }

        var step = target;
        while (cameFrom.ContainsKey(step) && cameFrom[step] != (start.X, start.Y))
        {
            step = cameFrom[step];
        }

        return new Position(step.Item1, step.Item2);
    }

    public static Position GoToResource(Position p, string resourceName) {
        var resources = ReadLoggedResources();

        Position min_target = new Position(0, 0);
        int min_dist = ManhattanDist(p, min_target);
        foreach (var resource in resources)
        {
            if (resource.Name.Equals(resourceName))
            {
                Console.WriteLine($"{resource.Name}");
                Position target = new Position(resource.Position.X, resource.Position.Y);
                int dist = ManhattanDist(p, target);
                if (dist < min_dist)
                {
                    min_dist = dist;
                    min_target = target;
                }
            }
        }
        return min_target;
    }
}
