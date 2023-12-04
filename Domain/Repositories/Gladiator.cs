
using Data.Constants;
using Data.InputOutputUtils;

namespace Domain.Repositories;

public class Gladiator : Hero
{
    public float RageHealthCostPercent { get; set; }
    public bool isRageActive { get; set; }
    public int BaseDamage { get; set; }
    public Gladiator(string Name) : base(Name)
    {
        RageHealthCostPercent = 0.10f;
        isRageActive = false;
        BaseDamage = (int)HeroDamage.Gladiator;
        this.Trait = "Gladiator";
        this.HP = (int)HeroHP.Gladiator;
        this.HPThreshold = (int)HeroHP.Gladiator;
        this.Damage = BaseDamage;
    }
    public void RageAbility()
    {
        Damage *= 2;
        isRageActive = true;
    }
    public override void UseHeroAbility()
    {
        if (HPThreshold * RageHealthCostPercent >= HP)
            Console.WriteLine("Not enough HP to use Rage Ability\n");
        else
        {
            Console.WriteLine("Do you want to use Rage Ability? (yes/no)");
            if (Utils.ConfirmationDialog() == GameLoop.CONTINUE)
                RageAbility();
        }
    }
    public override void BasicAttack(Enemy enemy)
    {
        UseHeroAbility();

        if (isRageActive)
        {
            Console.WriteLine("Rage is activated!\n");
            HP -= (int)(HPThreshold * RageHealthCostPercent);
            
            enemy.HP -= Damage;
            Console.WriteLine($"You damaged the {enemy.Type} for {Damage}");
            isRageActive = false;
            Damage = BaseDamage;
        }
        else
        {
            enemy.HP -= Damage;
            Console.WriteLine($"You damaged the {enemy.Type} for {Damage}");
        }
    }

    


}
