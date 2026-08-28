using System;
using FF7Nostalgia.Core.Battle;

namespace FF7Nostalgia.Core.Data
{
    public static class BattleActorFactory
    {
        public static BattleActor CreateCharacter(CharacterDefinition definition)
        {
            if (definition is null) throw new ArgumentNullException(nameof(definition));
            var stats = definition.BaseStats;

            return new BattleActor(
                id: definition.CharacterId,
                name: definition.Name,
                isPlayerControlled: definition.IsPlayerControlled,
                maxHP: stats.HP,
                maxMP: stats.MP,
                strength: stats.Attack,
                defense: stats.Defense,
                magic: stats.Magic,
                magicDefense: stats.MagicDefense,
                speed: stats.Speed);
        }

        public static BattleActor CreateEnemy(EnemyDefinition definition, string? instanceSuffix = null)
        {
            if (definition is null) throw new ArgumentNullException(nameof(definition));
            var stats = definition.BaseStats;
            var id = string.IsNullOrWhiteSpace(instanceSuffix)
                ? definition.EnemyId
                : $"{definition.EnemyId}_{instanceSuffix}";

            return new BattleActor(
                id: id,
                name: definition.Name,
                isPlayerControlled: false,
                maxHP: stats.HP,
                maxMP: stats.MP,
                strength: stats.Attack,
                defense: stats.Defense,
                magic: stats.Magic,
                magicDefense: stats.MagicDefense,
                speed: stats.Speed);
        }

        public static BattleActor CreateBoss(BossDefinition definition)
        {
            if (definition is null) throw new ArgumentNullException(nameof(definition));
            var stats = definition.BaseStats;

            return new BattleActor(
                id: definition.BossId,
                name: definition.BossName,
                isPlayerControlled: false,
                maxHP: stats.HP,
                maxMP: stats.MP,
                strength: stats.Attack,
                defense: stats.Defense,
                magic: stats.Magic,
                magicDefense: stats.MagicDefense,
                speed: stats.Speed);
        }
    }
}
