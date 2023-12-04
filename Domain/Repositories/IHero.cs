
namespace Domain.Repositories
{
    public interface IHero
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
    }
}
