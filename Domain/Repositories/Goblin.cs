
using Data.Constants;

namespace Domain.Repositories
{
    public class Goblin : Enemy
    {
        public Goblin()
        {
            this.HPThreshold = (int)EnemyHP.Goblin;
            this.HP = HPThreshold;
            this.XP = (int)EnemyXP.Goblin;
            this.Damage = (int)EnemyDamage.Goblin;
            this.Type = "Goblin";
        }
    }
}
