
using Data.Constants;
using Data.InputOutputUtils;
namespace Domain.Repositories;

public class Gladiator : Hero
{
    public float RageHealthCostPercent { get; set; }
    public bool IsRageActive { get; set; }
    public int BaseDamage { get; set; }
    public Gladiator(string Name) : base(Name)
    {
        RageHealthCostPercent = 0.10f;
        IsRageActive = false;
        BaseDamage = (int)HeroDamage.Gladiator;
        this.Trait = "Gladiator";
        this.HP = (int)HeroHP.Gladiator;
        this.HPThreshold = (int)HeroHP.Gladiator;
        this.Damage = BaseDamage;
    }
    public void RageAbility()
    {
        Damage *= 2;
        IsRageActive = true;
    }
    public override void UseHeroAbility()
    {
        if (HPThreshold * RageHealthCostPercent >= HP)
            Console.WriteLine("Not enough HP to use Rage Ability\n");
        else
        {
            Console.WriteLine("Do you want to use Rage Ability for double damage? (yes/no)");
            if (Utils.ConfirmationDialog() == GameLoop.CONTINUE)
                RageAbility();
        }
    }
    public override void BasicAttack(Enemy enemy)
    {
        UseHeroAbility();

        if (IsRageActive)
        {
            var HPCost = (int)(HPThreshold * RageHealthCostPercent);
            Console.WriteLine(
                    "Rage is activated!\n" +
                    $"Costs 10% of your Health: -{HPCost}HP\n"
            );
            HP -= HPCost;
        
            enemy.HP -= Damage;
            Console.WriteLine($"You damaged the {enemy.Type} for {Damage}");
            IsRageActive = false;
            Damage = BaseDamage;
        }
        else
        {
            enemy.HP -= Damage;
            Console.WriteLine($"You damaged the {enemy.Type} for {Damage}");
        }
    }
}
