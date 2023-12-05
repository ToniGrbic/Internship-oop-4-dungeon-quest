using Data.InputOutputUtils;
using Data.Constants;
using System.Reflection.Metadata;

namespace Domain.Repositories;
public class Hero : IHero
{
    public string Name { get; set; }
    public int XP { get; set; }
    public int XPThreshold { get; set; }
    public int HPThreshold { get; set; }
    public int Damage { get; set; }
    public int HP { get; set; }
    public int Level { get; set; }
    public string? Trait { get; set; }
    public virtual void BasicAttack(Enemy enemy) { }
    public virtual void UseHeroAbility() { }

    public Hero(string name)
    {
        Name = name;
        XP = 0;
        Level = 1;
        XPThreshold = Constants.XP_THRESHOLD;
        Trait = "";
    }
    public void GainExperienceAndLevelUp(int gainedXP)
    {
        Console.WriteLine($"You gained: {gainedXP} XP\n");
        if(XP + gainedXP >= XPThreshold)
        {
            ++Level;
            Console.WriteLine(
                "YOU HAVE LEVELED UP!!\n" +
                $"NEW LVL: {Level}\n" +
                $"***************************"
            );
            XP = (XP + gainedXP) - XPThreshold;
            HPThreshold += Constants.HP_THRESHOLD_PER_LVL;
            Damage += Constants.DAMAGE_PER_LVL;

            if (this is Gladiator gladiator)
                gladiator.BaseDamage += Constants.DAMAGE_PER_LVL;
            else if (this is Enchanter enchanter)
            {
                enchanter.ManaThreshold += Constants.MANA_INCREASE_PER_LVL;
                enchanter.Mana = enchanter.ManaThreshold;
            }
            else if (this is Marksman marksman)
            {
                if(marksman.CriticalStrikeChance <= 0.95f)
                    marksman.CriticalStrikeChance += Constants.CRIT_CHANCE_PER_LVL;
                if(marksman.StunChance <= 0.55f)
                    marksman.StunChance += Constants.CRIT_CHANCE_PER_LVL;
            }
        }
        else
            XP += gainedXP;
    }
    public void SpendXPforHP(int amount)
    {
        if(XP < 100 && XP >= amount)
        {
            XP -= amount;
            HP += amount;
            Console.WriteLine(
               $"\n{amount} HP added.\n" +
               $"HEALTH: {HP}\n" +
               $"EXPERIENCE: {XP}\n"
           );
            Console.WriteLine("Continue to next enemy. ");
            Utils.ConsoleClearAndContinue();
        }
        else{
            Console.WriteLine("Not enough XP\n");
        }
    }

    public void RegainHealthAfterBattle()
    {
        var HPToGain = (int)(HPThreshold * Constants.HP_PERCENT_REGAIN);
        if (HP + HPToGain > HPThreshold)
        {
            HP = HPThreshold;
            Console.WriteLine($"Healed to MAX HP\n");
        }
        else
        {
            HP += HPToGain;
            Console.WriteLine($"Healed for +{HPToGain}HP\n");
        }
        PrintHeroStats();
    }
    public void PrintHeroStats()
    {
        Console.WriteLine(
                $"HERO: {Name}\n" +
                $"********************\n" +
                $"Trait: {Trait}\n" +
                $"Level: {Level}\n" +
                $"XP: {XP} / {XPThreshold}\n" +
                $"HP: {HP} / {HPThreshold}\n" +
                $"{HasMana()}" +
                $"{HasStrikeAndStunChance()}" +
                $"Damage: {Damage}\n"
        );
    }
    public string HasMana()
    {
        return (
            this is Enchanter enchanter  
            ? $"Mana:{enchanter.Mana} / {enchanter.ManaThreshold}\n" 
            : ""
        );
    }
    public string HasStrikeAndStunChance()
    {
        return (
              this is Marksman marksman
              ? $"Crit Chance: {(marksman.CriticalStrikeChance * 100):0.0}%\n" +
                $"Stun Chance: {(marksman.StunChance * 100):0.0}%\n" : ""
        );
    }
}

