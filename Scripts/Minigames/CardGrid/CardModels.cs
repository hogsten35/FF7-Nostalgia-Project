using System;
using System.Collections.Generic;

namespace FF7Nostalgia.Core.Minigames.CardGrid
{
    public enum CardOwner
    {
        PlayerOne,
        PlayerTwo
    }

    public sealed class CardDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public int North { get; }
        public int East { get; }
        public int South { get; }
        public int West { get; }
        public string Element { get; }
        public string Rarity { get; }

        public CardDefinition(string id, string name, int north, int east, int south, int west,
            string element = "none", string rarity = "common")
        {
            Id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentException("Card id is required.", nameof(id)) : id;
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Card name is required.", nameof(name)) : name;
            ValidateRank(north, nameof(north));
            ValidateRank(east, nameof(east));
            ValidateRank(south, nameof(south));
            ValidateRank(west, nameof(west));
            North = north;
            East = east;
            South = south;
            West = west;
            Element = element ?? "none";
            Rarity = rarity ?? "common";
        }

        private static void ValidateRank(int value, string name)
        {
            if (value < 1 || value > 10)
                throw new ArgumentOutOfRangeException(name, "Card ranks must be between 1 and 10.");
        }
    }

    public sealed class CardInstance
    {
        public CardDefinition Definition { get; }
        public CardOwner Owner { get; private set; }
        public CardOwner OriginalOwner { get; }

        public CardInstance(CardDefinition definition, CardOwner owner)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            Owner = owner;
            OriginalOwner = owner;
        }

        public void Capture(CardOwner newOwner) => Owner = newOwner;
    }

    public readonly record struct BoardPosition(int Row, int Column);

    public sealed class CardRuleSet
    {
        public bool OpenHands { get; init; }
        public bool RandomHand { get; init; }
        public bool Same { get; init; } = true;
        public bool Plus { get; init; } = true;
        public bool Combo { get; init; } = true;
        public bool SuddenDeath { get; init; }
        public bool Elemental { get; init; }
    }

    public enum CardMatchResult
    {
        InProgress,
        PlayerOneWin,
        PlayerTwoWin,
        Draw
    }

    public sealed class CardMoveResult
    {
        public BoardPosition Position { get; init; }
        public CardInstance PlacedCard { get; init; } = null!;
        public IReadOnlyList<CardInstance> CapturedCards { get; init; } = Array.Empty<CardInstance>();
        public bool TriggeredSame { get; init; }
        public bool TriggeredPlus { get; init; }
        public bool TriggeredCombo { get; init; }
        public CardMatchResult MatchResult { get; init; }
    }
}
