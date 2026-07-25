// ============================== //
//                                //
//  NE PAS MODIFIER CE FICHIER    //
//   DO NOT MODIFY THIS FILE      //
//                                //
// ============================== //

using System.Text.Json;

namespace Csharp.Client;

public record Position
{
    public int X { get; init; }
    public int Y { get; init; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public enum TerrainType {
    Soda,
    Licorice,
    Fudge,
    Maple_Syrup,
    Sap,
    Sorbet,
    Vanilla,
    Cotton_Candy,
    Corn_Syrup,
    Base,
    Empty
}
public enum ZoneType {
    WarZone,
    SafeZone,
    BaseZone
}
public record Tile(
    Position Position,
    TerrainType Terrain,
    string TerrainCategory,
    ZoneType Zone,
    int? ZoneOwnerTeamId,
    bool HasStructure,
    bool HasEntity,
    bool HasResource);
public record ItemStack(string ItemName, int Quantity);

public record Resource
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string LootItem { get; init; } = "";
    public string Description { get; init; } = "";
    public Position Position { get; init; } = new(0, 0);
    public int CurrentAmount { get; init; }
    public int Capacity { get; init; }
    public int RemainingTicks { get; init; }
    public bool CanHostExtractor { get; init; }
    public bool CanHostPump { get; init; }
}

public enum BotType { BotA, BotB }
public class PlayerInfo
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public BotType BotType { get; set; } = BotType.BotA;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Shield { get; set; }
    public int MaxShield { get; set; }
    public Position Position { get; set; } = new(0, 0);
    public List<ItemStack> Inventory { get; set; } = new();
    public int Slots { get; set; }
}

public class VisiblePlayer
{
    public int PlayerId { get; set; }
    public int TeamId { get; set; }
    public string? BotType { get; set; }
    public bool IsAlly { get; set; }
    public bool IsSelf { get; set; }
    public bool PvpActivated { get; set; }
    public bool Alive { get; set; } = true;
    public int RespawnRemainingTicks { get; set; }
    public Position Position { get; set; } = new(0, 0);
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Shield { get; set; }
    public int MaxShield { get; set; }
    public int Slots { get; set; }
    public List<ItemStack> Inventory { get; set; } = new();
}

public class VisibleCompanion
{
    public int CompanionId { get; set; }
    public int TeamId { get; set; }
    public int OwnerPlayerId { get; set; }
    public bool IsAlly { get; set; }
    public bool PvpActivated { get; set; }
    public Position Position { get; set; } = new(0, 0);
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int InventoryItemsCount { get; set; }
}

public enum TypeStructure { Base, Extractor, Pump, Radar }
public class VisibleStructure
{
    public int Id { get; set; }
    public TypeStructure Type { get; set; } = TypeStructure.Extractor;
    public int OwnerTeamId { get; set; }
    public int OwnerBaseId { get; set; }
    public bool IsAlly { get; set; }
    public bool PvpActivated { get; set; }
    public Position Position { get; set; } = new(0, 0);
    public int Width { get; set; }
    public int Height { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public bool PowerOn { get; set; }
    public bool IsActive { get; set; }
    public int VisionRadius { get; set; }
}


public class ResearchJobInfo
{
    public int Id { get; set; }
    public string ResearchId { get; set; } = "";
    public int TotalTicks { get; set; }
    public int RemainingTicks { get; set; }
}

public class BadgeInfo
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public string MetricType { get; set; } = "";
    public bool Hidden { get; set; }
    public bool Unlocked { get; set; }
    public double Progress { get; set; }
    public double NextTarget { get; set; }
}

public class ProcessingJobInfo
{
    public int Id { get; set; }
    public string RecipeId { get; set; } = "";
    public int TotalTicks { get; set; }
    public int RemainingTicks { get; set; }
    public bool InputsPaid { get; set; }
    public int? RequestedRuns { get; set; }
    public int CompletedRuns { get; set; }
    public bool Repeat { get; set; }
}

public class GeneratorJobInfo
{
    public int Id { get; set; }
    public string ItemName { get; set; } = "";
    public int TotalTicks { get; set; }
    public int RemainingTicks { get; set; }
    public bool ItemPaid { get; set; }
    public bool Repeat { get; set; }
    public bool WaitingForItem { get; set; }
}

public class MuseumPedestalInfo
{
    public int SlotIndex { get; set; }
    public string? ItemName { get; set; }
    public int Quantity { get; set; }
    public double ValueBonus { get; set; }
    public double MaxValueBonus { get; set; }
}

public class InternalStructureInfo
{
    public string Type { get; set; } = "";
    public bool Locked { get; set; }
    public int MaxQueueSlots { get; set; }
    public int MuseumSlots { get; set; }
    public double MuseumBonusPerItem { get; set; }
    public double MuseumMaxBonusPerItem { get; set; }
    public List<ProcessingJobInfo> Queue { get; set; } = new();
    public List<GeneratorJobInfo> GeneratorQueue { get; set; } = new();
    public List<MuseumPedestalInfo> Pedestals { get; set; } = new();
}

