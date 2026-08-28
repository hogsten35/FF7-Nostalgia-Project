using System;

namespace FF7Nostalgia.Core.Battle
{
    public sealed class DamageResolver
    {
        private readonly Random _random;

        public DamageResolver(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public int CalculateDamage(BattleActor attacker, BattleActor defender, BattleCommand command)
        {
            var attackStat = command.UsesMagicStat ? attacker.Magic : attacker.Strength;
            var defenseStat = command.UsesMagicStat ? defender.MagicDefense : defender.Defense;
            var baseDamage = Math.Max(1, (attackStat * command.Power * 2f) - defenseStat);
            var variance = 0.90f + ((float)_random.NextDouble() * 0.20f);
            var critical = command.Type == BattleCommandType.Attack && _random.NextDouble() < 0.05;
            var damage = baseDamage * variance * (critical ? 2f : 1f);
            return Math.Max(1, (int)MathF.Round(damage));
        }

        public int CalculateHealing(BattleActor caster, BattleCommand command)
        {
            var baseHealing = caster.Magic * command.Power * 3f;
            var variance = 0.95f + ((float)_random.NextDouble() * 0.10f);
            return Math.Max(1, (int)MathF.Round(baseHealing * variance));
        }
    }
}
