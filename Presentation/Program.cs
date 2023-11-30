// See https://aka.ms/new-console-template for more information
using Domain.Repositories;
using Data.Constants;

GameState gameState = GameState.CONTINUE;

/*Console.WriteLine(
    $"Hero {newHero.Name}\n" +
    $"Trait: {newHero.Trait}\n" +
    $"HP: {newHero.HP}\n" +
    $"XP: {newHero.XP}\n" +
    $"Damage: {newHero.Damage}\n");
*/

Dictionary<int, Func<string, Hero>>heroTraitChoice = new()
{
    {1, name => new Gladiator(name)},
    {2, name => new Marksman(name)},
    {3, name => new Enchanter(name)}
};

do{
    //Characther creation
    Console.WriteLine(
        "Choose your Hero's Trait: \n" +
               "1. Gladiator\n" +
               "2. Marksman\n" +
               "3. Enchanter\n"
    );
    
    var success = int.TryParse(Console.ReadLine(), out int choice);
    if(!success || heroTraitChoice.ContainsKey(choice))
    {
        Console.WriteLine("Invalid input for trait, try again\n");
        Console.Clear();
        continue;
    }
    
    var heroName = InputNonEmpryStringFormat("Hero name");
    var newHero = heroTraitChoice[choice].Invoke(heroName);

    Console.WriteLine(
    $"Hero {newHero.Name}\n" +
    $"Trait: {newHero.Trait}\n" +
    $"HP: {newHero.HP}\n" +
    $"XP: {newHero.XP}\n" +
    $"Damage: {newHero.Damage}\n");


} while (gameState == GameState.CONTINUE);


string InputNonEmpryStringFormat(string message = "input")
{
    string? input;
    bool isError;
    do{
        Console.WriteLine(message);
        input = Console.ReadLine();
        isError = string.IsNullOrWhiteSpace(input);
        if (isError)
            Console.WriteLine(message + "cannot be a empty string, try again...\n");
    } while (isError);
    return input;
}

