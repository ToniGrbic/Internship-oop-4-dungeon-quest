﻿namespace Domain.Repositories;
public class Hero
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
        XPThreshold = 100;
        Trait = "";
    }
    public void GainExperienceAndLevelUp(int gainedXP)
    {
        if(XP + gainedXP >= XPThreshold)
        {
            ++Level;
            Console.WriteLine(
                "YOU HAVE LEVELED UP!!\n" +
                $"NEW LVL: {Level}\n" +
                $"***************************"
            );
            XP = (XP + gainedXP) - XPThreshold;
            HPThreshold += 25;
            Damage += 10;
            if (this is Gladiator gladiator)
                gladiator.BaseDamage += 15;
            
            if (this is Enchanter enchanter)
            {
                enchanter.ManaThreshold += 10;
                enchanter.Mana = enchanter.ManaThreshold;
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
        }
        else{
            Console.WriteLine("Not enough XP\n");
        }
    }

    public void RegainHealthAfterBattle()
    {
        var HPToGain = (int)(HPThreshold * 0.25);
        if (HP + HPToGain > HPThreshold)
        {
            HP = HPThreshold;
            Console.WriteLine($"Healed to MAX HP\n");
        }
        else
        {
            HP += HPToGain;
            Console.WriteLine($"Healed for: {HPToGain}\n");
        }
        PrintHeroStats();

    }
    public void PrintHeroStats()
    {
        Console.WriteLine(
                $"HERO: {Name}\n" +
                $"Trait: {Trait}\n" +
                $"Level: {Level}\n" +
                $"XP: {XP} / {XPThreshold}\n" +
                $"HP: {HP} / {HPThreshold}\n" +
                $"{HasMana()}" +
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
}

