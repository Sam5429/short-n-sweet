using Csharp.BotLogic;
using Csharp.Client;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Csharp.Moving;

public class Bot : IBot
{
    // EDIT THIS FOR YOUR OWN BOT TOKEN
    public const string TOKEN = "BOTA-YTkR-hUVS-BRFB";

    public ActionBase? GetNextAction(GameState state)
    {
        // foreach (var resources in state.VisibleResources)
        // {
        //     Console.WriteLine($"{resources.Position}");
        // }
        PlayerInfo bot = state.Bot;
        Resource target = state.VisibleResources
            .Where(r => r.CurrentAmount > 0)
            .OrderBy(r => Moving.ManhattanDist(bot.Position, r.Position))
            .FirstOrDefault();

        // return new DestroyStructureAction(new Position(213, 287));
        // return new PlacePumpAction(new Position(213, 290));
        // return new PlaceRadarAction(new Position(state.Bot.Position.X + 1, state.Bot.Position.Y));
        // return new MoveAction(new Position(state.Bot.Position.X, state.Bot.Position.Y + 1));
        Console.WriteLine($"Bot Position: {state.Base.Position}");
        Position new_pp = Moving.GoTo(bot.Position, new Position(163, 323), state.VisibleTiles);
        return new MoveAction(new_pp);

        // // foreach (KeyValuePair<(int, int), Tile> pair in state.VisibleTiles)
        // // {
        // //     Tile tile = pair.Value;
        // //     if (tile.TerrainCategory == "Liquid")
        // //         Console.WriteLine($"{tile.TerrainCategory}");
        // // }

        if (Moving.ManhattanDist(bot.Position, target.Position) > 1)
        {
            Console.WriteLine($"move");
            // return new MoveAction(new Position(state.Bot.Position.X + 1, state.Bot.Position.Y));
            Position new_p = Moving.GoTo(bot.Position, target.Position, state.VisibleTiles);
            return new MoveAction(new_p);
        }
        else if (bot.Inventory.Any(item => item.Quantity >= 50))
        {
            Console.WriteLine($"Companion");
            return new SendCompanionAction();
        }
        else
        {
            Console.WriteLine($"Gathering");
            return new GatherNodeAction(target.Position);
        }
    }
}
