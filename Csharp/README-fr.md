# Starterkit C# - Bot JDIS Games 2026

Ce starterkit permet de lancer un bot C# connecté au serveur.

Le fichier principal à modifier est:

```text
BotLogic/Bot.cs
```

## Prérequis

- Installer le SDK .NET 9.0.

## 1. Configurer le bot

Dans `BotLogic/Bot.cs`, le bot utilise une constante de token:

```csharp
public const string TOKEN = "BOTA-abcd-1234-ABCD";
```

Remplacez cette valeur par le token de votre bot.

## 2. Lancer le bot

Depuis le dossier du starterkit:

```powershell
cd starterkits\Csharp
dotnet run
```

## 3. Modifier le comportement

Cette méthode est appelée à chaque tick. Elle doit retourner une action qui sera envoyée au serveur pour manipuler le bot.

```csharp
public ActionBase? GetNextAction(GameState state)
```

### Exemple: se déplacer à droite

```csharp
public ActionBase? GetNextAction(GameState state)
{
    return new MoveAction(new Position(state.Bot.Position.X + 1, state.Bot.Position.Y));
}
```

### Exemple: récolter une ressource visible

```csharp
public ActionBase? GetNextAction(GameState state)
{
    var resource = state.VisibleResources.FirstOrDefault(r => r.CurrentAmount > 0);

    return new GatherNodeAction(resource.Position);
}
```

### Exemple: placer un extracteur

```csharp
public ActionBase? GetNextAction(GameState state)
{
    var node = state.VisibleResources.FirstOrDefault(r => r.CanHostExtractor);

    return new PlaceExtractorAction(node.Position);
}
```

## 4. Actions possibles

| Commande | Ce que ça fait |
| --- | --- |
| `MoveAction(Position newPosition)` | Déplace le bot vers une position cible. |
| `GatherNodeAction(Position gatherPosition)` | Récolte une node de ressource visible à la position cible. |
| `AttackAction(Position targetPosition)` | Attaque un bot ou un companion à la position cible. Les règles de PVP et de safezone sont validées par le serveur. |
| `DepositToBaseAction()` | Dépose l'inventaire du bot dans le stockage de la base lorsque le bot est à la base. |
| `WithdrawFromBaseAction(string itemName, int itemQuantity)` | Retire des items du stockage de la base vers l'inventaire du bot. |
| `SendCompanionAction()` | Envoie un companion disponible pour rapporter une partie de l'inventaire du bot vers la base. |
| `PlaceExtractorAction(Position targetNodePosition)` | Place un extracteur sur une node compatible visible. |
| `PlacePumpAction(Position targetNodePosition)` | Place une pompe sur une node liquide compatible visible. |
| `PlaceRadarAction(Position targetPosition)` | Place un radar à la position cible. |
| `DestroyStructureAction(Position structurePosition)` | Endommage ou détruit une structure externe: extracteur, pompe ou radar. |
| `AddItemToMuseumPedestalAction(int slotIndex, string itemName, int quantity)` | Ajoute des items du stockage sur un piédestal du musée. |
| `RespawnAction()` | Demande un respawn lorsque le bot est mort et que son cooldown est terminé. |

## 5. Informations disponibles

Le starterkit reçoit une vision limitée. Il ne reçoit pas toute la carte.

### État principal

| Classe | Informations disponibles |
| --- | --- |
| `GameState` | `CurrentTick`, `VisibleTiles`, `VisibleResources`, `VisiblePlayers`, `VisibleCompanions`, `VisibleStructures`, `TeamPlayers`, `Bot`, `Team`, `Base` |
| `Position` | `X`, `Y` |
| `ItemStack` | `ItemName`, `Quantity` |

### Carte et vision

| Classe | Informations disponibles |
| --- | --- |
| `Tile` | `Position`, `Terrain`, `TerrainCategory`, `Zone`, `ZoneOwnerTeamId`, `HasStructure`, `HasEntity`, `HasResource` |
| `Resource` | `Id`, `Name`, `LootItem`, `Description`, `Position`, `CurrentAmount`, `Capacity`, `RemainingTicks`, `CanHostExtractor`, `CanHostPump` |

