using System;
using System.Collections.Generic;

namespace FF7Nostalgia.Core.Battle
{
    public sealed class ATBBattleController
    {
        private readonly ATBManager _atbManager = new();
        private readonly Dictionary<ATBCharacter, BattleActor> _actorByGauge = new();

        public event Action<BattleActor>? OnActorReady;

        public ATBBattleController(IEnumerable<BattleActor> actors)
        {
            foreach (var actor in actors)
            {
                var gauge = new ATBCharacter(actor.Name, actor.Speed);
                _actorByGauge[gauge] = actor;
                _atbManager.AddCharacter(gauge);
            }

            _atbManager.OnTurnReady += HandleTurnReady;
        }

        public void Tick(float deltaTime)
        {
            _atbManager.Tick(deltaTime);
        }

        public float GetNormalizedGauge(BattleActor actor)
        {
            foreach (var pair in _actorByGauge)
            {
                if (ReferenceEquals(pair.Value, actor))
                    return pair.Key.MaxATB <= 0f ? 0f : pair.Key.CurrentATB / pair.Key.MaxATB;
            }

            return 0f;
        }

        public void ConsumeTurn(BattleActor actor)
        {
            foreach (var pair in _actorByGauge)
            {
                if (ReferenceEquals(pair.Value, actor))
                {
                    pair.Key.ConsumeTurn();
                    return;
                }
            }
        }

        private void HandleTurnReady(ATBCharacter gauge)
        {
            if (_actorByGauge.TryGetValue(gauge, out var actor) && actor.IsAlive)
                OnActorReady?.Invoke(actor);
        }
    }
}
