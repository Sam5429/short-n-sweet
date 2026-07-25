// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using Csharp.BotLogic;
using System.Text;

Console.WriteLine("Starting JDIS Bot Client...");

string URL_REMOTE = "https://jg26.jdis.ca";

// Main loop listening for ticks.
await BotRunner.RunAsync(URL_LOCAL, Bot.TOKEN);
