using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Constants;
namespace Domain.Repositories
{
    public class Enchanter : Hero
    {
        
        public Enchanter(string name, int XP) : base(name, XP)
        {
            this.HP = (int)HeroHP.Enchanter;
            this.Damage = (int)HeroDamage.Enchanter;
            this.Trait = "Enchanter";
        }
    }
}
