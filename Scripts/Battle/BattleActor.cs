using System;

namespace FF7Nostalgia.Core.Battle
{
    public sealed class BattleActor
    {
        public string Id { get; }
        public string Name { get; }
        public bool IsPlayerControlled { get; }
        public int MaxHP { get; }
        public int CurrentHP { get; private set; }
        public int MaxMP { get; }
        public int CurrentMP { get; private set; }
        public int Strength { get; }
        public int Defense { get; }
        public int Magic { get; }
        public int MagicDefense { get; }
        public int Speed { get; }
        public bool IsDefending { get; private set; }
        public bool IsAlive => CurrentHP > 0;

        public BattleActor(string id, string name, bool isPlayerControlled, int maxHP, int maxMP,
            int strength, int defense, int magic, int magicDefense, int speed)
        {
            if (maxHP <= 0) throw new ArgumentOutOfRangeException(nameof(maxHP));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            IsPlayerControlled = isPlayerControlled;
            MaxHP = maxHP;
            CurrentHP = maxHP;
            MaxMP = Math.Max(0, maxMP);
            CurrentMP = MaxMP;
            Strength = Math.Max(1, strength);
            Defense = Math.Max(0, defense);
            Magic = Math.Max(1, magic);
            MagicDefense = Math.Max(0, magicDefense);
            Speed = Math.Max(1, speed);
        }

        public void ApplyDamage(int amount)
        {
            var finalAmount = IsDefending ? Math.Max(1, amount / 2) : Math.Max(0, amount);
            CurrentHP = Math.Max(0, CurrentHP - finalAmount);
        }

        public void Heal(int amount) => CurrentHP = Math.Min(MaxHP, CurrentHP + Math.Max(0, amount));

        public bool SpendMP(int amount)
        {
            if (amount < 0 || CurrentMP < amount) return false;
            CurrentMP -= amount;
            return true;
        }

        public void BeginTurn() => IsDefending = false;
        public void Defend() => IsDefending = true;
    }
}
