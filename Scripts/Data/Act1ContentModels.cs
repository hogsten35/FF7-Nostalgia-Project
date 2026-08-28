using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FF7Nostalgia.Core.Data
{
    public sealed class CharacterDefinitionsRoot
    {
        [JsonPropertyName("characters")]
        public Dictionary<string, CharacterDefinition> Characters { get; set; } = new();
    }

    public sealed class CharacterDefinition
    {
        [JsonPropertyName("character_id")]
        public string CharacterId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("is_player_controlled")]
        public bool IsPlayerControlled { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = string.Empty;

        [JsonPropertyName("base_stats")]
        public CombatStats BaseStats { get; set; } = new();
    }

    public sealed class EnemyDefinitionsRoot
    {
        [JsonPropertyName("enemies")]
        public Dictionary<string, EnemyDefinition> Enemies { get; set; } = new();

        [JsonPropertyName("presentation_rules")]
        public PresentationRules PresentationRules { get; set; } = new();
    }

    public sealed class EnemyDefinition
    {
        [JsonPropertyName("enemy_id")]
        public string EnemyId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("tier")]
        public string Tier { get; set; } = string.Empty;

        [JsonPropertyName("base_stats")]
        public CombatStats BaseStats { get; set; } = new();

        [JsonPropertyName("moves")]
        public List<MoveDefinition> Moves { get; set; } = new();
    }

    public sealed class BossDefinitionsRoot
    {
        [JsonPropertyName("bosses")]
        public Dictionary<string, BossDefinition> Bosses { get; set; } = new();
    }

    public sealed class BossDefinition
    {
        [JsonPropertyName("boss_id")]
        public string BossId { get; set; } = string.Empty;

        [JsonPropertyName("boss_name")]
        public string BossName { get; set; } = string.Empty;

        [JsonPropertyName("difficulty")]
        public string Difficulty { get; set; } = string.Empty;

        [JsonPropertyName("base_stats")]
        public CombatStats BaseStats { get; set; } = new();

        [JsonPropertyName("phases")]
        public List<JsonElement> Phases { get; set; } = new();
    }

    public sealed class CombatStats
    {
        [JsonPropertyName("hp")]
        public int HP { get; set; }

        [JsonPropertyName("mp")]
        public int MP { get; set; }

        [JsonPropertyName("attack")]
        public int Attack { get; set; }

        [JsonPropertyName("defense")]
        public int Defense { get; set; }

        [JsonPropertyName("magic")]
        public int Magic { get; set; }

        [JsonPropertyName("magic_defense")]
        public int MagicDefense { get; set; }

        [JsonPropertyName("speed")]
        public int Speed { get; set; }
    }

    public sealed class MoveDefinition
    {
        [JsonPropertyName("move_id")]
        public string MoveId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("target")]
        public string Target { get; set; } = string.Empty;

        [JsonPropertyName("damage")]
        public int Damage { get; set; }

        [JsonPropertyName("action_probability")]
        public double ActionProbability { get; set; }

        [JsonPropertyName("condition")]
        public string Condition { get; set; } = string.Empty;

        [JsonPropertyName("effect")]
        public string Effect { get; set; } = string.Empty;
    }

    public sealed class PresentationRules
    {
        [JsonPropertyName("show_enemy_hp_numbers")]
        public bool ShowEnemyHPNumbers { get; set; }

        [JsonPropertyName("show_enemy_hp_percentages")]
        public bool ShowEnemyHPPercentages { get; set; }

        [JsonPropertyName("show_enemy_health_bars")]
        public bool ShowEnemyHealthBars { get; set; }
    }

    public sealed class Act1ContentBundle
    {
        public CharacterDefinitionsRoot Characters { get; init; } = new();
        public EnemyDefinitionsRoot Enemies { get; init; } = new();
        public BossDefinitionsRoot Bosses { get; init; } = new();
        public JsonDocument Dialogue { get; init; } = null!;
        public JsonDocument Objectives { get; init; } = null!;
        public JsonDocument Loot { get; init; } = null!;
        public JsonDocument SceneManifest { get; init; } = null!;
    }
}
