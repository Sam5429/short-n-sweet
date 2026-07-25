// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using Csharp.Client;

namespace Csharp.BotLogic
{
    public class BotRunner
    {
        public static async Task RunAsync(string serverUrl, string token)
        {
            var bot = BotLoader.LoadBot("BotLogic/Bot.cs");
            if (bot == null)
            {
                Console.WriteLine("[ERROR] Invalid bot. Fix Bot.cs and start again.");
                return;
            }

            Console.WriteLine("[SUCCESS] Bot loaded.");

            var client = new GameClient(serverUrl, token);

            try
            {
                Console.WriteLine($"[INFO] Connecting to server: {serverUrl}");

                bool connected = await client.ConnectAsync();

                if (!connected)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[ERROR] The bot could not authenticate.");
                    Console.ResetColor();
                    return;
                }
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[SUCCESS] Connected and authenticated to the server.");
                Console.ResetColor();
            }
            catch (HttpRequestException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ERROR] Could not connect to the remote server.");
                Console.ResetColor();
                Console.WriteLine($"-> Details: {ex.Message}");
                await Task.Delay(3000);
                return;
            }

            client.BackendListening(async state =>
            {
                await Task.Yield();
                try
                {
                    return bot.GetNextAction(state);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[ERROR] Bot error: {ex.Message}");
                    Console.ResetColor();
                    return null;
                }
            });

            await Task.Delay(-1);
        }
    }
}
