
using Data.Constants;
using Data.InputOutputUtils;

namespace Domain.Repositories;

public class Marksman : Hero
{
    public float CriticalStrikeChance { get; set; }
    public float StunChance { get; set; }
    public bool isCritStrike { get; set; }
    public bool isStunAttack { get; set; }
    public Marksman(string name) : base(name)
    {
        this.Damage = (int)HeroDamage.Marksman;
        this.HP = (int)HeroHP.Marksman;
        this.HPThreshold = (int)HeroHP.Marksman;
        this.Trait = "Marksman";
        CriticalStrikeChance = 0.45f;
        StunChance = 0.35f;
        isCritStrike = false;
        isStunAttack = false;
    }
    public void MarksmanAttack()
    {
        var random = new Random();
        var choice = random.Next(1, 101);
        var CritChanceOdds = CriticalStrikeChance * 100;
        if (choice <= CritChanceOdds)
            isCritStrike = true;

        choice = random.Next(1, 101);
        var StunChanceOdds = StunChance * 100;
        if (choice <= StunChanceOdds)
            isStunAttack = true;
    }
    public override void BasicAttack(Enemy enemy)
    {
        MarksmanAttack();

        if(isCritStrike)
        {
            enemy.HP -= Damage * 2;
            Console.WriteLine("CRITICAL STRIKE!");
            Console.WriteLine($"You damaged the {enemy.Type} for {Damage * 2}\n");
            isCritStrike = false;
        }
        else
        {
            enemy.HP -= Damage;
            Console.WriteLine($"You damaged the {enemy.Type} for {Damage}");
        }
           
        if(isStunAttack && enemy.HP > 0)
        {
            enemy.IsStunned = true;
            Console.WriteLine($"The enemy {enemy.Type} is stunned!\n");
            isStunAttack = false;
        }
       
    }
}