public class QuestStateInfo
{
    public int SlotIndex { get; set; }
    public string InstanceId { get; set; } = "";
    public string TemplateId { get; set; } = "";
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string Type { get; set; } = "";
    public string MetricType { get; set; } = "";
    public string SubjectName { get; set; } = "";
    public double Progress { get; set; }
    public double Target { get; set; }
    public string Status { get; set; } = "";
    public double BaseReward { get; set; }
    public double Reward { get; set; }
    public double? ResetAt { get; set; }
    public double RemainingResetSeconds { get; set; }
}
public class TeamInfo
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public double Score { get; set; }
    public int LaboratoryLevel { get; set; }
    public int LaboratoryQueuesNumber { get; set; }
    public double LaboratoryProductionSpeed { get; set; }
    public double GeneratorProductionSpeed { get; set; }
    public double GeneratorScoreMultiplier { get; set; }
    public int MuseumSlots { get; set; }
    public double MuseumBonusPerItem { get; set; }
    public double MuseumMaxBonusPerItem { get; set; }
    public bool CraftAutomaticCycle { get; set; }
    public bool PvpActivated { get; set; }
    public int NumberOfExtractor { get; set; }
    public int NumberOfPump { get; set; }
    public int NumberOfRadar { get; set; }
    public int CompanionNumber { get; set; }
    public int CompanionMaxHp { get; set; }
    public int CompanionSlots { get; set; }
    public List<string> CompletedResearchIds { get; set; } = new();
    public List<ResearchJobInfo> ResearchQueue { get; set; } = new();
    public int QuestSlots { get; set; }
    public double QuestTimerReset { get; set; }
    public double BonusQuestComplete { get; set; }
    public List<QuestStateInfo> QuestQueue { get; set; } = new();
    public List<BadgeInfo> Badges { get; set; } = new();
}

public class BaseInfo
{
    public int Id { get; set; }
    public Position Position { get; set; } = new(0, 0);
    public int Width { get; set; }
    public int Height { get; set; }
    public int StorageSlots { get; set; }
    public List<ItemStack> Inventory { get; set; } = new();
    public List<InternalStructureInfo> InternalStructures { get; set; } = new();
}

public class GameState
{
    public int CurrentTick { get; set; }

    public Dictionary<(int, int), Tile> VisibleTiles { get; } = new();
    public List<Resource> VisibleResources { get; } = new();
    public List<VisiblePlayer> VisiblePlayers { get; } = new();
    public List<VisibleCompanion> VisibleCompanions { get; } = new();
    public List<VisibleStructure> VisibleStructures { get; } = new();
    public List<VisiblePlayer> TeamPlayers { get; } = new();

    public PlayerInfo? Bot { get; private set; }
    public TeamInfo? Team { get; private set; }
    public BaseInfo? Base { get; private set; }

    public void UpdatePlayer(JsonElement data)
    {
        Bot = new PlayerInfo
        {
            Id = data.GetProperty("id").GetInt32(),
            TeamId = data.GetProperty("teamId").GetInt32(),
            BotType = Enum.Parse<BotType>(data.GetProperty("botType").GetString() ?? "", true),
            Health = data.GetProperty("health").GetInt32(),
            MaxHealth = data.GetProperty("maxHealth").GetInt32(),
            Shield = data.TryGetProperty("shield", out var shield) ? shield.GetInt32() : 0,
            MaxShield = data.TryGetProperty("maxShield", out var maxShield) ? maxShield.GetInt32() : 0,
            Position = ReadPosition(data.GetProperty("position")),
            Slots = data.GetProperty("slots").GetInt32(),
            Inventory = data.GetProperty("inventory")
                .EnumerateArray()
                .Select(item => new ItemStack(
                    item.GetProperty("itemName").GetString() ?? "unknown",
                    item.GetProperty("quantity").GetInt32()))
                .ToList()
        };
    }

    public void UpdateVisionFromServer(JsonElement data)
    {
        var isComplete = !data.TryGetProperty("isComplete", out var isCompleteElement)
            || isCompleteElement.GetBoolean();

        if (isComplete)
            UpdateVision(data);
        else
            UpdatePartialVision(data);
    }

    public void UpdateVision(JsonElement data)
    {
        UpdateTiles(data);
        UpdateTeam(data);
        UpdatePlayers(data);
        UpdateCompanions(data);
        UpdateResources(data);
        UpdateStructures(data);
    }

    private void UpdatePartialVision(JsonElement data)
    {
        UpdateTeam(data);
        UpdateTilesDelta(data);
        UpdatePlayersDelta(data);
        UpdateCompanionsDelta(data);
        UpdateResourcesDelta(data);
        UpdateStructuresDelta(data);
    }

    public Tile? GetTileAt(Position pos)
        => VisibleTiles.TryGetValue((pos.X, pos.Y), out var tile) ? tile : null;

