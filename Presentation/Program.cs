// See https://aka.ms/new-console-template for more information
using Domain.Repositories;
using Data.Constants;
using System.Threading.Tasks.Dataflow;
using System;

GameLoop gameLoop = GameLoop.CONTINUE;
GameState gameState = GameState.IN_PROGRESS;

Dictionary<int, Func<string, Hero>>heroTraitChoice = new()
{
    {1, name => new Gladiator(name)},
    {2, name => new Marksman(name)},
    {3, name => new Enchanter(name)}
};


Dictionary<int, Func<Enemy>> enemies= new()
{
    {1, () => new Goblin()},
    {2, () => new Brute()},
    {3, () => new Witch()}
};

do{
    Console.WriteLine(
        "DUNGEON QUEST\n" +
        "***********************\n" +
        "1. PLAY\n" +
        "2. EXIT\n"
    );
    var success = int.TryParse(Console.ReadLine(), out int choice);

    if (choice == 2)
    {
        gameLoop = GameLoop.EXIT;
        continue;
    }
    else if (choice != 1 || !success)
    {
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
    if (!success || !heroTraitChoice.ContainsKey(choice))
    {
        Console.WriteLine("Invalid input for trait, try again\n");
        Console.Clear();
        continue;
    }

    var heroName = InputNonEmpryStringFormat("Hero name");
    var newHero = heroTraitChoice[choice].Invoke(heroName);

    Console.Clear();
    PrintHeroStats(newHero);

    Console.WriteLine("Ready to start the game?\n");
    ConsoleClearAndContiue();

    gameState = PlayDungeon(newHero);
    if (gameState == GameState.LOSS)
        Console.WriteLine("You lost the game\n");
    else if (gameState == GameState.WIN)
        Console.WriteLine("You won the game, Congrats!\n");
    
    Console.WriteLine("Do you want to play again? (yes/no)");
    gameLoop = ConfirmationDialog();
    Console.Clear();

} while (gameLoop == GameLoop.CONTINUE);

GameState PlayDungeon(Hero hero)
{
    for(int i = 0; i < 10; i++)
    {
        PrintHeroStats(hero);
        var enemy = CreateEnemyWave();
        Console.WriteLine("\n\n");
        PrintEnemyStats(enemy);

        Console.WriteLine("Continue to start the fight! ");
        ConsoleClearAndContiue();

        Console.WriteLine("FIGHT STARTED!!\n");
        bool heroAlive = Fight(hero, enemy);
        
        if(!heroAlive)
            return GameState.LOSS;
        Console.WriteLine($"YOU HAVE DEFATED the {enemy.Type}!");

        hero.GainXP(enemy.XP);
        Console.WriteLine($"You gained {enemy.XP} XP\n");

        Console.WriteLine("Continue to next enemy. ");
        ConsoleClearAndContiue();
    }
    return GameState.WIN;
}

bool Fight(Hero hero, Enemy enemy)
{
    int round = 1;
    while(hero.HP > 0 && enemy.HP > 0)
    {
        Console.WriteLine($"ROUND {round++} :\n\n");
        hero.BasicAttack(enemy);
        enemy.BasicAttack(hero);
        PrintEnemyStats(enemy);
        PrintHeroStats(hero);
        Thread.Sleep(2000);
    }
    if(hero.HP <= 0)
        return false;
    else
        return true;
}
void PrintHeroStats(Hero hero)
{
    Console.WriteLine(
            $"HERO: {hero.Name}\n" +
            $"Trait: {hero.Trait}\n" +
            $"HP: {hero.HP}\n" +
            $"XP: {hero.XP}\n" +
            $"Damage: {hero.Damage}\n"
    );
}
void PrintEnemyStats(Enemy enemy)
{
    Console.WriteLine(
            $"ENEMY: {enemy.Type}\n" +
            $"HP: {enemy.HP}\n" +
            $"Damage: {enemy.Damage}\n"
    );
}

Enemy CreateEnemyWave()
{
   var random = new Random();
   var enemyType = EnemyChoiceProbability();
   var enemy = enemies[enemyType].Invoke();
       
   return enemy;
}

int EnemyChoiceProbability()
{
    var random = new Random();
    var choice = random.Next(1, 101);
    if(choice <= 50)
        return 1;
    else if(choice > 50 && choice <= 80)
        return 2;
    else
        return 3;
}

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
    Console.WriteLine("Press any key...");
    Console.ReadKey();
    Console.Clear();
}   

GameLoop ConfirmationDialog()
{
    string? choice;
    do
    {
        choice = Console.ReadLine();
        if (choice == "" || choice.ToLower() != "yes" || choice.ToLower() != "no")
        {
            Console.WriteLine("Invalid input, try again\n");
            continue;
        }
        break;
    } while (true);

    if(choice.ToLower() == "yes")
        return GameLoop.CONTINUE;
    
    return GameLoop.EXIT;
}
