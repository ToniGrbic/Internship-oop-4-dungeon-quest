
using Data.Constants;
using Data.Utils;
using System;

namespace Domain.Repositories;

public class Marksman : Hero
{
    public float CriticChance { get; set; }
    public float StunChance { get; set; }

    public Marksman(string name) : base(name)
    {
        this.Damage = (int)HeroDamage.Marksman;
        this.HP = (int)HeroHP.Marksman;
        this.HPThreshold = (int)HeroHP.Marksman;
        this.Trait = "Marksman";
    }

    public void MarksmanAttack()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 101);
        if (randomNumber <= 20)
        {
            Console.WriteLine("You have stunned the enemy!");
            StunChance = 1;
        }
        else
        {
            Console.WriteLine("You have not stunned the enemy!");
            StunChance = 0;
        }
        randomNumber = random.Next(1, 101);
        if (randomNumber <= 20)
        {
            Console.WriteLine("You have critically hit the enemy!");
            CriticChance = 1;
        }
        else
        {
            Console.WriteLine("You have not critically hit the enemy!");
            CriticChance = 0;
        }
    }

    public override void UseHeroAbility()
    {
        Console.WriteLine("Do you want to use Marksman Attack? (yes/no)");
        if (Utils.ConfirmationDialog() == GameLoop.CONTINUE) ;
        MarksmanAttack();
    }
}