    private void UpdateTiles(JsonElement data)
    {
        VisibleTiles.Clear();

        if (!data.TryGetProperty("tiles", out var tiles))
            return;

        foreach (var tile in tiles.EnumerateArray())
        {
            var x = tile.GetProperty("x").GetInt32();
            var y = tile.GetProperty("y").GetInt32();
            var position = new Position(x, y);
            TerrainType terrain = Enum.Parse<TerrainType>(tile.GetProperty("terrain").GetString() ?? "", true);
            var terrainCategory = tile.TryGetProperty("terrainCategory", out var terrainCategoryElement)
                ? terrainCategoryElement.GetString() ?? "Land"
                : "Land";
            ZoneType zone = tile.TryGetProperty("zone", out var zoneElement)
                ? Enum.Parse<ZoneType>(zoneElement.GetString() ?? "", true)
                : ZoneType.WarZone;
            var owner = tile.TryGetProperty("zoneOwnerTeamId", out var ownerElement) && ownerElement.ValueKind != JsonValueKind.Null
                ? ownerElement.GetInt32()
                : (int?)null;
            var hasStructure = tile.TryGetProperty("hasStructure", out var hasStructureElement) && hasStructureElement.GetBoolean();
            var hasEntity = tile.TryGetProperty("hasEntity", out var hasEntityElement) && hasEntityElement.GetBoolean();
            var hasResource = tile.TryGetProperty("hasResource", out var hasResourceElement) && hasResourceElement.GetBoolean();

            VisibleTiles[(x, y)] = new Tile(position, terrain, terrainCategory, zone, owner, hasStructure, hasEntity, hasResource);
        }
    }

    private void UpdateTilesDelta(JsonElement data)
    {
        if (data.TryGetProperty("updatedTiles", out var updatedTiles))
        {
            foreach (var tile in updatedTiles.EnumerateArray())
            {
                var x = tile.GetProperty("x").GetInt32();
                var y = tile.GetProperty("y").GetInt32();
                var position = new Position(x, y);
                TerrainType terrain = Enum.Parse<TerrainType>(tile.GetProperty("terrain").GetString() ?? "", true);
                var terrainCategory = tile.TryGetProperty("terrainCategory", out var terrainCategoryElement)
                    ? terrainCategoryElement.GetString() ?? "Land"
                    : "Land";
                ZoneType zone = tile.TryGetProperty("zone", out var zoneElement)
                    ? Enum.Parse<ZoneType>(zoneElement.GetString() ?? "", true)
                    : ZoneType.WarZone;
                var owner = tile.TryGetProperty("zoneOwnerTeamId", out var ownerElement) && ownerElement.ValueKind != JsonValueKind.Null
                    ? ownerElement.GetInt32()
                    : (int?)null;
                var hasStructure = tile.TryGetProperty("hasStructure", out var hasStructureElement) && hasStructureElement.GetBoolean();
                var hasEntity = tile.TryGetProperty("hasEntity", out var hasEntityElement) && hasEntityElement.GetBoolean();
                var hasResource = tile.TryGetProperty("hasResource", out var hasResourceElement) && hasResourceElement.GetBoolean();

                VisibleTiles[(x, y)] = new Tile(position, terrain, terrainCategory, zone, owner, hasStructure, hasEntity, hasResource);
            }
        }

        if (data.TryGetProperty("removedTiles", out var removedTiles))
        {
            foreach (var position in removedTiles.EnumerateArray())
            {
                VisibleTiles.Remove((position.GetProperty("x").GetInt32(), position.GetProperty("y").GetInt32()));
            }
        }
    }

