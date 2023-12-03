
using Data.Constants;

namespace Domain.Repositories
{
    public class Goblin : Enemy
    {
        public Goblin()
        {
            this.HPThreshold = (int)EnemiesHP.Goblin;
            this.HP = HPThreshold;
            this.XP = (int)EnemiesXP.Goblin;
            this.Damage = (int)EnemiesDamage.Goblin;
            this.Type = "Goblin";
        }
    }
}
