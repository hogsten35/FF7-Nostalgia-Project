using System;
using System.IO;
using System.Text.Json;

namespace FF7Nostalgia.Core.Summons
{
    public static class SummonCatalogLoader
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static SummonCatalog Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Summon catalog not found.", path);

            var json = File.ReadAllText(path);
            var catalog = JsonSerializer.Deserialize<SummonCatalog>(json, JsonOptions)
                ?? throw new InvalidOperationException("Summon catalog could not be parsed.");

            Validate(catalog);
            return catalog;
        }

        private static void Validate(SummonCatalog catalog)
        {
            if (!catalog.SystemRules.EnemyHpVisibilityUnchanged)
                throw new InvalidOperationException("Summon data cannot override the hidden enemy HP rule.");

            if (catalog.SystemRules.AvailableInVerticalSlice && catalog.Summons.Count > 0)
                throw new InvalidOperationException("Summons are intentionally unavailable in the opening vertical slice.");

            foreach (var pair in catalog.Summons)
            {
                var summon = pair.Value;
                if (string.IsNullOrWhiteSpace(summon.SummonId))
                    throw new InvalidOperationException($"Summon '{pair.Key}' is missing summon_id.");
                if (summon.Power < 0f)
                    throw new InvalidOperationException($"Summon '{summon.Name}' has invalid power.");
                if (summon.ResourceCost < 0)
                    throw new InvalidOperationException($"Summon '{summon.Name}' has invalid resource cost.");
            }
        }
    }
}
