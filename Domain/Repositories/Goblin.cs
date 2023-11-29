using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Constants;
namespace Domain.Repositories
{
    internal class Goblin : Enemy
    {
        public Goblin()
        {
            this.HP = (int)EnemiesHP.Goblin;
            this.Damage = (int)EnemiesDamage.Goblin;
            this.Type = "Goblin";
        }
    }
}