    private void UpdateTeam(JsonElement data)
    {
        if (data.TryGetProperty("team", out var team))
        {
            Team = new TeamInfo
            {
                Id = team.GetProperty("id").GetInt32(),
                Name = team.GetProperty("name").GetString() ?? "",
                Score = team.GetProperty("score").GetDouble(),
                LaboratoryLevel = team.TryGetProperty("laboratoryLevel", out var laboratoryLevel) ? laboratoryLevel.GetInt32() : 0,
                LaboratoryQueuesNumber = team.TryGetProperty("laboratoryQueuesNumber", out var laboratoryQueuesNumber) ? laboratoryQueuesNumber.GetInt32() : 0,
                LaboratoryProductionSpeed = team.TryGetProperty("laboratoryProductionSpeed", out var laboratoryProductionSpeed) ? laboratoryProductionSpeed.GetDouble() : 0,
                GeneratorProductionSpeed = team.TryGetProperty("generatorProductionSpeed", out var generatorProductionSpeed) ? generatorProductionSpeed.GetDouble() : 0,
                GeneratorScoreMultiplier = team.TryGetProperty("generatorScoreMultiplier", out var generatorScoreMultiplier) ? generatorScoreMultiplier.GetDouble() : 0,
                MuseumSlots = team.TryGetProperty("museumSlots", out var museumSlots) ? museumSlots.GetInt32() : 0,
                MuseumBonusPerItem = team.TryGetProperty("museumBonusPerItem", out var museumBonusPerItem) ? museumBonusPerItem.GetDouble() : 0.01,
                MuseumMaxBonusPerItem = team.TryGetProperty("museumMaxBonusPerItem", out var museumMaxBonusPerItem) ? museumMaxBonusPerItem.GetDouble() : 20,
                CraftAutomaticCycle = team.TryGetProperty("craftAutomaticCycle", out var craftAutomaticCycle) && craftAutomaticCycle.GetBoolean(),
                PvpActivated = team.TryGetProperty("pvpActivated", out var teamPvpActivated) && teamPvpActivated.GetBoolean(),
                NumberOfExtractor = team.TryGetProperty("numberOfExtractor", out var numberOfExtractor) ? numberOfExtractor.GetInt32() : 0,
                NumberOfPump = team.TryGetProperty("numberOfPump", out var numberOfPump) ? numberOfPump.GetInt32() : 0,
                NumberOfRadar = team.TryGetProperty("numberOfRadar", out var numberOfRadar) ? numberOfRadar.GetInt32() : 0,
                CompanionNumber = team.TryGetProperty("companionNumber", out var companionNumber) ? companionNumber.GetInt32() : 0,
                CompanionMaxHp = team.TryGetProperty("companionMaxHp", out var companionMaxHp) ? companionMaxHp.GetInt32() : 100,
                CompanionSlots = team.TryGetProperty("companionSlots", out var companionSlots) ? companionSlots.GetInt32() : 6,
                CompletedResearchIds = team.TryGetProperty("completedResearchIds", out var completedResearchIds) ? ReadStringList(completedResearchIds).ToList() : new List<string>(),
                ResearchQueue = team.TryGetProperty("researchQueue", out var researchQueue) ? ReadResearchQueue(researchQueue).ToList() : new List<ResearchJobInfo>(),
                QuestSlots = team.TryGetProperty("questSlots", out var questSlots) ? questSlots.GetInt32() : 0,
                QuestTimerReset = team.TryGetProperty("questTimerReset", out var questTimerReset) ? questTimerReset.GetDouble() : 0,
                BonusQuestComplete = team.TryGetProperty("bonusQuestComplete", out var bonusQuestComplete) ? bonusQuestComplete.GetDouble() : 1,
                QuestQueue = team.TryGetProperty("questQueue", out var questQueue) ? ReadQuestQueue(questQueue).ToList() : new List<QuestStateInfo>(),
                Badges = team.TryGetProperty("badges", out var badges) ? ReadBadges(badges).ToList() : new List<BadgeInfo>()
            };
        }

        if (data.TryGetProperty("base", out var baseElement))
        {
            Base = new BaseInfo
            {
                Id = baseElement.GetProperty("id").GetInt32(),
                Position = ReadPosition(baseElement.GetProperty("position")),
                Width = baseElement.GetProperty("width").GetInt32(),
                Height = baseElement.GetProperty("height").GetInt32(),
                StorageSlots = baseElement.GetProperty("storageSlots").GetInt32(),
                Inventory = baseElement.TryGetProperty("inventory", out var inventory)
                    ? ReadInventory(inventory).ToList()
                    : new List<ItemStack>(),
                InternalStructures = baseElement.TryGetProperty("internalStructures", out var internalStructures)
                    ? ReadInternalStructures(internalStructures).ToList()
                    : new List<InternalStructureInfo>()
            };
        }
    }

    private void UpdatePlayers(JsonElement data)
    {
        TeamPlayers.Clear();
        if (data.TryGetProperty("teamPlayers", out var teamPlayers))
            TeamPlayers.AddRange(ReadVisiblePlayers(teamPlayers));

        VisiblePlayers.Clear();
        if (data.TryGetProperty("visiblePlayers", out var visiblePlayers))
            VisiblePlayers.AddRange(ReadVisiblePlayers(visiblePlayers));
    }

    private void UpdatePlayersDelta(JsonElement data)
    {
        if (data.TryGetProperty("teamPlayers", out var teamPlayers))
        {
            TeamPlayers.Clear();
            TeamPlayers.AddRange(ReadVisiblePlayers(teamPlayers));
        }

        if (data.TryGetProperty("removedPlayerIds", out var removedPlayerIds))
        {
            var removed = removedPlayerIds.EnumerateArray()
                .Select(id => id.GetInt32())
                .ToHashSet();
            VisiblePlayers.RemoveAll(player => removed.Contains(player.PlayerId));
        }

        if (data.TryGetProperty("updatedPlayers", out var updatedPlayers))
        {
            foreach (var player in ReadVisiblePlayers(updatedPlayers))
            {
                VisiblePlayers.RemoveAll(existing => existing.PlayerId == player.PlayerId);
                VisiblePlayers.Add(player);
            }
        }
    }

    private void UpdateCompanions(JsonElement data)
    {
        VisibleCompanions.Clear();
        if (data.TryGetProperty("visibleCompanions", out var visibleCompanions))
            VisibleCompanions.AddRange(ReadVisibleCompanions(visibleCompanions));
    }

    private void UpdateCompanionsDelta(JsonElement data)
    {
        if (data.TryGetProperty("removedCompanionIds", out var removedCompanionIds))
        {
            var removed = removedCompanionIds.EnumerateArray()
                .Select(id => id.GetInt32())
                .ToHashSet();
            VisibleCompanions.RemoveAll(companion => removed.Contains(companion.CompanionId));
        }

        if (data.TryGetProperty("updatedCompanions", out var updatedCompanions))
        {
            foreach (var companion in ReadVisibleCompanions(updatedCompanions))
            {
                VisibleCompanions.RemoveAll(existing => existing.CompanionId == companion.CompanionId);
                VisibleCompanions.Add(companion);
            }
        }
    }

