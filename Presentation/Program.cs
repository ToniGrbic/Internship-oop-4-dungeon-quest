// See https://aka.ms/new-console-template for more information
using Domain.Repositories;

var newHero = new Gladiator("Maximus", 100);
Console.WriteLine(
    $"Hero {newHero.Name}\n" +
    $"Trait: {newHero.Trait}\n" +
    $"HP: {newHero.HP}\n" +
    $"XP: {newHero.XP}\n" +
    $"Damage: {newHero.Damage}\n");
