using Csharp.BotLogic;
using Csharp.Client;
using System;
using System.Linq;
using System.Collections.Generic;

public class Bot : IBot
{
    // EDIT THIS FOR YOUR OWN BOT TOKEN
    public const string TOKEN = "BOTB-YTkR-hUVS-BRFB";

    public ActionBase? GetNextAction(GameState state)
    {
        var nodePos = state.Bot.Position;
        return new MoveAction(new Position(nodePos.X, nodePos.Y + 1));
        // return new PlaceRadarAction(new Position(nodePos.X, nodePos.Y - 1));
        // return new RespawnAction();
        foreach (KeyValuePair<(int, int), Tile> pair in state.VisibleTiles)
        {
            if (pair.Value.Position == nodePos)
            {
                Console.WriteLine($"Tile at {nodePos} has resource terrain: {pair.Value.Terrain}");
            }
            // Console.WriteLine($"{tile.HasRessource}");
            // var tile = pair.Value;
            // var ressource = null;
            // if (tile.HasResource)
            // {
            //     ressource = tile.Resource;
            // }
            // Console.WriteLine($"{state.VisibleResources}");
        }
        return null;
        // if (state.Bot.Position.X - 1 == state.VisibleTiles)
        //     return new MoveAction(new Position(state.Bot.Position.X - 1, state.Bot.Position.Y));
    }
}