    private void UpdateResources(JsonElement data)
    {
        VisibleResources.Clear();

        if (!data.TryGetProperty("resources", out var resources))
            return;

        foreach (var resource in resources.EnumerateArray())
        {
            VisibleResources.Add(new Resource
            {
                Id = resource.GetProperty("id").GetInt32(),
                Name = resource.GetProperty("name").GetString() ?? "unknown",
                LootItem = resource.TryGetProperty("lootItem", out var lootItem) ? lootItem.GetString() ?? "" : "",
                Description = resource.GetProperty("description").GetString() ?? "",
                Position = ReadPosition(resource.GetProperty("position")),
                CurrentAmount = resource.GetProperty("currentAmount").GetInt32(),
                Capacity = resource.GetProperty("capacity").GetInt32(),
                RemainingTicks = resource.TryGetProperty("remainingTicks", out var remainingTicks) ? remainingTicks.GetInt32() : 0,
                CanHostExtractor = resource.TryGetProperty("canHostExtractor", out var canHostExtractor) && canHostExtractor.GetBoolean(),
                CanHostPump = resource.TryGetProperty("canHostPump", out var canHostPump) && canHostPump.GetBoolean()
            });
        }
    }

    private void UpdateResourcesDelta(JsonElement data)
    {
        if (data.TryGetProperty("removedResourceIds", out var removedResourceIds))
        {
            var removed = removedResourceIds.EnumerateArray()
                .Select(id => id.GetInt32())
                .ToHashSet();
            VisibleResources.RemoveAll(resource => removed.Contains(resource.Id));
        }

        if (!data.TryGetProperty("updatedResources", out var updatedResources))
            return;

        foreach (var resource in updatedResources.EnumerateArray())
        {
            var updated = new Resource
            {
                Id = resource.GetProperty("id").GetInt32(),
                Name = resource.GetProperty("name").GetString() ?? "unknown",
                LootItem = resource.TryGetProperty("lootItem", out var lootItem) ? lootItem.GetString() ?? "" : "",
                Description = resource.GetProperty("description").GetString() ?? "",
                Position = ReadPosition(resource.GetProperty("position")),
                CurrentAmount = resource.GetProperty("currentAmount").GetInt32(),
                Capacity = resource.GetProperty("capacity").GetInt32(),
                RemainingTicks = resource.TryGetProperty("remainingTicks", out var remainingTicks) ? remainingTicks.GetInt32() : 0,
                CanHostExtractor = resource.TryGetProperty("canHostExtractor", out var canHostExtractor) && canHostExtractor.GetBoolean(),
                CanHostPump = resource.TryGetProperty("canHostPump", out var canHostPump) && canHostPump.GetBoolean()
            };

            VisibleResources.RemoveAll(existing => existing.Id == updated.Id);
            VisibleResources.Add(updated);
        }
    }

    private void UpdateStructures(JsonElement data)
    {
        VisibleStructures.Clear();

        if (!data.TryGetProperty("visibleStructures", out var structures))
            return;

        VisibleStructures.AddRange(ReadVisibleStructures(structures));
    }

    private void UpdateStructuresDelta(JsonElement data)
    {
        if (data.TryGetProperty("removedStructureIds", out var removedStructureIds))
        {
            var removed = removedStructureIds.EnumerateArray()
                .Select(id => id.GetInt32())
                .ToHashSet();
            VisibleStructures.RemoveAll(structure => removed.Contains(structure.Id));
        }

        if (!data.TryGetProperty("updatedStructures", out var updatedStructures))
            return;

        foreach (var structure in ReadVisibleStructures(updatedStructures))
        {
            VisibleStructures.RemoveAll(existing => existing.Id == structure.Id);
            VisibleStructures.Add(structure);
        }
    }

    private static IEnumerable<VisibleStructure> ReadVisibleStructures(JsonElement structures)
    {
        foreach (var structure in structures.EnumerateArray())
        {
            yield return new VisibleStructure
            {
                Id = structure.GetProperty("id").GetInt32(),
                Type = Enum.Parse<TypeStructure>(structure.GetProperty("type").GetString() ?? "", true),
                OwnerTeamId = structure.TryGetProperty("ownerTeamId", out var ownerTeamId) ? ownerTeamId.GetInt32() : 0,
                OwnerBaseId = structure.TryGetProperty("ownerBaseId", out var ownerBaseId) ? ownerBaseId.GetInt32() : 0,
                IsAlly = structure.TryGetProperty("isAlly", out var isAlly) && isAlly.GetBoolean(),
                PvpActivated = structure.TryGetProperty("pvpActivated", out var structurePvpActivated) && structurePvpActivated.GetBoolean(),
                Position = ReadPosition(structure.GetProperty("position")),
                Width = structure.TryGetProperty("width", out var width) ? width.GetInt32() : 1,
                Height = structure.TryGetProperty("height", out var height) ? height.GetInt32() : 1,
                Hp = structure.TryGetProperty("hp", out var hp) ? hp.GetInt32() : 0,
                MaxHp = structure.TryGetProperty("maxHp", out var maxHp) ? maxHp.GetInt32() : 0,
                PowerOn = structure.TryGetProperty("powerOn", out var powerOn) && powerOn.GetBoolean(),
                IsActive = structure.TryGetProperty("isActive", out var isActive) && isActive.GetBoolean(),
                VisionRadius = structure.TryGetProperty("visionRadius", out var visionRadius) ? visionRadius.GetInt32() : 0
            };
        }
    }

