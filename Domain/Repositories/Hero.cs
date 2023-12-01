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
    public void GainXP(int gainedXP)
    {
        if(XP + gainedXP >= XPTreshold)
        {
            Level++;
            XP = (XP + gainedXP) - XPTreshold;
            //XPTreshold += 100; poslje mozda dodat da se threshold povecava sa levelUp
            HP += 100;
            Damage += 10;
        }
        else
            XP += gainedXP;
    }
    public void BasicAttack(Enemy enemy)
    {
        enemy.HP -= Damage;
    }
    public void SpendXPforHP(int amount)
    {
        if(XP < 100 && XP > amount)
        {
            XP -= amount;
            HP += amount;
        }
        else{
            Console.WriteLine("Not enough XP");
        }
    }
}

