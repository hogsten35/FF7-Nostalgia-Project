using System;
using System.IO;
using System.Text.Json;

namespace FF7Nostalgia.Core.Magic
{
    public sealed class SpellCatalog
    {
        private readonly SpellDefinitionsRoot _root;

        private SpellCatalog(SpellDefinitionsRoot root)
        {
            _root = root;
        }

        public static SpellCatalog Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Spell definition file not found.", path);

            var json = File.ReadAllText(path);
            var root = JsonSerializer.Deserialize<SpellDefinitionsRoot>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new InvalidOperationException("Could not parse spell definitions.");

            if (root.Spells.Count == 0)
                throw new InvalidOperationException("Spell catalog contains no spells.");

            return new SpellCatalog(root);
        }

        public SpellDefinition Get(string spellId)
        {
            if (!_root.Spells.TryGetValue(spellId, out var spell))
                throw new KeyNotFoundException($"Unknown spell '{spellId}'.");

            return spell;
        }

        public IReadOnlyDictionary<string, SpellDefinition> All => _root.Spells;
    }
}