    private static IEnumerable<VisiblePlayer> ReadVisiblePlayers(JsonElement players)
    {
        foreach (var player in players.EnumerateArray())
        {
            var id = player.TryGetProperty("playerId", out var playerId)
                ? playerId.GetInt32()
                : player.GetProperty("id").GetInt32();

            yield return new VisiblePlayer
            {
                PlayerId = id,
                TeamId = player.TryGetProperty("teamId", out var teamId) ? teamId.GetInt32() : 0,
                BotType = player.TryGetProperty("botType", out var botType) && botType.ValueKind != JsonValueKind.Null ? botType.GetString() : null,
                IsAlly = player.TryGetProperty("isAlly", out var isAlly) && isAlly.GetBoolean(),
                IsSelf = player.TryGetProperty("isSelf", out var isSelf) && isSelf.GetBoolean(),
                PvpActivated = player.TryGetProperty("pvpActivated", out var pvpActivated) && pvpActivated.GetBoolean(),
                Alive = !player.TryGetProperty("alive", out var alive) || alive.GetBoolean(),
                RespawnRemainingTicks = player.TryGetProperty("respawnRemainingTicks", out var respawnRemainingTicks) ? respawnRemainingTicks.GetInt32() : 0,
                Position = ReadPosition(player.GetProperty("position")),
                Health = player.TryGetProperty("health", out var health) ? health.GetInt32() : 0,
                MaxHealth = player.TryGetProperty("maxHealth", out var maxHealth) ? maxHealth.GetInt32() : 0,
                Shield = player.TryGetProperty("shield", out var shield) ? shield.GetInt32() : 0,
                MaxShield = player.TryGetProperty("maxShield", out var maxShield) ? maxShield.GetInt32() : 0,
                Slots = player.TryGetProperty("slots", out var slots) ? slots.GetInt32() : 0,
                Inventory = player.TryGetProperty("inventory", out var inventory) ? ReadInventory(inventory).ToList() : new List<ItemStack>()
            };
        }
    }

    private static IEnumerable<VisibleCompanion> ReadVisibleCompanions(JsonElement companions)
    {
        foreach (var companion in companions.EnumerateArray())
        {
            yield return new VisibleCompanion
            {
                CompanionId = companion.GetProperty("companionId").GetInt32(),
                TeamId = companion.TryGetProperty("teamId", out var teamId) ? teamId.GetInt32() : 0,
                OwnerPlayerId = companion.TryGetProperty("ownerPlayerId", out var ownerPlayerId) ? ownerPlayerId.GetInt32() : 0,
                IsAlly = companion.TryGetProperty("isAlly", out var isAlly) && isAlly.GetBoolean(),
                PvpActivated = companion.TryGetProperty("pvpActivated", out var companionPvpActivated) && companionPvpActivated.GetBoolean(),
                Position = ReadPosition(companion.GetProperty("position")),
                Health = companion.TryGetProperty("health", out var health) ? health.GetInt32() : 0,
                MaxHealth = companion.TryGetProperty("maxHealth", out var maxHealth) ? maxHealth.GetInt32() : 0,
                InventoryItemsCount = companion.TryGetProperty("inventoryItemsCount", out var inventoryItemsCount) ? inventoryItemsCount.GetInt32() : 0
            };
        }
    }

    private static IEnumerable<InternalStructureInfo> ReadInternalStructures(JsonElement structures)
    {
        foreach (var structure in structures.EnumerateArray())
        {
            yield return new InternalStructureInfo
            {
                Type = structure.TryGetProperty("type", out var type) ? type.GetString() ?? "" : "",
                Locked = structure.TryGetProperty("locked", out var locked) && locked.GetBoolean(),
                MaxQueueSlots = structure.TryGetProperty("maxQueueSlots", out var maxQueueSlots) ? maxQueueSlots.GetInt32() : 0,
                MuseumSlots = structure.TryGetProperty("museumSlots", out var museumSlots) ? museumSlots.GetInt32() : 0,
                MuseumBonusPerItem = structure.TryGetProperty("museumBonusPerItem", out var museumBonusPerItem) ? museumBonusPerItem.GetDouble() : 0,
                MuseumMaxBonusPerItem = structure.TryGetProperty("museumMaxBonusPerItem", out var museumMaxBonusPerItem) ? museumMaxBonusPerItem.GetDouble() : 0,
                Queue = structure.TryGetProperty("queue", out var queue) ? ReadProcessingQueue(queue).ToList() : new List<ProcessingJobInfo>(),
                GeneratorQueue = structure.TryGetProperty("generatorQueue", out var generatorQueue) ? ReadGeneratorQueue(generatorQueue).ToList() : new List<GeneratorJobInfo>(),
                Pedestals = structure.TryGetProperty("pedestals", out var pedestals) ? ReadPedestals(pedestals).ToList() : new List<MuseumPedestalInfo>()
            };
        }
    }

