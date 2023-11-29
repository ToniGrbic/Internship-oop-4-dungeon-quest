using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Constants;

namespace Domain.Repositories
{
    public class Witch:Enemy
    {
        public Witch()
        {
            this.HP = (int)EnemiesHP.Witch;
            this.Damage = (int)EnemiesDamage.Witch;
            this.Type = "Witch";    
        }
    
    }
}
