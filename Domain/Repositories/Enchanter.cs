
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
            this.HPTheshold = (int)HeroHP.Enchanter;
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
            }
        }

        public void ReviveAbility()
        { 
            HP = HPTheshold;
            Mana = ManaThreshold; 
            HasRevive = false;
        }
    }
}
