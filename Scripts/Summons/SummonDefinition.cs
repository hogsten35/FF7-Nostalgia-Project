using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FF7Nostalgia.Core.Summons
{
    public sealed class SummonCatalog
    {
        [JsonPropertyName("summons")]
        public Dictionary<string, SummonDefinition> Summons { get; set; } = new();

        [JsonPropertyName("system_rules")]
        public SummonSystemRules SystemRules { get; set; } = new();
    }

    public sealed class SummonDefinition
    {
        [JsonPropertyName("summon_id")]
        public string SummonId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("element")]
        public string Element { get; set; } = "none";

        [JsonPropertyName("resource_type")]
        public string ResourceType { get; set; } = "mp";

        [JsonPropertyName("resource_cost")]
        public int ResourceCost { get; set; }

        [JsonPropertyName("power")]
        public float Power { get; set; } = 1f;

        [JsonPropertyName("target")]
        public string Target { get; set; } = "all_enemies";

        [JsonPropertyName("effect")]
        public string Effect { get; set; } = "damage";

        [JsonPropertyName("unlock_flag")]
        public string UnlockFlag { get; set; } = string.Empty;

        [JsonPropertyName("presentation")]
        public SummonPresentation Presentation { get; set; } = new();
    }

    public sealed class SummonPresentation
    {
        [JsonPropertyName("model_key")]
        public string ModelKey { get; set; } = string.Empty;

        [JsonPropertyName("animation_key")]
        public string AnimationKey { get; set; } = string.Empty;

        [JsonPropertyName("vfx_key")]
        public string VfxKey { get; set; } = string.Empty;

        [JsonPropertyName("audio_key")]
        public string AudioKey { get; set; } = string.Empty;

        [JsonPropertyName("camera_sequence_key")]
        public string CameraSequenceKey { get; set; } = string.Empty;

        [JsonPropertyName("return_sequence_key")]
        public string ReturnSequenceKey { get; set; } = string.Empty;
    }

    public sealed class SummonSystemRules
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("available_in_vertical_slice")]
        public bool AvailableInVerticalSlice { get; set; }

        [JsonPropertyName("default_resource")]
        public string DefaultResource { get; set; } = "mp";

        [JsonPropertyName("allow_multiple_summons_per_battle")]
        public bool AllowMultipleSummonsPerBattle { get; set; }

        [JsonPropertyName("cinematic_sequence_required")]
        public bool CinematicSequenceRequired { get; set; }

        [JsonPropertyName("enemy_hp_visibility_unchanged")]
        public bool EnemyHpVisibilityUnchanged { get; set; }
    }
}
