
using Data.Constants;

namespace Domain.Repositories;

    public class Gladiator : Hero
    {
        public float RageHealthCostPercent { get; set; }
        public bool isRageActive { get; set; }
        public int BaseDamage { get; set; }
        public Gladiator(string Name) : base(Name)
        {
            RageHealthCostPercent = 0.15f;
            isRageActive = false;
            this.Trait = "Gladiator";
            this.HP = (int)HeroHP.Gladiator;
            this.HPThreshold = (int)HeroHP.Gladiator;
            BaseDamage = (int)HeroDamage.Gladiator;
            this.Damage = BaseDamage;
            
        }

        public void RageAbility()
        {
           this.Damage *= 2;
           this.isRageActive = true;
        }

    public override void BasicAttack(Enemy enemy)
    {

            if (this.isRageActive)
            {
                Console.WriteLine("Rage is activated!\n");
                HP -= (int)(HPThreshold * this.RageHealthCostPercent);
                this.isRageActive = false;
                enemy.HP -= Damage;
            }
            else
                enemy.HP -= Damage;
        }
    }

