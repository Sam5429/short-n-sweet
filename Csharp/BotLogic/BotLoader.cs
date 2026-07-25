// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Csharp.BotLogic
{
    public static class BotLoader
    {
        public static IBot? LoadBot(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"[ERROR] File not found: {path}");
                return null;
            }

            string code = File.ReadAllText(path);

            var refs = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && !string.IsNullOrEmpty(a.Location))
                .Select(a => MetadataReference.CreateFromFile(a.Location))
                .ToList();

            var compilation = CSharpCompilation.Create(
                "BotAssembly",
                new[] { CSharpSyntaxTree.ParseText(code) },
                refs,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                Console.WriteLine("\n[ERROR] The bot code has compilation errors.");
                foreach (var diag in result.Diagnostics
                    .Where(d => d.Severity == DiagnosticSeverity.Error))
                {
                    Console.WriteLine($"-> {diag}");
                }
                Console.WriteLine("\nFix your Bot.cs file before starting the bot again.\n");
                return null;
            }

            try
            {
                ms.Seek(0, SeekOrigin.Begin);
                var asm = Assembly.Load(ms.ToArray());
                var botType = asm.GetTypes().FirstOrDefault(t => typeof(IBot).IsAssignableFrom(t));

                if (botType == null)
                {
                    Console.WriteLine("[ERROR] No bot found. Make sure your class implements IBot.");
                    return null;
                }

                return (IBot?)Activator.CreateInstance(botType);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Failed to load the bot: {ex.Message}");
                Console.WriteLine("Check the exceptions in your Bot.cs code.");
                return null;
            }
        }
    }
}