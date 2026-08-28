using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FF7Nostalgia.Core.Minigames.CardGrid
{
    public sealed class CardGridConfig
    {
        [JsonPropertyName("game_id")]
        public string GameId { get; set; } = string.Empty;

        [JsonPropertyName("working_title")]
        public string WorkingTitle { get; set; } = string.Empty;

        [JsonPropertyName("board_width")]
        public int BoardWidth { get; set; }

        [JsonPropertyName("board_height")]
        public int BoardHeight { get; set; }

        [JsonPropertyName("hand_size")]
        public int HandSize { get; set; }

        [JsonPropertyName("rank_min")]
        public int RankMin { get; set; }

        [JsonPropertyName("rank_max")]
        public int RankMax { get; set; }

        [JsonPropertyName("available_in_vertical_slice")]
        public bool AvailableInVerticalSlice { get; set; }

        [JsonPropertyName("default_rules")]
        public CardGridRuleConfig DefaultRules { get; set; } = new();
    }

    public sealed class CardGridRuleConfig
    {
        [JsonPropertyName("open_hands")]
        public bool OpenHands { get; set; }

        [JsonPropertyName("random_hand")]
        public bool RandomHand { get; set; }

        [JsonPropertyName("same")]
        public bool Same { get; set; }

        [JsonPropertyName("plus")]
        public bool Plus { get; set; }

        [JsonPropertyName("combo")]
        public bool Combo { get; set; }

        [JsonPropertyName("sudden_death")]
        public bool SuddenDeath { get; set; }

        [JsonPropertyName("elemental")]
        public bool Elemental { get; set; }

        public CardRuleSet ToRuleSet() => new()
        {
            OpenHands = OpenHands,
            RandomHand = RandomHand,
            Same = Same,
            Plus = Plus,
            Combo = Combo,
            SuddenDeath = SuddenDeath,
            Elemental = Elemental
        };
    }

    public static class CardGridConfigLoader
    {
        public static CardGridConfig Load(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Card-grid rules file was not found.", path);

            var config = JsonSerializer.Deserialize<CardGridConfig>(File.ReadAllText(path),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new InvalidDataException("Could not deserialize card-grid rules.");

            if (config.BoardWidth != 3 || config.BoardHeight != 3 || config.HandSize != 5)
                throw new InvalidDataException("Card Grid currently requires a 3x3 board and five-card hands.");
            if (config.RankMin != 1 || config.RankMax != 10)
                throw new InvalidDataException("Card Grid currently requires ranks from 1 through 10.");
            if (config.AvailableInVerticalSlice)
                throw new InvalidDataException("Card Grid is intentionally excluded from the opening vertical slice.");

            return config;
        }
    }
}
