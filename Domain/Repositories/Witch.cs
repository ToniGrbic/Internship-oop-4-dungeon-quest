using Data.Constants;

namespace Domain.Repositories
{
    public class Witch:Enemy
    {
        public Witch()
        {
            this.HP = (int)EnemiesHP.Witch;
            this.XP = (int)EnemiesXP.Witch;
            this.Damage = (int)EnemiesDamage.Witch;
            this.Type = "Witch";    
        }
    
    }
}
