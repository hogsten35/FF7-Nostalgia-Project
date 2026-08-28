namespace FF7Nostalgia.Core.Battle
{
    public enum BattleCommandType
    {
        Attack,
        Magic,
        Item,
        Defend
    }

    public enum BattleCommandEffect
    {
        Damage,
        Heal,
        Status,
        Revive,
        None
    }

    public sealed class BattleCommand
    {
        public string Id { get; }
        public string DisplayName { get; }
        public BattleCommandType Type { get; }
        public BattleCommandEffect Effect { get; }
        public int MPCost { get; }
        public float Power { get; }
        public bool UsesMagicStat { get; }
        public bool TargetsAll { get; }
        public string Element { get; }

        public BattleCommand(string id, string displayName, BattleCommandType type,
            float power = 1f, int mpCost = 0, bool usesMagicStat = false, bool targetsAll = false,
            BattleCommandEffect effect = BattleCommandEffect.Damage, string element = "none")
        {
            Id = id;
            DisplayName = displayName;
            Type = type;
            Power = power;
            MPCost = mpCost;
            UsesMagicStat = usesMagicStat;
            TargetsAll = targetsAll;
            Effect = effect;
            Element = element;
        }

        public static BattleCommand Attack() => new("attack", "Attack", BattleCommandType.Attack, 1f);
        public static BattleCommand Defend() => new("defend", "Defend", BattleCommandType.Defend, 0f,
            effect: BattleCommandEffect.None);
        public static BattleCommand Fire() => new("fire", "Fire", BattleCommandType.Magic, 1.0f, 4, true,
            effect: BattleCommandEffect.Damage, element: "fire");
        public static BattleCommand Cure() => new("cure", "Cure", BattleCommandType.Magic, 1.0f, 4, true,
            effect: BattleCommandEffect.Heal);
    }
}