    private static IEnumerable<ProcessingJobInfo> ReadProcessingQueue(JsonElement queue)
    {
        foreach (var job in queue.EnumerateArray())
        {
            yield return new ProcessingJobInfo
            {
                Id = job.TryGetProperty("id", out var id) ? id.GetInt32() : 0,
                RecipeId = job.TryGetProperty("recipeId", out var recipeId) ? recipeId.GetString() ?? "" : "",
                TotalTicks = job.TryGetProperty("totalTicks", out var totalTicks) ? totalTicks.GetInt32() : 0,
                RemainingTicks = job.TryGetProperty("remainingTicks", out var remainingTicks) ? remainingTicks.GetInt32() : 0,
                InputsPaid = job.TryGetProperty("inputsPaid", out var inputsPaid) && inputsPaid.GetBoolean(),
                RequestedRuns = job.TryGetProperty("requestedRuns", out var requestedRuns) && requestedRuns.ValueKind != JsonValueKind.Null ? requestedRuns.GetInt32() : null,
                CompletedRuns = job.TryGetProperty("completedRuns", out var completedRuns) ? completedRuns.GetInt32() : 0,
                Repeat = job.TryGetProperty("repeat", out var repeat) && repeat.GetBoolean()
            };
        }
    }

    private static IEnumerable<GeneratorJobInfo> ReadGeneratorQueue(JsonElement queue)
    {
        foreach (var job in queue.EnumerateArray())
        {
            yield return new GeneratorJobInfo
            {
                Id = job.TryGetProperty("id", out var id) ? id.GetInt32() : 0,
                ItemName = job.TryGetProperty("itemName", out var itemName) ? itemName.GetString() ?? "" : "",
                TotalTicks = job.TryGetProperty("totalTicks", out var totalTicks) ? totalTicks.GetInt32() : 0,
                RemainingTicks = job.TryGetProperty("remainingTicks", out var remainingTicks) ? remainingTicks.GetInt32() : 0,
                ItemPaid = job.TryGetProperty("itemPaid", out var itemPaid) && itemPaid.GetBoolean(),
                Repeat = job.TryGetProperty("repeat", out var repeat) && repeat.GetBoolean(),
                WaitingForItem = job.TryGetProperty("waitingForItem", out var waitingForItem) && waitingForItem.GetBoolean()
            };
        }
    }

    private static IEnumerable<MuseumPedestalInfo> ReadPedestals(JsonElement pedestals)
    {
        foreach (var pedestal in pedestals.EnumerateArray())
        {
            yield return new MuseumPedestalInfo
            {
                SlotIndex = pedestal.TryGetProperty("slotIndex", out var slotIndex) ? slotIndex.GetInt32() : 0,
                ItemName = pedestal.TryGetProperty("itemName", out var itemName) && itemName.ValueKind != JsonValueKind.Null ? itemName.GetString() : null,
                Quantity = pedestal.TryGetProperty("quantity", out var quantity) ? quantity.GetInt32() : 0,
                ValueBonus = pedestal.TryGetProperty("valueBonus", out var valueBonus) ? valueBonus.GetDouble() : 0,
                MaxValueBonus = pedestal.TryGetProperty("maxValueBonus", out var maxValueBonus) ? maxValueBonus.GetDouble() : 0
            };
        }
    }
    private static IEnumerable<ItemStack> ReadInventory(JsonElement inventory)
    {
        foreach (var item in inventory.EnumerateArray())
        {
            yield return new ItemStack(
                item.GetProperty("itemName").GetString() ?? "unknown",
                item.GetProperty("quantity").GetInt32());
        }
    }

    private static IEnumerable<string> ReadStringList(JsonElement values)
    {
        foreach (var value in values.EnumerateArray())
        {
            if (value.ValueKind == JsonValueKind.String)
                yield return value.GetString() ?? "";
        }
    }

    private static IEnumerable<ResearchJobInfo> ReadResearchQueue(JsonElement queue)
    {
        foreach (var job in queue.EnumerateArray())
        {
            yield return new ResearchJobInfo
            {
                Id = job.TryGetProperty("id", out var id) ? id.GetInt32() : 0,
                ResearchId = job.TryGetProperty("researchId", out var researchId) ? researchId.GetString() ?? "" : "",
                TotalTicks = job.TryGetProperty("totalTicks", out var totalTicks) ? totalTicks.GetInt32() : 0,
                RemainingTicks = job.TryGetProperty("remainingTicks", out var remainingTicks) ? remainingTicks.GetInt32() : 0
            };
        }
    }

