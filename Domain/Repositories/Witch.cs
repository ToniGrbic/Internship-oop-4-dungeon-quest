using Data.Constants;

namespace Domain.Repositories
{
    public class Witch:Enemy
    {
        public List<Enemy> EnemiesToSpawn { get; set; } = new();
        public int DisarrayHealthPercentage { get; set; }
        public Witch()
        {
            this.HPThreshold = (int)EnemyHP.Witch;
            this.HP = HPThreshold;
            this.XP = (int)EnemyXP.Witch;
            this.Damage = (int)EnemyDamage.Witch;
            this.Type = "Witch";
            this.DisarrayHealthPercentage = new Random().Next(20, 71);
        }
        public void DisarrayAbility(Hero hero)
        {
            Console.WriteLine(
                  $"The {Type} has used Disarray Ability!\n" +
                  $"Everyones HP is set to {DisarrayHealthPercentage}% !\n"
            );
            
            hero.HP = (int)(hero.HPThreshold * (DisarrayHealthPercentage / 100f));
            Console.WriteLine($"Hero HP: {hero.HP} / {hero.HPThreshold}");

            this.HP = (int)(HPThreshold * (DisarrayHealthPercentage / 100f));
            Console.WriteLine($"Enemy {Type} HP: {HP} / {HPThreshold}");

            foreach (var enemy in EnemiesToSpawn){
                enemy.HP = (int)(enemy.HPThreshold * (DisarrayHealthPercentage / 100f));
                Console.WriteLine($"Enemy {enemy.Type} HP: {enemy.HP} / {enemy.HPThreshold}");  
            }
               
        }

        public override void BasicAttack(Hero hero)
        {
            var disarryChance = new Random().Next(1, 101);
            if (disarryChance <= 50)
            {
                DisarrayAbility(hero);
            }
            else
            {
                hero.HP -= Damage;
                Console.WriteLine($"Enemy {Type} has damaged you for {Damage}");
            }
        }

        public void PrintSpawnedEnemies()
        {
            Console.WriteLine("\tSpawned Enemies:");
            foreach (var enemy in EnemiesToSpawn)
            {
                Console.WriteLine($"\t1. {enemy.Type}");
            }
            Console.WriteLine("\n");
        }
    }
}
