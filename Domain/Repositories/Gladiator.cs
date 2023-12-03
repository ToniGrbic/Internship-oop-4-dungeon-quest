
using Data.Constants;

namespace Domain.Repositories;

    public class Gladiator : Hero
    {
        public float RageHealthCostPercent { get; set; }
        public int BaseDamage { get; set; }
        public Gladiator(string Name) : base(Name)
        {
            RageHealthCostPercent = 0.15f;
            this.HP = (int)HeroHP.Gladiator;
            this.HPTheshold = (int)HeroHP.Gladiator;
            this.BaseDamage = (int)HeroDamage.Gladiator;
            this.Damage = (int)HeroDamage.Gladiator;
            this.Trait = "Gladiator";
        }

        public void RageAbility()
        {
           this.HP -= (int)(this.HPTheshold * RageHealthCostPercent);
           this.Damage *= 2;
        }
    }

