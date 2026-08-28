using System;
using FF7Nostalgia.Core.Battle;

namespace FF7Nostalgia.Core.Summons
{
    public static class SummonCommandFactory
    {
        public static BattleCommand Create(SummonDefinition summon)
        {
            if (!string.Equals(summon.ResourceType, "mp", StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException($"Summon resource '{summon.ResourceType}' is not implemented yet.");

            var effect = summon.Effect.ToLowerInvariant() switch
            {
                "damage" => BattleCommandEffect.Damage,
                "heal" => BattleCommandEffect.Heal,
                "status" => BattleCommandEffect.Status,
                _ => BattleCommandEffect.None
            };

            return new BattleCommand(
                summon.SummonId,
                summon.Name,
                BattleCommandType.Summon,
                summon.Power,
                summon.ResourceCost,
                usesMagicStat: true,
                targetsAll: summon.Target == "all_enemies" || summon.Target == "all_allies",
                effect: effect,
                element: summon.Element);
        }
    }
}
