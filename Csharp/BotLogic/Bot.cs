using Csharp.BotLogic;
using Csharp.Client;
using System;
using System.Linq;
using System.Collections.Generic;

public class Bot : IBot
{
    // EDIT THIS FOR YOUR OWN BOT TOKEN
    public const string TOKEN = "BOTA-abcd-1234-ABCD";
    
    public ActionBase? GetNextAction(GameState state)
    {   
        return null; // Placeholder for the bot's logic implementation
    }
}