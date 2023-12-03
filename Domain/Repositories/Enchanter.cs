﻿
using Data.Constants;
namespace Domain.Repositories
{
    public class Enchanter : Hero
    {
        public int Mana { get; set; }
        public int ManaThreshold { get; set; }
        public bool HasRevive { get; set; }
        public Enchanter(string name) : base(name)
        {
            this.Trait = "Enchanter";
            this.HP = (int)HeroHP.Enchanter;
            this.HPThreshold = (int)HeroHP.Enchanter;
            this.Damage = (int)HeroDamage.Enchanter;
            this.ManaThreshold = (int)ManaAmount.Enchanter;
            this.Mana = ManaThreshold;
        }

        public void HealAbility()
        {
            if (Mana >= 50)
            {
                this.HP += 250;
                Mana -= 50;
                this.PrintHeroStats();
            }
        }

        public void ReviveAbility()
        { 
            HP = HPThreshold;
            Mana = ManaThreshold; 
            HasRevive = false;
        }

        public override void BasicAttack(Enemy enemy)
        {
            if (this.Mana >= 15)
            {
                this.Mana -= 15;
                enemy.HP -= Damage;
            }
            else
            {
                Console.WriteLine("Not enough mana for attack, regaining mana for this round.\n");
                this.Mana = this.ManaThreshold;
            }
        }
    }
}
