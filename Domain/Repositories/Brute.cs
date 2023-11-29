using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
