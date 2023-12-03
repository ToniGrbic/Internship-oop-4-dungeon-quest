
namespace Domain.Repositories
{
    public class Enemy
    {
        public int HP { get; set; }
        public int HPThreshold { get; set; }
        public int XP { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; }
        
        public Enemy()
        {
           Type = "";
        }
        public void BasicAttack(Hero hero)
        {
            hero.HP -= Damage;
        }

        public void PrintEnemyStats()
        {
            Console.WriteLine(
                    $"ENEMY: {Type}\n" +
                    $"HP: {HP} / {HPThreshold}\n" +
                    $"Damage: {Damage}\n" +
                    $"XP: {XP}\n"
            );
        }
    }
}