`Tile.Zone` indique si une tuile est dans une war zone, safezone ou base zone. `ZoneOwnerTeamId` indique quelle équipe possède cette zone lorsque c'est applicable.

### Joueurs et companions

| Classe | Informations disponibles |
| --- | --- |
| `PlayerInfo` | `Id`, `TeamId`, `BotType`, `Health`, `MaxHealth`, `Shield`, `MaxShield`, `Position`, `Inventory`, `Slots` |
| `VisiblePlayer` | `PlayerId`, `TeamId`, `BotType`, `IsAlly`, `IsSelf`, `PvpActivated`, `Alive`, `RespawnRemainingTicks`, `Position`, `Health`, `MaxHealth`, `Shield`, `MaxShield`, `Slots`, `Inventory` |
| `VisibleCompanion` | `CompanionId`, `TeamId`, `OwnerPlayerId`, `IsAlly`, `PvpActivated`, `Position`, `Health`, `MaxHealth`, `InventoryItemsCount` |

### Structures et base

| Classe | Informations disponibles |
| --- | --- |
| `VisibleStructure` | `Id`, `Type`, `OwnerTeamId`, `OwnerBaseId`, `IsAlly`, `PvpActivated`, `Position`, `Width`, `Height`, `Hp`, `MaxHp`, `PowerOn`, `IsActive`, `VisionRadius` |
| `BaseInfo` | `Id`, `Position`, `Width`, `Height`, `StorageSlots`, `Inventory`, `InternalStructures` |
| `InternalStructureInfo` | `Type`, `Locked`, `MaxQueueSlots`, `MuseumSlots`, `MuseumBonusPerItem`, `MuseumMaxBonusPerItem`, `Queue`, `GeneratorQueue`, `Pedestals` |
| `ProcessingJobInfo` | `Id`, `RecipeId`, `TotalTicks`, `RemainingTicks`, `InputsPaid`, `RequestedRuns`, `CompletedRuns`, `Repeat` |
| `GeneratorJobInfo` | `Id`, `ItemName`, `TotalTicks`, `RemainingTicks`, `ItemPaid`, `Repeat`, `WaitingForItem` |
| `MuseumPedestalInfo` | `SlotIndex`, `ItemName`, `Quantity`, `ValueBonus`, `MaxValueBonus` |

### Progression d'équipe

| Classe | Informations disponibles |
| --- | --- |
| `TeamInfo` | `Id`, `Name`, `Score`, `LaboratoryLevel`, `LaboratoryQueuesNumber`, `LaboratoryProductionSpeed`, `GeneratorProductionSpeed`, `GeneratorScoreMultiplier`, `MuseumSlots`, `MuseumBonusPerItem`, `MuseumMaxBonusPerItem`, `CraftAutomaticCycle`, `PvpActivated`, `NumberOfExtractor`, `NumberOfPump`, `NumberOfRadar`, `CompanionNumber`, `CompanionMaxHp`, `CompanionSlots`, `CompletedResearchIds`, `ResearchQueue`, `QuestSlots`, `QuestTimerReset`, `BonusQuestComplete`, `QuestQueue`, `Badges` |
| `ResearchJobInfo` | `Id`, `ResearchId`, `TotalTicks`, `RemainingTicks` |
| `QuestStateInfo` | `SlotIndex`, `InstanceId`, `TemplateId`, `Name`, `Description`, `Type`, `MetricType`, `SubjectName`, `Progress`, `Target`, `Status`, `BaseReward`, `Reward`, `ResetAt`, `RemainingResetSeconds` |
| `BadgeInfo` | `Id`, `Name`, `Description`, `Category`, `MetricType`, `Hidden`, `Unlocked`, `Progress`, `NextTarget` |
