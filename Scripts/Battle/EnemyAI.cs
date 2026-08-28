using System;

namespace FF7Nostalgia.Core.Battle
{
    public sealed class EnemyAI
    {
        private readonly Random _random;

        public EnemyAI(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public BattleCommand ChooseCommand(BattleActor self)
        {
            if (!self.IsAlive) return BattleCommand.Defend();

            // Vertical-slice behavior: mostly attack, occasional defend.
            // This is intentionally deterministic enough to tune later.
            return _random.NextDouble() < 0.85
                ? BattleCommand.Attack()
                : BattleCommand.Defend();
        }
    }
}
