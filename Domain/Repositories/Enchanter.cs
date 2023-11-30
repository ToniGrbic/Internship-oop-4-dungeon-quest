
using Data.Constants;

namespace Domain.Repositories
{
    public class Enchanter : Hero
    {
        public Enchanter(string name) : base(name)
        {
            this.HP = (int)HeroHP.Enchanter;
            this.Damage = (int)HeroDamage.Enchanter;
            this.Trait = "Enchanter";
        }
    }
}
