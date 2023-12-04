// See https://aka.ms/new-console-template for more information
using Domain.Repositories;
using Data.Constants;
using Data.InputOutputUtils;

GameLoop gameLoop = GameLoop.CONTINUE;
GameState gameState = GameState.IN_PROGRESS;
const int NUMBER_OF_DUNGEON_WAVES = 10;

Dictionary<int, Func<string, Hero>>heroTraitChoice = new()
{
    {1, name => new Gladiator(name)},
    {2, name => new Marksman(name)},
    {3, name => new Enchanter(name)}
};

Dictionary<EnemyType, Func<Enemy>> enemies= new()
{
    {EnemyType.Goblin, () => new Goblin()},
    {EnemyType.Brute, () => new Brute()},
    {EnemyType.Witch, () => new Witch()}
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
        Utils.ConsoleClearAndContinue();
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

    var heroName = Utils.InputNonEmptyStringFormat("Hero name");
    var newHero = heroTraitChoice[choice].Invoke(heroName);

    Console.Clear();
    newHero.PrintHeroStats();

    Console.WriteLine("Ready to start the game?\n");
    Utils.ConsoleClearAndContinue();

    gameState = PlayDungeon(newHero);

    if (gameState == GameState.LOSS)
        Console.WriteLine("You lost the game\n");
    else if (gameState == GameState.WIN)
    {
        Console.WriteLine(
            "YOU HAVE WON, Congrats!\n" +
            "****************************\n\n"
        );
    }
    Console.WriteLine("END HERO STATS:\n");
    newHero.PrintHeroStats();
        
    Console.WriteLine("Do you want to play again? (yes/no)");
    gameLoop = Utils.ConfirmationDialog();
    Console.Clear();

} while (gameLoop == GameLoop.CONTINUE);

GameState PlayDungeon(Hero hero)
{
    for(int i = 0; i < NUMBER_OF_DUNGEON_WAVES; i++)
    {
        var enemy = CreateRandomEnemy();
        
        Console.WriteLine(
            $"DUNGEON WAVE {i+1}:\n" +
            $"**********************************\n");
        PrintHeroAndEnemyStats(hero, enemy);
        Console.WriteLine("Continue to start the fight! Press any key...");
        Utils.ConsoleClearAndContinue();

        bool heroAlive = FightEnemy(hero, enemy);
        
        if(!heroAlive)
            return GameState.LOSS;

        if (i == NUMBER_OF_DUNGEON_WAVES - 1)
            break;

        Console.Clear();
        PrintHeroAndEnemyStats(hero, enemy);
        Thread.Sleep(1500);
        Console.WriteLine(
            $"YOU HAVE DEFATED THE {enemy.Type}!\n" +
            $"***********************************\n"
        );

        hero.GainExperienceAndLevelUp(enemy.XP);
        hero.RegainHealthAfterBattle();

        Console.WriteLine("Continue to next enemy. ");
        Utils.ConsoleClearAndContinue();

        if (hero.XP > 0 && hero.HP < hero.HPThreshold)
        {
            Console.WriteLine("Do you want to spend your XP for Healh? (yes/no)");
            if (Utils.ConfirmationDialog() == GameLoop.CONTINUE)
            {
                var amount = InputExperiance(hero);
                hero.SpendXPforHP(amount);
            }
            Console.Clear();
        } 
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
            $"ROUND {round++} :\n" +
            $"*********************\n"
        );
        if (hero is Enchanter)
            hero.UseHeroAbility();
        InitiateCombatAndDecideOutcome(hero, enemy);
        Utils.ConsoleClearAndContinue();
    }
    return hero.HP > 0;
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

CombatOutcome IsHeroWinner(AttackType heroAttack, AttackType enemyAttack, Enemy enemy)
{
    if (enemy.IsStunned)
        return CombatOutcome.WIN;
        
    if(heroAttack == AttackType.DIRECT && enemyAttack == AttackType.SIDE)
        return CombatOutcome.WIN;
    if(heroAttack == AttackType.SIDE && enemyAttack == AttackType.COUNTER)
        return CombatOutcome.WIN;
    if(heroAttack == AttackType.COUNTER && enemyAttack == AttackType.DIRECT)
        return CombatOutcome.WIN;
    if (heroAttack == enemyAttack)
        return CombatOutcome.DRAW;  
    
    return CombatOutcome.LOSE;
}

void InitiateCombatAndDecideOutcome(Hero hero, Enemy enemy)
{
    var heroAttack = HeroChooseAttack();
    var enemyAttack = EnemyChooseAttack();
    var combatOutcome = IsHeroWinner(heroAttack, enemyAttack, enemy);
    Console.WriteLine("Attacking...");
    Thread.Sleep(1000);

    if (combatOutcome == CombatOutcome.WIN && enemy.IsStunned)
    {
        Console.WriteLine($"Enemy {enemy.Type} is stunned, Hero wins round!\n");
        Thread.Sleep(1000);
        hero.BasicAttack(enemy);
        enemy.IsStunned = false;
    }
    else if (combatOutcome == CombatOutcome.WIN)
    {
        Console.WriteLine($"Hero uses {AttacksString[heroAttack]} attack and beats Enemy {AttacksString[enemyAttack]} attack\n");
        Thread.Sleep(1000);
        hero.BasicAttack(enemy);
    }
    else if (combatOutcome == CombatOutcome.LOSE)
    {
        Console.WriteLine($"Enemy uses {AttacksString[enemyAttack]} attack and beats Hero {AttacksString[heroAttack]} attack\n");
        Thread.Sleep(1000);
        enemy.BasicAttack(hero);

    }
    else
    {
        Console.WriteLine($"Both Hero and Enemy used {AttacksString[heroAttack]} attack, DRAW!\n");
        return;
    }
    
    if (hero is Enchanter && hero.HP <= 0)
    {
        var enchanter = hero as Enchanter;
        if (enchanter!.HasRevive)
        {
            Console.WriteLine("Enchnter hero has died, do you want to use Revive? (yes/no)");
            if (Utils.ConfirmationDialog() == GameLoop.CONTINUE)
                enchanter.ReviveAbility();
        }
        else
            Console.WriteLine("Revive ability has been used before\n");
    }
}
Enemy CreateRandomEnemy()
{
   var random = new Random();
   var enemyType = EnemyChoiceProbability();
   var enemy = enemies[enemyType].Invoke();
   return enemy;
}
EnemyType EnemyChoiceProbability()
{
    var random = new Random();
    var choice = random.Next(1, 101);
    if (choice < 60)
        return EnemyType.Goblin;
    else if (choice >= 60 && choice < 90)
        return EnemyType.Brute;
    else
        return EnemyType.Witch;
}

int InputExperiance(Hero hero)
{
    int amount = 0;
    int halfXP = hero.XP / 2;
    do{
        
        Console.WriteLine($"CURRENT XP: {hero.XP} / {hero.XPThreshold}\n");
        Console.WriteLine($"CURRENT HP: {hero.HP} / {hero.HPThreshold}\n");
        amount = Utils.InputIntFormat($"Input amount of XP to spend (MAX is {halfXP})");
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


