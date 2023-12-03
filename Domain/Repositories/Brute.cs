
using Data.Constants;

namespace Domain.Repositories
{
    public class Brute:Enemy
    {
        public Brute()
        {
            this.HPThreshold = (int)EnemiesHP.Brute;
            this.HP = HPThreshold;
            this.XP = (int)EnemiesXP.Brute;
            this.Damage = (int)EnemiesDamage.Brute;
            this.Type = "Brute";
        }
    }
}
