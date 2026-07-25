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