    private static IEnumerable<QuestStateInfo> ReadQuestQueue(JsonElement quests)
    {
        foreach (var quest in quests.EnumerateArray())
        {
            yield return new QuestStateInfo
            {
                SlotIndex = quest.TryGetProperty("slotIndex", out var slotIndex) ? slotIndex.GetInt32() : 0,
                InstanceId = quest.TryGetProperty("instanceId", out var instanceId) ? instanceId.GetString() ?? "" : "",
                TemplateId = quest.TryGetProperty("templateId", out var templateId) ? templateId.GetString() ?? "" : "",
                Name = quest.TryGetProperty("name", out var name) ? name.GetString() ?? "" : "",
                Description = quest.TryGetProperty("description", out var description) ? description.GetString() ?? "" : "",
                Type = quest.TryGetProperty("type", out var type) ? type.GetString() ?? "" : "",
                MetricType = quest.TryGetProperty("metricType", out var metricType) ? metricType.GetString() ?? "" : "",
                SubjectName = quest.TryGetProperty("subjectName", out var subjectName) ? subjectName.GetString() ?? "" : "",
                Progress = quest.TryGetProperty("progress", out var progress) ? progress.GetDouble() : 0,
                Target = quest.TryGetProperty("target", out var target) ? target.GetDouble() : 0,
                Status = quest.TryGetProperty("status", out var status) ? status.GetString() ?? "" : "",
                BaseReward = quest.TryGetProperty("baseReward", out var baseReward) ? baseReward.GetDouble() : 0,
                Reward = quest.TryGetProperty("reward", out var reward) ? reward.GetDouble() : 0,
                ResetAt = quest.TryGetProperty("resetAt", out var resetAt) && resetAt.ValueKind != JsonValueKind.Null ? resetAt.GetDouble() : null,
                RemainingResetSeconds = quest.TryGetProperty("remainingResetSeconds", out var remainingResetSeconds) ? remainingResetSeconds.GetDouble() : 0
            };
        }
    }
    private static IEnumerable<BadgeInfo> ReadBadges(JsonElement badges)
    {
        foreach (var badge in badges.EnumerateArray())
        {
            yield return new BadgeInfo
            {
                Id = badge.TryGetProperty("id", out var id) ? id.GetString() ?? "" : "",
                Name = badge.TryGetProperty("name", out var name) ? name.GetString() ?? "" : "",
                Description = badge.TryGetProperty("description", out var description) ? description.GetString() ?? "" : "",
                Category = badge.TryGetProperty("category", out var category) ? category.GetString() ?? "" : "",
                MetricType = badge.TryGetProperty("metricType", out var metricType) ? metricType.GetString() ?? "" : "",
                Hidden = badge.TryGetProperty("hidden", out var hidden) && hidden.GetBoolean(),
                Unlocked = badge.TryGetProperty("unlocked", out var unlocked) && unlocked.GetBoolean(),
                Progress = badge.TryGetProperty("progress", out var progress) ? progress.GetDouble() : 0,
                NextTarget = badge.TryGetProperty("nextTarget", out var nextTarget) ? nextTarget.GetDouble() : 0
            };
        }
    }

    private static Position ReadPosition(JsonElement position)
    {
        return new Position(
            position.GetProperty("x").GetInt32(),
            position.GetProperty("y").GetInt32());
    }
}

public abstract record ActionBase
{
    public string Type { get; init; } = "";
}

public record MoveAction : ActionBase
{
    public Position NewPosition { get; init; }

    public MoveAction(Position newPosition)
    {
        Type = "Move";
        NewPosition = newPosition;
    }
}

public record GatherNodeAction : ActionBase
{
    public Position GatherPosition { get; init; }

    public GatherNodeAction(Position gatherPosition)
    {
        Type = "Gather";
        GatherPosition = gatherPosition;
    }
}

public record AttackAction : ActionBase
{
    public Position TargetPosition { get; init; }

    public AttackAction(Position targetPosition)
    {
        Type = "Attack";
        TargetPosition = targetPosition;
    }
}

public record DepositToBaseAction : ActionBase
{
    public DepositToBaseAction()
    {
        Type = "DepositToBase";
    }
}

public record WithdrawFromBaseAction : ActionBase
{
    public string ItemName { get; init; }
    public int ItemQuantity { get; init; }

    public WithdrawFromBaseAction(string itemName, int itemQuantity)
    {
        Type = "WithdrawFromBase";
        ItemName = itemName;
        ItemQuantity = itemQuantity;
    }
}

public record SendCompanionAction : ActionBase
{
    public SendCompanionAction()
    {
        Type = "SendCompanion";
    }
}

public record PlaceExtractorAction : ActionBase
{
    public Position TargetNodePosition { get; init; }

    public PlaceExtractorAction(Position targetNodePosition)
    {
        Type = "PlaceExtractor";
        TargetNodePosition = targetNodePosition;
    }
}

public record PlacePumpAction : ActionBase
{
    public Position TargetNodePosition { get; init; }

    public PlacePumpAction(Position targetNodePosition)
    {
        Type = "PlacePump";
        TargetNodePosition = targetNodePosition;
    }
}

public record PlaceRadarAction : ActionBase
{
    public Position TargetPosition { get; init; }

    public PlaceRadarAction(Position targetPosition)
    {
        Type = "PlaceRadar";
        TargetPosition = targetPosition;
    }
}

public record DestroyStructureAction : ActionBase
{
    public Position StructurePosition { get; init; }

    public DestroyStructureAction(Position structurePosition)
    {
        Type = "DestroyStructure";
        StructurePosition = structurePosition;
    }
}

public record AddItemToMuseumPedestalAction : ActionBase
{
    public int SlotIndex { get; init; }
    public string ItemName { get; init; }
    public int Quantity { get; init; }

    public AddItemToMuseumPedestalAction(int slotIndex, string itemName, int quantity = 0)
    {
        Type = "AddItemToMuseumPedestal";
        SlotIndex = slotIndex;
        ItemName = itemName;
        Quantity = quantity;
    }
}

public record RespawnAction : ActionBase
{
    public RespawnAction()
    {
        Type = "Respawn";
    }
}
