// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using Csharp.Client;

namespace Csharp.BotLogic
{
    public interface IBot
    {
        public const string TOKEN = "";
        // Appelé à chaque tick de jeu
        ActionBase? GetNextAction(GameState state);
    }
}