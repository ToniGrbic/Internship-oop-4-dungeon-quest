
using Data.Constants;

namespace Domain.Repositories;

public class Marksman : Hero
{
    public float CriticChance { get; set; }
    public Marksman(string name) : base(name)
    {
        this.Damage = (int)HeroDamage.Marksman;
        this.HP = (int)HeroHP.Marksman;
        this.HPTheshold = (int)HeroHP.Marksman;
        this.Trait = "Marksman";
    }
}

