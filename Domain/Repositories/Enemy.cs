
namespace Domain.Repositories
{
    public class Enemy
    {
        public int HP { get; set; }
        public int XP { get; set; }
        public int Damage { get; set; }
        public string Type { get; set; }
        
        public Enemy()
        {
           Type = "";
        }
        public void BasicAttack(Hero hero)
        {
            hero.HP -= this.Damage;
        }
    }
}
