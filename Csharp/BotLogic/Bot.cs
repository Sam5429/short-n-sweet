using Csharp.BotLogic;
using Csharp.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Csharp.Moving;
using System.IO;

public class Bot : IBot
{
    // EDIT THIS FOR YOUR OWN BOT TOKEN
    public const string TOKEN = "BOTA-YTkR-hUVS-BRFB";

    private static readonly string LogPath = "bot_log.txt";

    private static void LogResource(Resource r)
    {
        string line = $"{r.Name}, {r.Position.X}, {r.Position.Y}";

        if (File.Exists(LogPath))
        {
            var existingLines = File.ReadAllLines(LogPath);
            foreach (var existing in existingLines)
            {
                var parts = existing.Split(',');
                if (parts.Length >= 3
                    && int.TryParse(parts[1].Trim(), out int x)
                    && int.TryParse(parts[2].Trim(), out int y)
                    && x == r.Position.X
                    && y == r.Position.Y)
                {
                    return; // déjà loguée
                }
            }
        }

        File.AppendAllText(LogPath, line + Environment.NewLine);
    }

    public ActionBase? GetNextAction(GameState state)
    {
        // foreach (var resources in state.VisibleResources)
        // {
        //     Console.WriteLine($"{resources.Position}");
        // }
        PlayerInfo bot = state.Bot;
        //Resource target = state.VisibleResources.FirstOrDefault(r => r.CurrentAmount > 0);
        // BaseInfo target = state.Base;

        // Console.WriteLine($"{bot.Position}");
        // Console.WriteLine($"{target.Position}");
        // regarde si chaque ressource trouver a un radar proche
        foreach (Resource resource in state.VisibleResources)
        {
            LogResource(resource);
        }

        string resourceName = "mint";


        Position target = Moving.GoToResource(bot.Position, resourceName);
        // return new RespawnAction();
        // return new PlacePumpAction(target);
        // Position target = state.Base.Position;
        Console.WriteLine($"{bot.Position}");
        Console.WriteLine($"{target}");
        // return new SendCompanionAction();
        // return new MoveAction(new Position(bot.Position.X, bot.Position.Y - 1));
        if (Moving.ManhattanDist(bot.Position, target) > 1)
        {
            // return new GatherNodeAction(target);
            target = Moving.GoTo(bot.Position, target);
            return new MoveAction(target);
        }
        else if (bot.Inventory.Any(item => item.Quantity >= 30))
        {
            return new SendCompanionAction();
        }
        else
        {
            return new GatherNodeAction(target);
            return new PlaceExtractorAction(target);
        }

        return new MoveAction(target);
    }
}