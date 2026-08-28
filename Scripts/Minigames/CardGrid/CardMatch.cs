using System;
using System.Collections.Generic;
using System.Linq;

namespace FF7Nostalgia.Core.Minigames.CardGrid
{
    public sealed class CardMatch
    {
        private readonly CardInstance?[,] _board = new CardInstance?[3, 3];
        private readonly List<CardInstance> _playerOneHand;
        private readonly List<CardInstance> _playerTwoHand;
        private int _placements;

        public CardOwner CurrentTurn { get; private set; }
        public CardRuleSet Rules { get; }
        public CardMatchResult Result { get; private set; } = CardMatchResult.InProgress;

        public CardMatch(IEnumerable<CardDefinition> playerOneDeck, IEnumerable<CardDefinition> playerTwoDeck,
            CardRuleSet? rules = null, CardOwner startingPlayer = CardOwner.PlayerOne)
        {
            _playerOneHand = BuildHand(playerOneDeck, CardOwner.PlayerOne);
            _playerTwoHand = BuildHand(playerTwoDeck, CardOwner.PlayerTwo);
            Rules = rules ?? new CardRuleSet();
            CurrentTurn = startingPlayer;
        }

        public CardInstance? GetCard(int row, int column)
        {
            ValidatePosition(row, column);
            return _board[row, column];
        }

        public IReadOnlyList<CardInstance> GetHand(CardOwner owner) =>
            owner == CardOwner.PlayerOne ? _playerOneHand : _playerTwoHand;

        public CardMoveResult PlayCard(CardOwner owner, string cardId, int row, int column)
        {
            if (Result != CardMatchResult.InProgress)
                throw new InvalidOperationException("The match is already complete.");
            if (owner != CurrentTurn)
                throw new InvalidOperationException("It is not that player's turn.");

            ValidatePosition(row, column);
            if (_board[row, column] is not null)
                throw new InvalidOperationException("That board position is occupied.");

            var hand = owner == CardOwner.PlayerOne ? _playerOneHand : _playerTwoHand;
            var card = hand.FirstOrDefault(x => x.Definition.Id == cardId)
                ?? throw new InvalidOperationException($"Card '{cardId}' is not in the active hand.");

            hand.Remove(card);
            _board[row, column] = card;
            _placements++;

            var captured = new HashSet<CardInstance>();
            var specialCaptures = new List<BoardPosition>();
            var same = Rules.Same && ApplySame(card, row, column, captured, specialCaptures);
            var plus = Rules.Plus && ApplyPlus(card, row, column, captured, specialCaptures);
            ApplyNormalCaptures(card, row, column, captured);

            var combo = false;
            if (Rules.Combo && specialCaptures.Count > 0)
                combo = ApplyCombo(owner, specialCaptures, captured);

            Result = EvaluateResult();
            if (Result == CardMatchResult.InProgress)
                CurrentTurn = owner == CardOwner.PlayerOne ? CardOwner.PlayerTwo : CardOwner.PlayerOne;

            return new CardMoveResult
            {
                Position = new BoardPosition(row, column),
                PlacedCard = card,
                CapturedCards = captured.ToArray(),
                TriggeredSame = same,
                TriggeredPlus = plus,
                TriggeredCombo = combo,
                MatchResult = Result
            };
        }

        public (int PlayerOne, int PlayerTwo) GetScore()
        {
            var p1 = _playerOneHand.Count;
            var p2 = _playerTwoHand.Count;
            foreach (var card in _board)
            {
                if (card is null) continue;
                if (card.Owner == CardOwner.PlayerOne) p1++;
                else p2++;
            }
            return (p1, p2);
        }

        private bool ApplySame(CardInstance placed, int row, int col, HashSet<CardInstance> captured,
            List<BoardPosition> specialCaptures)
        {
            var matches = Neighbors(row, col)
                .Where(n => GetRank(placed.Definition, n.Direction) == GetOpposingRank(n.Card.Definition, n.Direction))
                .ToArray();

            if (matches.Length < 2) return false;
            foreach (var match in matches)
                CaptureIfOpponent(placed.Owner, match.Card, match.Position, captured, specialCaptures);
            return true;
        }

