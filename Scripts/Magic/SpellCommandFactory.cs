using System;
using FF7Nostalgia.Core.Battle;

namespace FF7Nostalgia.Core.Magic
{
    public static class SpellCommandFactory
    {
        public static BattleCommand Create(SpellDefinition spell)
        {
            if (!spell.Implemented)
                throw new InvalidOperationException($"Spell '{spell.Name}' is defined but its effect is not implemented yet.");

            var effect = spell.EffectType switch
            {
                "damage" => BattleCommandEffect.Damage,
                "heal" => BattleCommandEffect.Heal,
                "status" => BattleCommandEffect.Status,
                "revive" => BattleCommandEffect.Revive,
                _ => BattleCommandEffect.None
            };

            var targetsAll = spell.Target is "all_enemies" or "all_allies";

            return new BattleCommand(
                id: spell.SpellId,
                displayName: spell.Name,
                type: BattleCommandType.Magic,
                power: spell.Power,
                mpCost: spell.MPCost,
                usesMagicStat: true,
                targetsAll: targetsAll,
                effect: effect,
                element: spell.Element);
        }
    }
}
