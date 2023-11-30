
using Data.Constants;

namespace Domain.Repositories
{
    public class Goblin : Enemy
    {
        public Goblin()
        {
            this.HP = (int)EnemiesHP.Goblin;
            this.Damage = (int)EnemiesDamage.Goblin;
            this.Type = "Goblin";
        }
    }
}
