# C# Starterkit - JDIS Games 2026 Bot

This starterkit lets you run a C# bot connected to the server.

The main file to modify is:

```text
BotLogic/Bot.cs
```

## Requirements

- Install the .NET 9.0 SDK.

## 1. Configure the bot

In `BotLogic/Bot.cs`, the bot uses a token constant:

```csharp
public const string TOKEN = "BOTA-abcd-1234-ABCD";
```

Replace this value with your bot token.

## 2. Run the bot

From the starterkit folder:

```powershell
cd starterkits\Csharp
dotnet run
```

## 3. Modify the behavior

This method is called every tick. It must return an action that will be sent to the server to control the bot.

```csharp
public ActionBase? GetNextAction(GameState state)
```

### Example: move right

```csharp
public ActionBase? GetNextAction(GameState state)
{
    return new MoveAction(new Position(state.Bot.Position.X + 1, state.Bot.Position.Y));
}
```

### Example: gather a visible resource

```csharp
public ActionBase? GetNextAction(GameState state)
{
    var resource = state.VisibleResources.FirstOrDefault(r => r.CurrentAmount > 0);

    return new GatherNodeAction(resource.Position);
}
```

### Example: place an extractor

```csharp
public ActionBase? GetNextAction(GameState state)
{
    var node = state.VisibleResources.FirstOrDefault(r => r.CanHostExtractor);

    return new PlaceExtractorAction(node.Position);
}
```

## 4. Available actions

| Command | What it does |
| --- | --- |
| `MoveAction(Position newPosition)` | Moves the bot toward a target position. |
| `GatherNodeAction(Position gatherPosition)` | Gathers a visible resource node at the target position. |
| `AttackAction(Position targetPosition)` | Attacks a bot or companion at the target position. PVP and safezone rules are validated by the server. |
| `DepositToBaseAction()` | Deposits the bot inventory into base storage when the bot is at the base. |
| `WithdrawFromBaseAction(string itemName, int itemQuantity)` | Withdraws items from base storage into the bot inventory. |
| `SendCompanionAction()` | Sends an available companion to bring part of the bot inventory back to base. |
| `PlaceExtractorAction(Position targetNodePosition)` | Places an extractor on a visible compatible node. |
| `PlacePumpAction(Position targetNodePosition)` | Places a pump on a visible compatible liquid node. |
| `PlaceRadarAction(Position targetPosition)` | Places a radar at the target position. |
| `DestroyStructureAction(Position structurePosition)` | Damages or destroys an external structure: extractor, pump, or radar. |
| `AddItemToMuseumPedestalAction(int slotIndex, string itemName, int quantity)` | Adds items from storage onto a museum pedestal. |
| `RespawnAction()` | Requests a respawn when the bot is dead and its cooldown is finished. |

## 5. Available information

The starterkit receives limited vision. It does not receive the full map.

### Main state

| Class | Available information |
| --- | --- |
| `GameState` | `CurrentTick`, `VisibleTiles`, `VisibleResources`, `VisiblePlayers`, `VisibleCompanions`, `VisibleStructures`, `TeamPlayers`, `Bot`, `Team`, `Base` |
| `Position` | `X`, `Y` |
| `ItemStack` | `ItemName`, `Quantity` |

### Map and vision

| Class | Available information |
| --- | --- |
| `Tile` | `Position`, `Terrain`, `TerrainCategory`, `Zone`, `ZoneOwnerTeamId`, `HasStructure`, `HasEntity`, `HasResource` |
| `Resource` | `Id`, `Name`, `LootItem`, `Description`, `Position`, `CurrentAmount`, `Capacity`, `RemainingTicks`, `CanHostExtractor`, `CanHostPump` |

`Tile.Zone` indicates whether a tile is in a war zone, safezone, or base zone. `ZoneOwnerTeamId` indicates which team owns that zone when applicable.

### Players and companions

| Class | Available information |
| --- | --- |
| `PlayerInfo` | `Id`, `TeamId`, `BotType`, `Health`, `MaxHealth`, `Shield`, `MaxShield`, `Position`, `Inventory`, `Slots` |
| `VisiblePlayer` | `PlayerId`, `TeamId`, `BotType`, `IsAlly`, `IsSelf`, `PvpActivated`, `Alive`, `RespawnRemainingTicks`, `Position`, `Health`, `MaxHealth`, `Shield`, `MaxShield`, `Slots`, `Inventory` |
| `VisibleCompanion` | `CompanionId`, `TeamId`, `OwnerPlayerId`, `IsAlly`, `PvpActivated`, `Position`, `Health`, `MaxHealth`, `InventoryItemsCount` |

### Structures and base

| Class | Available information |
| --- | --- |
| `VisibleStructure` | `Id`, `Type`, `OwnerTeamId`, `OwnerBaseId`, `IsAlly`, `PvpActivated`, `Position`, `Width`, `Height`, `Hp`, `MaxHp`, `PowerOn`, `IsActive`, `VisionRadius` |
| `BaseInfo` | `Id`, `Position`, `Width`, `Height`, `StorageSlots`, `Inventory`, `InternalStructures` |
| `InternalStructureInfo` | `Type`, `Locked`, `MaxQueueSlots`, `MuseumSlots`, `MuseumBonusPerItem`, `MuseumMaxBonusPerItem`, `Queue`, `GeneratorQueue`, `Pedestals` |
| `ProcessingJobInfo` | `Id`, `RecipeId`, `TotalTicks`, `RemainingTicks`, `InputsPaid`, `RequestedRuns`, `CompletedRuns`, `Repeat` |
| `GeneratorJobInfo` | `Id`, `ItemName`, `TotalTicks`, `RemainingTicks`, `ItemPaid`, `Repeat`, `WaitingForItem` |
| `MuseumPedestalInfo` | `SlotIndex`, `ItemName`, `Quantity`, `ValueBonus`, `MaxValueBonus` |

### Team progression

| Class | Available information |
| --- | --- |
| `TeamInfo` | `Id`, `Name`, `Score`, `LaboratoryLevel`, `LaboratoryQueuesNumber`, `LaboratoryProductionSpeed`, `GeneratorProductionSpeed`, `GeneratorScoreMultiplier`, `MuseumSlots`, `MuseumBonusPerItem`, `MuseumMaxBonusPerItem`, `CraftAutomaticCycle`, `PvpActivated`, `NumberOfExtractor`, `NumberOfPump`, `NumberOfRadar`, `CompanionNumber`, `CompanionMaxHp`, `CompanionSlots`, `CompletedResearchIds`, `ResearchQueue`, `QuestSlots`, `QuestTimerReset`, `BonusQuestComplete`, `QuestQueue`, `Badges` |
| `ResearchJobInfo` | `Id`, `ResearchId`, `TotalTicks`, `RemainingTicks` |
| `QuestStateInfo` | `SlotIndex`, `InstanceId`, `TemplateId`, `Name`, `Description`, `Type`, `MetricType`, `SubjectName`, `Progress`, `Target`, `Status`, `BaseReward`, `Reward`, `ResetAt`, `RemainingResetSeconds` |
| `BadgeInfo` | `Id`, `Name`, `Description`, `Category`, `MetricType`, `Hidden`, `Unlocked`, `Progress`, `NextTarget` |
