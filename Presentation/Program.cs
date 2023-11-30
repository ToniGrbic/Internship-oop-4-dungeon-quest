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
    Console.WriteLine(
        "DUNGEON QUEST\n" +
        "***********************\n" +
        "1. PLAY\n" +
        "2. EXIT\n"
    );
    var success = int.TryParse(Console.ReadLine(), out int choice);

    if(choice == 2){
        gameState = GameState.EXIT;
        continue;
    }else if(choice != 1 || !success){
        Console.WriteLine("Invalid input, try again\n");
        ConsoleClearAndContiue();
        continue;
    }

    Console.Clear();
    Console.WriteLine(
        "CREATE YOUR HERO:\n" +
        "*********************\n" +
        "Choose your Hero's Trait: \n" +
               "1. Gladiator\n" +
               "2. Marksman\n" +
               "3. Enchanter\n"
    );
    
    success = int.TryParse(Console.ReadLine(), out choice);
    if(!success || !heroTraitChoice.ContainsKey(choice))
    {
        Console.WriteLine("Invalid input for trait, try again\n");
        Console.Clear();
        continue;
    }
    
    var heroName = InputNonEmpryStringFormat("Hero name");
    var newHero = heroTraitChoice[choice].Invoke(heroName);
    Console.Clear();
    Console.WriteLine(
        $"Hero {newHero.Name}\n" +
        $"Trait: {newHero.Trait}\n" +
        $"HP: {newHero.HP}\n" +
        $"XP: {newHero.XP}\n" +
        $"Damage: {newHero.Damage}\n"
    );
    Console.WriteLine("Ready to start the game?\n");
    ConsoleClearAndContiue();

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
    return input!;
}

void ConsoleClearAndContiue()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
}   
