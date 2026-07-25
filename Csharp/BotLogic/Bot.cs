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
    public const string TOKEN = "BOTB-YTkR-hUVS-BRFB";

    public ActionBase? GetNextAction(GameState state)
    {
        // foreach (var resources in state.VisibleResources)
        // {
        //     Console.WriteLine($"{resources.Position}");
        // }
        PlayerInfo bot = state.Bot;
        Resource target = state.VisibleResources.FirstOrDefault(r => r.CurrentAmount > 0);


        if (Moving.ManhattanDist(bot.Position, target.Position) > 1)
        {
            Position new_p = Moving.GoTo(bot.Position, target.Position);
            return new MoveAction(new_p);
        } else if (bot.Inventory[0].Quantity > 100)
        {
            return new SendCompanionAction();
        } else
        {
            return new GatherNodeAction(target.Position);
        }
    }
}
