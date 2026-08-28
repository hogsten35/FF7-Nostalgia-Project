using System;
using System.Collections.Generic;
using System.Linq;

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

            actor.BeginTurn();

            switch (command.Type)
            {
                case BattleCommandType.Defend:
                    actor.Defend();
                    OnBattleLog?.Invoke($"{actor.Name} defends.");
                    break;

                case BattleCommandType.Magic when command.Id == "cure":
                    if (!actor.SpendMP(command.MPCost))
                    {
                        OnBattleLog?.Invoke($"{actor.Name} does not have enough MP.");
                        return false;
                    }
                    var healing = _damageResolver.CalculateHealing(actor, command);
                    target.Heal(healing);
                    OnHeal?.Invoke(target, healing);
                    OnBattleLog?.Invoke($"{actor.Name} casts {command.DisplayName}. {target.Name} recovers {healing} HP.");
                    break;

                default:
                    if (command.MPCost > 0 && !actor.SpendMP(command.MPCost))
                    {
                        OnBattleLog?.Invoke($"{actor.Name} does not have enough MP.");
                        return false;
                    }
                    if (!target.IsAlive) return false;
                    var damage = _damageResolver.CalculateDamage(actor, target, command);
                    target.ApplyDamage(damage);
                    OnDamage?.Invoke(actor, target, damage);
                    OnBattleLog?.Invoke($"{actor.Name} uses {command.DisplayName} on {target.Name} for {damage} damage.");
                    break;
            }

            EvaluateBattleState();
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
