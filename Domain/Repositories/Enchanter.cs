
using Data.Constants;
using Data.Utils;

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
            ManaThreshold = (int)ManaAmount.Enchanter;
            Mana = ManaThreshold;
            HasRevive = true;
        }

        public void HealAbility()
        {
            if (Mana >= 50)
            {
                HP += 250;
                Mana -= 50;
                PrintHeroStats();
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
            if (Mana >= 15)
            {
                Mana -= 15;
                enemy.HP -= Damage;
            }
            else
            {
                Console.WriteLine("Not enough mana for attack, regaining mana for this round.\n");
                Mana = ManaThreshold;
            }
        }

        public override void UseHeroAbility()
        {
            if (Mana < 50)
                Console.WriteLine("Not enough mana to use Heal Ability\n");
            else
            {
                Console.WriteLine("Do you want to use Heal Ablity? (yes/no)");
                if (Utils.ConfirmationDialog() == GameLoop.CONTINUE)
                    HealAbility();
            }
        }
    }
}
