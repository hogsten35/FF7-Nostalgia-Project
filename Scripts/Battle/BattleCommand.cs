namespace FF7Nostalgia.Core.Battle
{
    public enum BattleCommandType
    {
        Attack,
        Magic,
        Item,
        Defend
    }

    public sealed class BattleCommand
    {
        public string Id { get; }
        public string DisplayName { get; }
        public BattleCommandType Type { get; }
        public int MPCost { get; }
        public float Power { get; }
        public bool UsesMagicStat { get; }
        public bool TargetsAll { get; }

        public BattleCommand(string id, string displayName, BattleCommandType type,
            float power = 1f, int mpCost = 0, bool usesMagicStat = false, bool targetsAll = false)
        {
            Id = id;
            DisplayName = displayName;
            Type = type;
            Power = power;
            MPCost = mpCost;
            UsesMagicStat = usesMagicStat;
            TargetsAll = targetsAll;
        }

        public static BattleCommand Attack() => new("attack", "Attack", BattleCommandType.Attack, 1f);
        public static BattleCommand Defend() => new("defend", "Defend", BattleCommandType.Defend, 0f);
        public static BattleCommand Fire() => new("fire", "Fire", BattleCommandType.Magic, 1.35f, 4, true);
        public static BattleCommand Cure() => new("cure", "Cure", BattleCommandType.Magic, 1.15f, 5, true);
    }
}
