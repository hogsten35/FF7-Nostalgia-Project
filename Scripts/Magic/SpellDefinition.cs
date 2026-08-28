using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FF7Nostalgia.Core.Magic
{
    public sealed class SpellDefinitionsRoot
    {
        [JsonPropertyName("spells")]
        public Dictionary<string, SpellDefinition> Spells { get; set; } = new();
    }

    public sealed class SpellDefinition
    {
        [JsonPropertyName("spell_id")]
        public string SpellId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("school")]
        public string School { get; set; } = string.Empty;

        [JsonPropertyName("family")]
        public string Family { get; set; } = string.Empty;

        [JsonPropertyName("tier")]
        public int Tier { get; set; }

        [JsonPropertyName("mp_cost")]
        public int MPCost { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; } = string.Empty;

        [JsonPropertyName("effect_type")]
        public string EffectType { get; set; } = string.Empty;

        [JsonPropertyName("element")]
        public string Element { get; set; } = string.Empty;

        [JsonPropertyName("power")]
        public float Power { get; set; }

        [JsonPropertyName("unlock_level")]
        public int UnlockLevel { get; set; }

        [JsonPropertyName("status_effect")]
        public string StatusEffect { get; set; } = string.Empty;

        [JsonPropertyName("implemented")]
        public bool Implemented { get; set; }
    }
}
