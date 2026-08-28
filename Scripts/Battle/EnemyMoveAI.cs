using System;
using System.Collections.Generic;
using System.Linq;
using FF7Nostalgia.Core.Data;

namespace FF7Nostalgia.Core.Battle
{
    public sealed class EnemyMoveAI
    {
        private readonly Random _random;

        public EnemyMoveAI(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public MoveDefinition ChooseMove(EnemyDefinition definition, BattleActor self,
            IReadOnlyList<BattleActor> allies)
        {
            if (definition.Moves.Count == 0)
                throw new InvalidOperationException($"Enemy '{definition.Name}' has no moves defined.");

            var applicable = definition.Moves
                .Where(move => IsConditionMet(move.Condition, self, allies))
                .ToArray();

            if (applicable.Length == 0)
                applicable = definition.Moves.Where(move => string.IsNullOrWhiteSpace(move.Condition) || move.Condition == "default").ToArray();

            if (applicable.Length == 0)
                applicable = definition.Moves.ToArray();

            var totalWeight = applicable.Sum(move => Math.Max(0.0, move.ActionProbability));
            if (totalWeight <= 0)
                return applicable[0];

            var roll = _random.NextDouble() * totalWeight;
            foreach (var move in applicable)
            {
                roll -= Math.Max(0.0, move.ActionProbability);
                if (roll <= 0)
                    return move;
            }

            return applicable[^1];
        }

        private static bool IsConditionMet(string condition, BattleActor self, IReadOnlyList<BattleActor> allies)
        {
            if (string.IsNullOrWhiteSpace(condition) || condition == "default")
                return true;

            if (condition == "in_group_2_plus")
                return allies.Count(actor => actor.IsAlive) >= 2;

            if (condition == "allies_alive")
                return allies.Any(actor => actor.IsAlive && actor != self);

            if (condition.StartsWith("hp_below_", StringComparison.OrdinalIgnoreCase) && condition.EndsWith("_percent", StringComparison.OrdinalIgnoreCase))
            {
                var numeric = condition[9..^8];
                if (int.TryParse(numeric, out var percent))
                    return self.CurrentHP * 100 < self.MaxHP * percent;
            }

            // Conditions such as 'spotted' are controlled by field/encounter context later.
            return false;
        }
    }
}
