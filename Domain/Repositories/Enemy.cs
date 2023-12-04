
using System;

namespace Domain.Repositories
{
    public class Enemy : IEnemy
    {
        public int HP { get; set; }
        public int HPThreshold { get; set; }
        public int XP { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; }
        public bool IsStunned { get; set; }
        
        public Enemy()
        {
           Type = "";
           IsStunned = false;
        }
        public virtual void BasicAttack(Hero hero)
        {
            hero.HP -= Damage;
            Console.WriteLine($"Enemy {Type} has damaged you for {Damage}");
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


