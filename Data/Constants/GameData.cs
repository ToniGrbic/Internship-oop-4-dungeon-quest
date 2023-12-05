
namespace Data.Constants
{
    public class Constants
    {
        public static readonly int NUMBER_OF_DUNGEON_WAVES = 10;

        public static Dictionary<int, AttackType> Attacks = new()
        {
            {1, AttackType.DIRECT},
            {2, AttackType.SIDE},
            {3, AttackType.COUNTER}
        };

        public static Dictionary<AttackType, string> AtkString = new()
        {
            {AttackType.DIRECT, "Direct"},
            {AttackType.SIDE, "Side"},
            {AttackType.COUNTER, "Counter"}
        };

        public static readonly int HP_THRESHOLD_PER_LVL = 25;

        public static readonly int XP_THRESHOLD = 100;

        public static readonly int DAMAGE_PER_LVL = 10;

        public static readonly int MANA_INCREASE_PER_LVL = 10;

        public static readonly int MANA_AMOUNT_ENCHANTER = 100;

        public static readonly float HP_PERCENT_REGAIN = 0.25f;

        public static readonly float CRIT_CHANCE_PER_LVL = 0.05f;

        public static readonly float STUN_CHANCE_PER_LVL = 0.05f;
    }
    public enum GameLoop
    {
        CONTINUE,
        EXIT
    }
    public enum GameState
    {
        IN_PROGRESS,    
        WIN,
        LOSS,
    }

    public enum AttackType
    {
        DIRECT,
        SIDE,
        COUNTER
    }

    public enum CombatOutcome
    {
        WIN,
        LOSE,
        DRAW
    }
}
