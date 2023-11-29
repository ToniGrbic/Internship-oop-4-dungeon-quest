using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Data.Constants;
namespace Domain.Repositories
{
    public class Marksman : Hero
    {
        public Marksman(string name, int HP) : base(name, HP)
        {
            this.Damage = (int)HeroDamage.Marksman;
            this.HP = (int)HeroHP.Marksman;
            this.Trait = "Marksman";
        }
    }
}
