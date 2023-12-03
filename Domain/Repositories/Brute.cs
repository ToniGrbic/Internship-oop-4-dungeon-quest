
using Data.Constants;

namespace Domain.Repositories
{
    public class Brute:Enemy
    {
        public Brute()
        {
            this.HP = (int)EnemiesHP.Brute;
            this.XP = (int)EnemiesXP.Brute;
            this.Damage = (int)EnemiesDamage.Brute;
            this.Type = "Brute";
        }
    }
}
