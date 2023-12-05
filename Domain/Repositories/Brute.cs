
using Data.Constants;

namespace Domain.Repositories
{
    public class Brute:Enemy
    {
        public bool isHeavyStrike = false;
        public float HeavyStrikeChance = 0.25f;
        public float PercentageOfHPDamage = 0.25f;
        public Brute()
        {
            this.HPThreshold = (int)EnemyHP.Brute;
            this.HP = HPThreshold;
            this.XP = (int)EnemyXP.Brute;
            this.Damage = (int)EnemyDamage.Brute;
            this.Type = "Brute";
        }

        public override void BasicAttack(Hero hero)
        {
            var random = new Random();
            var choice = random.Next(1, 101);
            var HeavyStrikeOdds = HeavyStrikeChance * 100;
            if (choice <= HeavyStrikeOdds)
                isHeavyStrike = true;

            if (isHeavyStrike)
            {
                var HeavyStrikeDamage = (int)(hero.HPThreshold * PercentageOfHPDamage);
                hero.HP -= HeavyStrikeDamage;
                Console.WriteLine(
                    $"Enemy {Type} made a Heavy Strike!\n" +
                    $"Hero is damaged for {HeavyStrikeDamage}");
                isHeavyStrike = false;
            }
            else
            {
                hero.HP -= Damage;
                Console.WriteLine($"Enemy {Type} has damaged you for {Damage}");
            }
                
        }
    }
}
