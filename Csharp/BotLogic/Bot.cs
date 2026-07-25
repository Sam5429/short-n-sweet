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
    public const string TOKEN = "BOTB-YTkR-hUVS-BRFB";

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

        // string resourceName = "maple_syrup";

        // // return new MoveAction(Moving.GoTo(bot.Position, state.Base.Position, state.VisibleTiles));
        // Position target = new Position(191, 287);

        // // // return new SendCompanionAction();

        // // Position target = Moving.GoToResource(bot.Position, resourceName);
        // // return new MoveAction(new Position(bot.Position.X + 1, bot.Position.Y));
        // Console.WriteLine($"{target}");
        // Console.WriteLine($"{bot.Position}");
        // if (Moving.ManhattanDist(bot.Position, target) > 1)
        // {
        //     target = Moving.GoTo(bot.Position, target, state.VisibleTiles);
        //     return new MoveAction(target);
        // }
        // // else if (bot.Inventory[1].Quantity == 100)
        // // {
        // //     return new SendCompanionAction();
        // // }
        // else
        // {
        //     return new GatherNodeAction(target);
        //     return new PlaceExtractorAction(target);
        // }



        // return new SendCompanionAction();
        // Position target = new Position(59, 188);
        Position target = new Position(bot.Position.X-100 , bot.Position.Y);
        // return new MoveAction(target);
        if (bot.Health < 50)
        {
            return new RespawnAction();
        }


        var visibleStructures = state.VisibleStructures.OrderBy(s => s.Hp).ToList();
        foreach (var structure in visibleStructures)
        {
            if (structure.PvpActivated && !structure.IsAlly)
            {
                Console.WriteLine($"{structure.Position}");
                target = structure.Position;
                if (Moving.ManhattanDist(bot.Position, target) > 1)
                {
                    target = Moving.GoTo(bot.Position, target, state.VisibleTiles, state.VisibleResources);
                    return new MoveAction(target);
                }
                else
                {
                    return new DestroyStructureAction(target);
                }
            }
        }

        var visiblePlayers = state.VisiblePlayers.OrderBy(s => s.Health).ToList();
        foreach (var player in state.VisiblePlayers)
        {
            if (player.PvpActivated && !player.IsAlly)
            {
                target = player.Position;
                Console.WriteLine($"{target}");
                if (Moving.ManhattanDist(bot.Position, target) > 1)
                {
                    target = Moving.GoTo(bot.Position, target, state.VisibleTiles, state.VisibleResources);
                    return new MoveAction(target);
                }
                else
                {
                    return new AttackAction(target);
                }
            }
        }

        target = Moving.GoTo(bot.Position, target, state.VisibleTiles, state.VisibleResources);
        return new MoveAction(target);

    }
}
