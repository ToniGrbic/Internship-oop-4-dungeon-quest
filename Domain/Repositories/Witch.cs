using Data.Constants;

namespace Domain.Repositories
{
    public class Witch:Enemy
    {
        public Witch()
        {
            this.HPThreshold = (int)EnemiesHP.Witch;
            this.HP = HPThreshold;
            this.XP = (int)EnemiesXP.Witch;
            this.Damage = (int)EnemiesDamage.Witch;
            this.Type = "Witch";    
        }
    
    }
}
