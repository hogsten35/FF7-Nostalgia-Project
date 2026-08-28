using System;
using System.Collections.Generic;

namespace FF7Nostalgia.Core.Battle
{
    /// <summary>
    /// Pure C# Active Time Battle manager.
    /// A Unity-facing adapter can call Tick(Time.deltaTime) later.
    /// </summary>
    public sealed class ATBManager
    {
        private readonly List<ATBCharacter> _characters = new();

        public IReadOnlyList<ATBCharacter> Characters => _characters;

        /// <summary>
        /// Fired once when a participant crosses the ready threshold.
        /// The gauge remains full until ConsumeTurn is called on that character.
        /// </summary>
        public event Action<ATBCharacter>? OnTurnReady;

        public void AddCharacter(ATBCharacter character)
        {
            if (character == null)
                throw new ArgumentNullException(nameof(character));

            if (!_characters.Contains(character))
                _characters.Add(character);
        }

        public bool RemoveCharacter(ATBCharacter character)
        {
            return _characters.Remove(character);
        }

        public void Tick(float deltaTime)
        {
            if (deltaTime < 0f)
                throw new ArgumentOutOfRangeException(nameof(deltaTime));

            foreach (ATBCharacter character in _characters)
            {
                bool becameReady = character.Advance(deltaTime);
                if (becameReady)
                    OnTurnReady?.Invoke(character);
            }
        }

        public void ResetAllGauges()
        {
            foreach (ATBCharacter character in _characters)
                character.SetATB(0f);
        }
    }
}
