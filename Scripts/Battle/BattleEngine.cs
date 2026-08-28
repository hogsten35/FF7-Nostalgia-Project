using System;
using System.Collections.Generic;
using System.Linq;
using FF7Nostalgia.Core.Data;

namespace FF7Nostalgia.Core.Battle
{
    public enum BattleResult
    {
        InProgress,
        Victory,
        Defeat
    }

    public sealed class BattleEngine
    {
        private readonly DamageResolver _damageResolver;

        public IReadOnlyList<BattleActor> Players { get; }
        public IReadOnlyList<BattleActor> Enemies { get; }
        public BattleResult Result { get; private set; } = BattleResult.InProgress;

        public event Action<string>? OnBattleLog;
        public event Action<BattleActor, BattleActor, int>? OnDamage;
        public event Action<BattleActor, int>? OnHeal;
        public event Action<BattleResult>? OnBattleEnded;

        public BattleEngine(IEnumerable<BattleActor> players, IEnumerable<BattleActor> enemies,
            DamageResolver? damageResolver = null)
        {
            Players = players.ToList();
            Enemies = enemies.ToList();
            _damageResolver = damageResolver ?? new DamageResolver();

            if (Players.Count == 0) throw new ArgumentException("At least one player actor is required.");
            if (Enemies.Count == 0) throw new ArgumentException("At least one enemy actor is required.");
        }

        public bool Execute(BattleActor actor, BattleCommand command, BattleActor target)
        {
            if (Result != BattleResult.InProgress || !actor.IsAlive)
                return false;

            if (command.MPCost > 0 && actor.CurrentMP < command.MPCost)
            {
                OnBattleLog?.Invoke($"{actor.Name} does not have enough MP.");
                return false;
            }

            if (command.Type != BattleCommandType.Defend && !target.IsTargetable && command.Effect != BattleCommandEffect.Revive)
                return false;

            actor.BeginTurn();

            if (command.MPCost > 0 && !actor.SpendMP(command.MPCost))
                return false;

            switch (command.Type)
            {
                case BattleCommandType.Defend:
                    actor.Defend();
                    OnBattleLog?.Invoke($"{actor.Name} defends.");
                    break;

                case BattleCommandType.Magic when command.Effect == BattleCommandEffect.Heal:
                    var healing = _damageResolver.CalculateHealing(actor, command);
                    target.Heal(healing);
                    OnHeal?.Invoke(target, healing);
                    OnBattleLog?.Invoke($"{actor.Name} casts {command.DisplayName}. {target.Name} recovers {healing} HP.");
                    break;

                case BattleCommandType.Magic when command.Effect is BattleCommandEffect.Status or BattleCommandEffect.Revive:
                    OnBattleLog?.Invoke($"{command.DisplayName} is defined but this effect is not executable yet.");
                    return false;

                default:
                    var damage = _damageResolver.CalculateDamage(actor, target, command);
                    target.ApplyDamage(damage);
                    OnDamage?.Invoke(actor, target, damage);
                    OnBattleLog?.Invoke($"{actor.Name} uses {command.DisplayName} on {target.Name} for {damage} damage.");
                    break;
            }

            EvaluateBattleState();
            return true;
        }

        public bool ExecuteEnemyMove(BattleActor actor, MoveDefinition move, BattleActor? target = null)
        {
            if (Result != BattleResult.InProgress || !actor.IsAlive)
                return false;

            actor.BeginTurn();

            if (move.Effect == "remove_from_combat_1_turn")
            {
                actor.RemoveFromCombatForTurns(1);
                OnBattleLog?.Invoke($"{actor.Name} uses {move.Name} and disappears into the smoke.");
                return true;
            }

            if (move.Effect == "reduce_damage_50_percent_1_turn")
            {
                actor.Defend();
                OnBattleLog?.Invoke($"{actor.Name} uses {move.Name} and takes cover.");
                return true;
            }

            if (target is null || !target.IsTargetable)
                return false;

            if (move.Damage > 0)
            {
                target.ApplyDamage(move.Damage);
                OnDamage?.Invoke(actor, target, move.Damage);
                OnBattleLog?.Invoke($"{actor.Name} uses {move.Name} on {target.Name} for {move.Damage} damage.");
                EvaluateBattleState();
                return true;
            }

            OnBattleLog?.Invoke($"{actor.Name} uses {move.Name}.");
            return true;
        }

        private void EvaluateBattleState()
        {
            if (Enemies.All(x => !x.IsAlive))
            {
                Result = BattleResult.Victory;
                OnBattleEnded?.Invoke(Result);
            }
            else if (Players.All(x => !x.IsAlive))
            {
                Result = BattleResult.Defeat;
                OnBattleEnded?.Invoke(Result);
            }
        }
    }
}
