using Data.Constants;

namespace Domain.Repositories
{
    public class Witch:Enemy
    {
        public List<Enemy> EnemiesToSpawn { get; set; } = new();

        public Witch()
        {
            this.HPThreshold = (int)EnemiesHP.Witch;
            this.HP = HPThreshold;
            this.XP = (int)EnemiesXP.Witch;
            this.Damage = (int)EnemiesDamage.Witch;
            this.Type = "Witch";    
        }

        public void DisarrayAbility(Hero hero)
        {
            
        }
    
    }
}
