// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Csharp.Utils
{
    public static class JsonOptions
    {
        public static JsonSerializerOptions Default => new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter() },
            WriteIndented = false
        };
    }
}