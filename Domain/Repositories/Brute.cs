
using Data.Constants;

namespace Domain.Repositories
{
    internal class Brute:Enemy
    {
        public Brute()
        {
            this.HP = (int)EnemiesHP.Brute;
            this.Damage = (int)EnemiesDamage.Brute;
            this.Type = "Brute";
        }
    }
}
