
using Data.Constants;

namespace Domain.Repositories;

    public class Gladiator : Hero
    {
        public int Rage { get; set; }
        public Gladiator(string Name, int XP) : base(Name, XP)
        {
            this.Rage = 0;
            this.HP = (int)HeroHP.Gladiator;
            this.Damage = (int)HeroDamage.Gladiator;
            this.Trait = "Gladiator";
        }
    }

