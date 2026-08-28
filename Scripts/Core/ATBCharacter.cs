using System;

namespace FF7Nostalgia.Core.Battle
{
    /// <summary>
    /// Engine-agnostic battle participant used by the ATB system.
    /// This class intentionally has no UnityEngine dependency.
    /// </summary>
    public sealed class ATBCharacter
    {
        public string Name { get; }
        public float Speed { get; set; }
        public float CurrentATB { get; private set; }
        public float MaxATB { get; }

        public bool IsTurnReady => CurrentATB >= MaxATB;

        public ATBCharacter(string name, float speed, float maxATB = 100f)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Character name is required.", nameof(name));
            if (speed < 0f)
                throw new ArgumentOutOfRangeException(nameof(speed));
            if (maxATB <= 0f)
                throw new ArgumentOutOfRangeException(nameof(maxATB));

            Name = name;
            Speed = speed;
            MaxATB = maxATB;
            CurrentATB = 0f;
        }

        internal bool Advance(float deltaTime)
        {
            if (deltaTime < 0f)
                throw new ArgumentOutOfRangeException(nameof(deltaTime));

            if (IsTurnReady)
                return false;

            CurrentATB = Math.Min(MaxATB, CurrentATB + (Speed * deltaTime));
            return IsTurnReady;
        }

        /// <summary>
        /// Call after this character completes an action.
        /// </summary>
        public void ConsumeTurn()
        {
            CurrentATB = 0f;
        }

        public void SetATB(float value)
        {
            CurrentATB = Math.Clamp(value, 0f, MaxATB);
        }
    }
}
