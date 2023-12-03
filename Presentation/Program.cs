// See https://aka.ms/new-console-template for more information
using Domain.Repositories;
using Data.Constants;
using System.Runtime.InteropServices;
using System;

GameLoop gameLoop = GameLoop.CONTINUE;
GameState gameState = GameState.IN_PROGRESS;
const int NUMBER_OF_DUNGEON_WAVES = 10;

Dictionary<int, Func<string, Hero>>heroTraitChoice = new()
{
    {1, name => new Gladiator(name)},
    {2, name => new Marksman(name)},
    {3, name => new Enchanter(name)}
};

Dictionary<int, AttackType> Attacks = new()
{
    {1, AttackType.DIRECT},
    {2, AttackType.SIDE},
    {3, AttackType.COUNTER}
};

Dictionary<AttackType, string> AttacksString = new()
{
    {AttackType.DIRECT, "Direct"},
    {AttackType.SIDE, "Side"},
    {AttackType.COUNTER, "Counter"}
};

Dictionary<int, Func<Enemy>> enemies= new()
{
    {1, () => new Goblin()},
    {2, () => new Brute()},
    {3, () => new Witch()}
};

do{
    Console.WriteLine(
        "WELCOME TO DUNGEON QUEST!!\n" +
        "***************************\n" +
        "1 - PLAY\n" +
        "2 - EXIT\n"
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
               "1 - Gladiator\n" +
               "2 - Marksman -> work in proggres\n" +
               "3 - Enchanter\n"
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
    newHero.PrintHeroStats();

    Console.WriteLine("Ready to start the game?\n");
    ConsoleClearAndContiue();

    gameState = PlayDungeon(newHero);

    if (gameState == GameState.LOSS)
        Console.WriteLine("You lost the game\n");
    else if (gameState == GameState.WIN)
    {
        Console.WriteLine(
            "YOU HAVE WON, Congrats!\n\n" +
            "END HERO STATS:\n"
        );
        newHero.PrintHeroStats();
    }
        
    Console.WriteLine("Do you want to play again? (yes/no)");
    gameLoop = ConfirmationDialog();
    Console.Clear();

} while (gameLoop == GameLoop.CONTINUE);

GameState PlayDungeon(Hero hero)
{
    for(int i = 0; i < NUMBER_OF_DUNGEON_WAVES; i++)
    {
        var enemy = CreateEnemyWave();
        Console.WriteLine(
            $"DUNGEON WAVE {i+1}:\n" +
            $"**********************************\n");
        PrintHeroAndEnemyStats(hero, enemy);

        Console.WriteLine("Continue to start the fight! Press any key...");
        ConsoleClearAndContiue();

        Console.WriteLine(
            "\nFIGHT STARTED!!\n" +
            "***************************\n"
        );
        bool heroAlive = FightEnemy(hero, enemy);
        
        if(!heroAlive && (hero is Enchanter))
        {
            var enchanter = hero as Enchanter;
            if (enchanter!.HasRevive)
            {
                Console.WriteLine("Enchnter hero has died, do you want to use Revive? (yes/no)");
                if (ConfirmationDialog() == GameLoop.CONTINUE)
                    enchanter.ReviveAbility();
            }
            else {
                Console.WriteLine("Revive ability has been used before\n");
                return GameState.LOSS;
            }
                
        }
        else if(!heroAlive)
            return GameState.LOSS;

        if (i == NUMBER_OF_DUNGEON_WAVES - 1)
            break;

        Console.Clear();
        PrintHeroAndEnemyStats(hero, enemy);
        Console.WriteLine("Calculating...");
        Thread.Sleep(1500);
        Console.WriteLine($"YOU HAVE DEFATED THE {enemy.Type}!\n" +
            $"***********************************\n");
        Console.WriteLine($"You gained: {enemy.XP} XP");
        hero.GainExperienceAndLevelUp(enemy.XP);
        hero.RegainHealthAfterBattle();
        ConsoleClearAndContiue();

        if (hero.XP > 0 && hero.HP < hero.HPThreshold)
        {
            Console.WriteLine("Do you want to spend your XP for Healh? (yes/no)");
            if (ConfirmationDialog() == GameLoop.CONTINUE)
            {
                var amount = InputExperiance(hero);
                hero.SpendXPforHP(amount);
            }
        } 
        Console.WriteLine("Continue to next enemy. ");
        ConsoleClearAndContiue();
    }
    return GameState.WIN;
}

bool FightEnemy(Hero hero, Enemy enemy)
{
    int round = 1;
    while(hero.HP > 0 && enemy.HP > 0)
    {
        PrintHeroAndEnemyStats(hero, enemy);
        Console.WriteLine(
            $"\nROUND {round++} :\n" +
            $"*********************\n"
        );

        UseHeroAbility(hero);
        InitiateCombatAndDecideOutcome(hero, enemy);
        ConsoleClearAndContiue();

        PrintHeroAndEnemyStats(hero, enemy);
        Console.Clear();
    }
    return hero.HP > 0;
}

void UseHeroAbility(Hero hero)
{
    if (hero is Gladiator)
    {
        var gladiator = hero as Gladiator;
        if (hero.HPThreshold * gladiator!.RageHealthCostPercent >= hero.HP)
            Console.WriteLine("Not enough HP to use Rage Ability\n");
        else
        {
            Console.WriteLine("Do you want to use Rage Ability? (yes/no)");
            if (ConfirmationDialog() == GameLoop.CONTINUE)
                gladiator.RageAbility();
        }
    }
    else if (hero is Enchanter)
    {
        var enchanter = hero as Enchanter;

        if (enchanter!.Mana < 50)
            Console.WriteLine("Not enough mana to use Heal Ability\n");
        else
        {
            Console.WriteLine("Do you want to use Heal Ablity? (yes/no)");
            if (ConfirmationDialog() == GameLoop.CONTINUE)
                enchanter.HealAbility();
        }
    }
    else if (hero is Marksman)
    {
        Console.WriteLine("Do you want to use Marksman Attack? (yes/no)");
        if (ConfirmationDialog() == GameLoop.CONTINUE) ;
        //(hero as Marksman)!.MarksmanAttack(enemy);
    }
}

AttackType HeroChooseAttack()
{
    Console.WriteLine(
            "Choose your attack type:\n" +
            "1 - DIRECT\n" +
            "2 - SIDE\n" +
            "3 - COUNTER\n"
    );
    int choice;
    bool success;
    do{
        success = int.TryParse(Console.ReadLine(), out choice) && Attacks.ContainsKey(choice);
        if (!success)
            Console.WriteLine("Invalid input for attack type, try again\n");
  
    } while (!success);
    return Attacks[choice];
}
AttackType EnemyChooseAttack()
{
    var random = new Random();
    var choice = random.Next(1, 4);
    return Attacks[choice];
}

CombatOutcome IsHeroWinner(AttackType heroAttack, AttackType enemyAttack)
{
    if(heroAttack == AttackType.DIRECT && enemyAttack == AttackType.SIDE)
        return CombatOutcome.WIN;
    else if(heroAttack == AttackType.SIDE && enemyAttack == AttackType.COUNTER)
        return CombatOutcome.WIN;
    else if(heroAttack == AttackType.COUNTER && enemyAttack == AttackType.DIRECT)
        return CombatOutcome.WIN;
    else if (heroAttack == enemyAttack)
        return CombatOutcome.DRAW;  
    else
        return CombatOutcome.LOSE;
}

void InitiateCombatAndDecideOutcome(Hero hero, Enemy enemy)
{
    var heroAttack = HeroChooseAttack();
    var enemyAttack = EnemyChooseAttack();
    var combatOutcome = IsHeroWinner(heroAttack, enemyAttack);
    Console.WriteLine("Attacking...");
    Thread.Sleep(2000);

    if (combatOutcome == CombatOutcome.WIN)
    {
        Console.WriteLine($"Hero uses {AttacksString[heroAttack]} attack and beats Enemy {AttacksString[enemyAttack]} attack\n");
        Thread.Sleep(1000);

        hero.BasicAttack(enemy);
        Console.WriteLine($"You damaged {enemy.Type} for {hero.Damage}");
        
    }
    else if (combatOutcome == CombatOutcome.LOSE)
    {
        Console.WriteLine($"Enemy uses {AttacksString[enemyAttack]} attack and beats Hero {AttacksString[heroAttack]} attack\n");
        Thread.Sleep(1000);
        enemy.BasicAttack(hero);
        Console.WriteLine($"Enemy {enemy.Type} has damaged you for {enemy.Damage}");
    }
    else{
        Console.WriteLine($"Both Hero and Enemy used {AttacksString[heroAttack]} attack, DRAW!\n");
    }

    if (hero is Gladiator)
    {
        var gladiator = hero as Gladiator;
        if(gladiator!.Damage > gladiator!.BaseDamage)
            gladiator.Damage = gladiator.BaseDamage;
    }
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
    if(choice <= 60)
        return 1;
    else if(choice > 60 && choice <= 80)
        return 2;
    else
        return 3;
}

string InputNonEmpryStringFormat(string message = "Input")
{
    string? input;
    bool isError;
    do{
        Console.WriteLine(message + ": \n");
        input = Console.ReadLine();
        isError = string.IsNullOrWhiteSpace(input);
        if (isError)
            Console.WriteLine(message + "cannot be a empty string, try again...\n");
    } while (isError);
    return input!;
}

int InputIntFormat(string message = "Input")
{
    int input;
    bool isError;
    do{
        Console.WriteLine(message + ": \n");
        isError = !int.TryParse(Console.ReadLine(), out input);
        if (isError)
            Console.WriteLine(message + "must be a number, try again...\n");
    } while (isError);
    return input;
}

int InputExperiance(Hero hero)
{
    int amount = 0;
    int halfXP = hero.XP / 2;
    do{
        
        Console.WriteLine($"CURRENT XP: {hero.XP} / {hero.XPThreshold}\n");
        Console.WriteLine($"CURRENT HP: {hero.HP} / {hero.HPThreshold}\n");
        amount = InputIntFormat($"Input amount of XP to spend (MAX is {halfXP})");
        if (amount > halfXP)
        {
            Console.WriteLine("Not enough XP, try again\n");
            continue;
        }
    } while (amount > halfXP);

    return amount;
}

void PrintHeroAndEnemyStats(Hero hero, Enemy enemy)
{
    hero.PrintHeroStats();
    Console.WriteLine(
        "\n************** VS ***************\n");
    enemy.PrintEnemyStats();
}

void ConsoleClearAndContiue()
{
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
}   

GameLoop ConfirmationDialog()
{
    string choice;
    do{
        choice = Console.ReadLine()!.ToLower();
        if (choice == "" || (choice != "yes" && choice != "no"))
        {
            Console.WriteLine("Invalid input, try again\n");
            continue;
        }
        break;
    } while (true);

    if(choice!.ToLower() == "yes")
        return GameLoop.CONTINUE;
    
    return GameLoop.EXIT;
}
