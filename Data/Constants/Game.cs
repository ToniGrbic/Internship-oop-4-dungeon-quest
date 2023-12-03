
namespace Data.Constants
{
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
