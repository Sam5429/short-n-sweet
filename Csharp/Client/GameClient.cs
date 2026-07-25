// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using Microsoft.AspNetCore.SignalR.Client;
using Csharp.Client;
using Csharp.Utils;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Csharp.Client
{
    public class GameClient
    {
        private readonly HubConnection _connection;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly TaskCompletionSource<bool> _authenticated = new();

        private readonly GameState _currentState = new();
        private readonly SemaphoreSlim _actionGate = new(1, 1);
        private int _lastActionTickSent = -1;

        public GameClient(string url, string token)
        {
            _jsonOptions = Utils.JsonOptions.Default;
            _connection = new HubConnectionBuilder()
                .WithUrl($"{url}/api/hub?token={token}&type=bot")
                .WithAutomaticReconnect()
                .Build();

            // Validation Token
            _connection.On<object>("Authenticated", data =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[AUTH] Successfully authenticated.");
                Console.ResetColor();
                _authenticated.TrySetResult(true);
            });
            _connection.On<string>("Error", msg =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[AUTH] Authentication error: {msg}");
                Console.ResetColor();
                _authenticated.TrySetException(new Exception(msg));
            });
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                await _connection.StartAsync();
                Console.WriteLine("[INFO] Network connection established, waiting for token validation...");

                // Timeout si jamais le serveur ne répond pas
                var authTask = await Task.WhenAny(_authenticated.Task, Task.Delay(5000));
                if (authTask != _authenticated.Task)
                    throw new TimeoutException("No authentication response from the server.");

                return await _authenticated.Task;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Connection failed: {ex.Message}");
                Console.ResetColor();
                return false;
            }
        }

        public void OnGameState(Func<GameState, Task<ActionBase?>> onUpdate)
            => BackendListening(onUpdate);

        public void BackendListening(Func<GameState, Task<ActionBase?>> onUpdate)
        {
            // Tick global : maj du compteur
            _connection.On<dynamic>("Tick", async tickData =>
            {
                try
                {
                    _currentState.CurrentTick = tickData.GetProperty("tick").GetInt32();
                    await TrySendAction(onUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Tick: {ex.Message}");
                }
            });

            // Vision du bot
            _connection.On<dynamic>("ReceiveVisibleMap", async data =>
            {
                try
                {
                    _currentState.UpdateVisionFromServer(data);
                    await TrySendAction(onUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] Tick: {ex.Message}");
                }
            });

            // Player Info
            _connection.On<JsonElement>("ReceivePlayerInfo", data =>
            {
                _currentState.UpdatePlayer(data);
            });
        }

        private async Task TrySendAction(Func<GameState, Task<ActionBase?>> onUpdate)
        {
            await _actionGate.WaitAsync();
            try
            {
                await TrySendActionCore(onUpdate);
            }
            finally
            {
                _actionGate.Release();
            }
        }

        private async Task TrySendActionCore(Func<GameState, Task<ActionBase?>> onUpdate)
        {
            // Si bot n'est pas initialisé
            if (_currentState.Bot is null)
            {
                Console.WriteLine("[BOT] Waiting for bot state initialization...");
                return;
            }

            if (_currentState.CurrentTick <= _lastActionTickSent)
            {
                return;
            }
            
            var action = await onUpdate(_currentState);
            if (action is null) return;

            _lastActionTickSent = _currentState.CurrentTick;

            // Type d'action
            object actionConcrete = action switch
            {
                MoveAction move => new
                {
                    type = move.Type,
                    newPosition = new { x = move.NewPosition.X, y = move.NewPosition.Y }
                },


                GatherNodeAction gather => new
                {
                    type = gather.Type,
                    gatherPosition = new { x = gather.GatherPosition.X, y = gather.GatherPosition.Y }
                },

                AttackAction attack => new
                {
                    type = attack.Type,
                    targetPosition = new { x = attack.TargetPosition.X, y = attack.TargetPosition.Y }
                },
                
                DepositToBaseAction depositToBase => new
                {
                    type = depositToBase.Type  
                },

                WithdrawFromBaseAction withdrawFromBase => new
                {
                    type = withdrawFromBase.Type,
                    itemName = withdrawFromBase.ItemName, 
                    itemQuantity = withdrawFromBase.ItemQuantity
                },

                SendCompanionAction sendCompanion => new
                {
                    type = sendCompanion.Type
                },

                PlaceExtractorAction placeExtractor => new
                {
                    type = placeExtractor.Type,
                    targetNodePosition = new { x = placeExtractor.TargetNodePosition.X, y = placeExtractor.TargetNodePosition.Y }
                },

                PlacePumpAction placePump => new
                {
                    type = placePump.Type,
                    targetNodePosition = new { x = placePump.TargetNodePosition.X, y = placePump.TargetNodePosition.Y }
                },

                PlaceRadarAction placeRadar => new
                {
                    type = placeRadar.Type,
                    targetPosition = new { x = placeRadar.TargetPosition.X, y = placeRadar.TargetPosition.Y }
                },

                DestroyStructureAction destroyStructure => new
                {
                    type = destroyStructure.Type,
                    structurePosition = new { x = destroyStructure.StructurePosition.X, y = destroyStructure.StructurePosition.Y }
                },

                AddItemToMuseumPedestalAction addMuseumItem => new
                {
                    type = addMuseumItem.Type,
                    slotIndex = addMuseumItem.SlotIndex,
                    itemName = addMuseumItem.ItemName,
                    quantity = addMuseumItem.Quantity
                },

                RespawnAction respawn => new
                {
                    type = respawn.Type
                },

                _ => new { type = action.Type }
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            string actionJson = JsonSerializer.Serialize(actionConcrete, jsonOptions);
            var actionElement = JsonDocument.Parse(actionJson).RootElement;

            var envelope = new
            {
                Type = "COMMAND",
                Action = actionElement,
                Tick = _currentState.CurrentTick
            };

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=== [BOT->SERVER] Sending JSON ===");
            Console.WriteLine(JsonSerializer.Serialize(envelope, new JsonSerializerOptions { WriteIndented = true }));
            Console.WriteLine("===============================");
            Console.ResetColor();

            await _connection.InvokeAsync("SubmitAction", envelope);
        }
    }
}
