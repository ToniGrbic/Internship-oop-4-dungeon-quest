namespace Domain.Repositories;
public class Hero
{
    public string Name { get; set; }
    public int XP { get; set; }
    public int XPTreshold { get; set; }
    public int HPTheshold { get; set; }
    public int Damage { get; set; }
    public int HP { get; set; }
    public int Level { get; set; }
    public string? Trait { get; set; }
    public Hero(string name)
    {
        Name = name;
        XP = 0;
        Level = 1;
        XPTreshold = 100;
        Trait = "";
    }
    public void GainExperienceAndLevelUp(int gainedXP)
    {
        if(XP + gainedXP >= XPTreshold)
        {
            ++Level;
            Console.WriteLine(
                "YOU HAVE LEVELED UP!!\n" +
                $"NEW LVL: {Level}\n" +
                $"***************************"
            );
            XP = (XP + gainedXP) - XPTreshold;
            HPTheshold += 50;
            HP += 100;
            Damage += 30;

            if (this is Enchanter enchanter)
            {
                enchanter.ManaThreshold += 50;
                enchanter.Mana = enchanter.ManaThreshold;
            }
        }
        else
            XP += gainedXP;
    }
    public void BasicAttack(Enemy enemy)
    {
        enemy.HP -= Damage;
        if (this is Enchanter enchanter)
        {
            enchanter.Mana -= 15;
        }
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
        var HPToGain = (int)(HPTheshold * 0.25);
        if (HP + HPToGain > HPTheshold)
        {
            HP = HPTheshold;
            Console.WriteLine($"Healed to MAX HP\n");
        }
        else
        {
            HP += HPToGain;
            Console.WriteLine($"Healed for: {HPToGain}\n");
        }
        this.PrintHeroStats();

    }
    public void PrintHeroStats()
    {
        Console.WriteLine(
                $"HERO: {Name}\n" +
                $"Trait: {Trait}\n" +
                $"Level: {Level}\n" +
                $"HP: {HP} / {HPTheshold}\n" +
                $"XP: {XP} / {XPTreshold}\n" +
                $"Damage: {Damage}"
        );
    }
}

