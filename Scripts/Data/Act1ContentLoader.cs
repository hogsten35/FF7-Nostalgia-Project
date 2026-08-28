using System;
using System.IO;
using System.Text.Json;

namespace FF7Nostalgia.Core.Data
{
    public static class Act1ContentLoader
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        public static Act1ContentBundle LoadFromDirectory(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory))
                throw new ArgumentException("Act 1 content directory is required.", nameof(directory));

            string PathFor(string fileName) => Path.Combine(directory, fileName);

            var characters = LoadTyped<CharacterDefinitionsRoot>(PathFor("character_definitions.json"));
            var enemies = LoadTyped<EnemyDefinitionsRoot>(PathFor("enemy_definitions.json"));
            var bosses = LoadTyped<BossDefinitionsRoot>(PathFor("boss_encounters.json"));

            ValidatePresentationRules(enemies.PresentationRules);

            return new Act1ContentBundle
            {
                Characters = characters,
                Enemies = enemies,
                Bosses = bosses,
                Dialogue = LoadDocument(PathFor("dialogue_trees.json")),
                Objectives = LoadDocument(PathFor("objectives_flow.json")),
                Loot = LoadDocument(PathFor("loot_tables.json")),
                SceneManifest = LoadDocument(PathFor("scene_manifest.json"))
            };
        }

        private static T LoadTyped<T>(string path)
        {
            EnsureFile(path);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json, Options)
                   ?? throw new InvalidDataException($"Could not deserialize content file: {path}");
        }

        private static JsonDocument LoadDocument(string path)
        {
            EnsureFile(path);
            return JsonDocument.Parse(File.ReadAllText(path), new JsonDocumentOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Skip
            });
        }

        private static void EnsureFile(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Required Echoes Act 1 content file was not found.", path);
        }

        private static void ValidatePresentationRules(PresentationRules rules)
        {
            if (rules.ShowEnemyHPNumbers || rules.ShowEnemyHPPercentages || rules.ShowEnemyHealthBars)
                throw new InvalidDataException("Echoes canon forbids exposing enemy or boss HP in the player-facing UI.");
        }
    }
}
