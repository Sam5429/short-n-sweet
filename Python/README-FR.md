# Starterkit Python - Bot JDIS Games 2026

Ce starterkit permet de lancer un bot Python connecté au serveur.

Le fichier principal à modifier est:

```text
bot.py
```

## Prérequis

- Installer Python 3.11 ou plus récent.
- Installer les dépendances du starterkit.

```powershell
pip install -r requirements.txt
```

## 1. Configurer le bot

Dans `bot.py`, le bot utilise une constante de token:

```python
TOKEN = "BOTA-abcd-1234-ABCD"
```

Remplacez cette valeur par le token de votre bot.

## 2. Lancer le bot

Depuis le dossier du starterkit:

```powershell
cd starterkits\Python
python main.py
```

## 3. Modifier le comportement

Cette méthode est appelée à chaque tick. Elle doit retourner une action qui sera envoyée au serveur pour manipuler le bot.

```python
def get_next_action(self, state: GameState) -> ActionBase | None:
```

### Exemple: se déplacer à droite

```python
def get_next_action(self, state: GameState) -> ActionBase | None:
    return MoveAction(Position(state.Bot.Position.X + 1, state.Bot.Position.Y))
```

### Exemple: récolter une ressource visible

```python
def get_next_action(self, state: GameState) -> ActionBase | None:
    resource = next((r for r in state.VisibleResources if r.CurrentAmount > 0), None)
    if resource is None:
        return None

    return GatherNodeAction(resource.Position)
```

### Exemple: placer un extracteur

```python
def get_next_action(self, state: GameState) -> ActionBase | None:
    node = next((r for r in state.VisibleResources if r.CanHostExtractor), None)
    if node is None:
        return None

    return PlaceExtractorAction(node.Position)
```

## 4. Actions possibles

| Commande | Ce que ça fait |
| --- | --- |
| `MoveAction(Position new_position)` | Déplace le bot vers une position cible. |
| `GatherNodeAction(Position gather_position)` | Récolte une node de ressource visible à la position cible. |
| `AttackAction(Position target_position)` | Attaque un bot ou un companion à la position cible. Les règles de PVP et de safezone sont validées par le serveur. |
| `DepositToBaseAction()` | Dépose l'inventaire du bot dans le stockage de la base lorsque le bot est à la base. |
| `WithdrawFromBaseAction(str item_name, int item_quantity)` | Retire des items du stockage de la base vers l'inventaire du bot. |
| `SendCompanionAction()` | Envoie un companion disponible pour rapporter une partie de l'inventaire du bot vers la base. |
| `PlaceExtractorAction(Position target_node_position)` | Place un extracteur sur une node compatible visible. |
| `PlacePumpAction(Position target_node_position)` | Place une pompe sur une node liquide compatible visible. |
| `PlaceRadarAction(Position target_position)` | Place un radar à la position cible. |
| `DestroyStructureAction(Position structure_position)` | Endommage ou détruit une structure externe: extracteur, pompe ou radar. |
| `AddItemToMuseumPedestalAction(int slot_index, str item_name, int quantity)` | Ajoute des items du stockage sur un piédestal du musée. |
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