        private bool ApplyPlus(CardInstance placed, int row, int col, HashSet<CardInstance> captured,
            List<BoardPosition> specialCaptures)
        {
            var groups = Neighbors(row, col)
                .Select(n => new
                {
                    Neighbor = n,
                    Sum = GetRank(placed.Definition, n.Direction) + GetOpposingRank(n.Card.Definition, n.Direction)
                })
                .GroupBy(x => x.Sum)
                .Where(g => g.Count() >= 2)
                .ToArray();

            if (groups.Length == 0) return false;
            foreach (var group in groups)
            foreach (var match in group)
                CaptureIfOpponent(placed.Owner, match.Neighbor.Card, match.Neighbor.Position, captured, specialCaptures);
            return true;
        }

        private void ApplyNormalCaptures(CardInstance placed, int row, int col, HashSet<CardInstance> captured)
        {
            foreach (var neighbor in Neighbors(row, col))
            {
                if (neighbor.Card.Owner == placed.Owner) continue;
                if (GetRank(placed.Definition, neighbor.Direction) <= GetOpposingRank(neighbor.Card.Definition, neighbor.Direction))
                    continue;
                neighbor.Card.Capture(placed.Owner);
                captured.Add(neighbor.Card);
            }
        }

        private bool ApplyCombo(CardOwner owner, IEnumerable<BoardPosition> startingPositions,
            HashSet<CardInstance> captured)
        {
            var queue = new Queue<BoardPosition>(startingPositions.Distinct());
            var visited = new HashSet<BoardPosition>();
            var triggered = false;

            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                if (!visited.Add(pos)) continue;
                var source = _board[pos.Row, pos.Column];
                if (source is null || source.Owner != owner) continue;

                foreach (var neighbor in Neighbors(pos.Row, pos.Column))
                {
                    if (neighbor.Card.Owner == owner) continue;
                    if (GetRank(source.Definition, neighbor.Direction) <= GetOpposingRank(neighbor.Card.Definition, neighbor.Direction))
                        continue;

                    neighbor.Card.Capture(owner);
                    captured.Add(neighbor.Card);
                    queue.Enqueue(neighbor.Position);
                    triggered = true;
                }
            }

            return triggered;
        }

        private CardMatchResult EvaluateResult()
        {
            if (_placements < 9) return CardMatchResult.InProgress;
            var score = GetScore();
            if (score.PlayerOne > score.PlayerTwo) return CardMatchResult.PlayerOneWin;
            if (score.PlayerTwo > score.PlayerOne) return CardMatchResult.PlayerTwoWin;
            return CardMatchResult.Draw;
        }

        private void CaptureIfOpponent(CardOwner owner, CardInstance target, BoardPosition position,
            HashSet<CardInstance> captured, List<BoardPosition> specialCaptures)
        {
            if (target.Owner == owner) return;
            target.Capture(owner);
            captured.Add(target);
            specialCaptures.Add(position);
        }

        private IEnumerable<Neighbor> Neighbors(int row, int col)
        {
            if (row > 0 && _board[row - 1, col] is { } north)
                yield return new Neighbor(north, new BoardPosition(row - 1, col), Direction.North);
            if (col < 2 && _board[row, col + 1] is { } east)
                yield return new Neighbor(east, new BoardPosition(row, col + 1), Direction.East);
            if (row < 2 && _board[row + 1, col] is { } south)
                yield return new Neighbor(south, new BoardPosition(row + 1, col), Direction.South);
            if (col > 0 && _board[row, col - 1] is { } west)
                yield return new Neighbor(west, new BoardPosition(row, col - 1), Direction.West);
        }

        private static int GetRank(CardDefinition card, Direction direction) => direction switch
        {
            Direction.North => card.North,
            Direction.East => card.East,
            Direction.South => card.South,
            Direction.West => card.West,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        private static int GetOpposingRank(CardDefinition card, Direction direction) => direction switch
        {
            Direction.North => card.South,
            Direction.East => card.West,
            Direction.South => card.North,
            Direction.West => card.East,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

        private static List<CardInstance> BuildHand(IEnumerable<CardDefinition> deck, CardOwner owner)
        {
            var cards = deck?.ToList() ?? throw new ArgumentNullException(nameof(deck));
            if (cards.Count != 5)
                throw new ArgumentException("Each player must bring exactly five cards.", nameof(deck));
            return cards.Select(x => new CardInstance(x, owner)).ToList();
        }

        private static void ValidatePosition(int row, int column)
        {
            if (row is < 0 or > 2) throw new ArgumentOutOfRangeException(nameof(row));
            if (column is < 0 or > 2) throw new ArgumentOutOfRangeException(nameof(column));
        }

        private enum Direction { North, East, South, West }
        private readonly record struct Neighbor(CardInstance Card, BoardPosition Position, Direction Direction);
    }
}
