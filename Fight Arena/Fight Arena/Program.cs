using System;
using System.Collections.Generic;

public abstract class Hero
{
    private Random random;

    public string Name { get; private set; }
    public int HitPoints { get; protected internal set; }
    public int Attack { get; protected set; }
    public int Defense { get; protected set; }

    public Hero(string name)
    {
        Name = name;
        random = new Random();
    }

    public void AttackOpponent(Hero opponent)
    {
        int damage = Math.Max(0, Attack - opponent.Defense);
        opponent.HitPoints -= damage;
    }

    protected int GenerateRandomNumber(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public virtual void DisplayStats()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Hit Points: {HitPoints}");
        Console.WriteLine($"Attack: {Attack}");
        Console.WriteLine($"Defense: {Defense}");
    }

    public abstract int GetPlayerAttack();
    public abstract int GetPlayerDefense();
}


//over here I have all my heroes

//hero 1
public class KongFuHarry : Hero
{
    public KongFuHarry() : base("Kong Fu Harry")
    {
        HitPoints = 120;
        Attack = 2;
        Defense = 5;
    }

    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

//hero 2
public class SuperDog : Hero
{
    public SuperDog() : base("Super Dog")
    {
        HitPoints = 70;
        Attack = GenerateRandomNumber(6, 8);
        Defense = GenerateRandomNumber(2, 8);
    }

    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

//hero 3
public class FastKarl : Hero
{
    public FastKarl() : base("Fast Karl")
    {
        HitPoints = 90;
        Attack = GenerateRandomNumber(2, 5);
        Defense = 3;
    }

    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

//hero 4
public class PoisonGunner : Hero
{
    public PoisonGunner() : base("Poison Gunner")
    {
        HitPoints = 100;
        Attack = GenerateRandomNumber(1, 13);
        Defense = 5;
    }

    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

//hero 5
public class MiniMouseMikkle : Hero
{
    public MiniMouseMikkle() : base("Mini Mouse Mikkle")
    {
        HitPoints = 40;
        Attack = 9;
        Defense = 9;
    }

    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

//hero 6
public class IvanTheRubberGoat : Hero
{
    public IvanTheRubberGoat() : base("Ivan The Rubber Goat")
    {
        HitPoints = 70;
        Attack = 6;
        Defense = 6;
    }
    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

//hero 7
public class ElkEgon : Hero
{
    public ElkEgon() : base("Elk Egon")
    {
        HitPoints = 90;
        Attack = GenerateRandomNumber(5, 11);
        Defense = 4;
    }

    public override int GetPlayerAttack()
    {
        return Attack;
    }

    public override int GetPlayerDefense()
    {
        return Defense;
    }
}

// Implement other hero classes similarly

public class FightArena
{
    private static List<Hero> heroes;
    private static Hero player1;
    private static Hero player2;
    private static int matchCount;

    public static void Main()
    {
        matchCount = 0;
        InitializeHeroes();
        RunTournament();
    }

    private static void InitializeHeroes()
    {
        heroes = new List<Hero>()
        {
            new KongFuHarry(),
            new SuperDog(),
            new FastKarl(),
            new PoisonGunner(),
            new MiniMouseMikkle(),
            new IvanTheRubberGoat(),
            new ElkEgon()
            // Add other hero instances here
        };
    }

    private static void RunTournament()
    {
        while (heroes.Count > 1)
        {
            matchCount++;

            Console.WriteLine($"=== Match {matchCount} ===");
            Console.WriteLine();

            SelectPlayers();

            Hero winner = RunMatch(player1, player2);

            if (winner == player1)
            {
                Console.WriteLine("Congratulations! You won the match!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Better Luck Next Time!");
                Console.WriteLine("Game Over!");
                return;
            }

            heroes.Remove(player2);
            player1 = winner;
            player2 = null;
        }

        Console.WriteLine("Congratulations! You won the tournament!");
        Console.WriteLine("Game Over!");
    }

    private static void SelectPlayers()
    {
        Console.WriteLine("Available Heroes:");
        for (int i = 0; i < heroes.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {heroes[i].Name}");
        }

        Console.WriteLine("Select Player 1:");
        player1 = SelectPlayer();

        Console.WriteLine("Select Player 2 (Computer):");
        player2 = SelectRandomPlayer();
    }

    private static Hero SelectPlayer()
    {
        int selection = ReadUserInput(1, heroes.Count);
        return heroes[selection - 1];
    }

    private static Hero SelectRandomPlayer()
    {
        Random random = new Random();
        int selection = random.Next(0, heroes.Count);
        return heroes[selection];
    }

    private static int ReadUserInput(int minValue, int maxValue)
    {
        int input;
        bool validInput = false;

        do
        {
            Console.Write($"Enter a number ({minValue}-{maxValue}): ");
            string inputStr = Console.ReadLine();

            if (int.TryParse(inputStr, out input) && input >= minValue && input <= maxValue)
                validInput = true;

        } while (!validInput);

        return input;
    }

    private static Hero RunMatch(Hero hero1, Hero hero2)
    {
        Console.WriteLine("Match: " + hero1.Name + " vs " + hero2.Name);
        Console.WriteLine();

        while (true)
        {
            if (hero1 == player1)
                PlayUserMatch(hero1, hero2);
            else
                PlayComputerMatch(hero1, hero2);

            if (hero2.HitPoints <= 0)
                return hero1;
            if (hero2 == player1)
                PlayUserMatch(hero2, hero1);
            else
                PlayComputerMatch(hero2, hero1);
            if (hero1.HitPoints <= 0)
                return hero2;
        }
    }

    private static void PlayUserMatch(Hero userHero, Hero opponent)
    {
        Console.WriteLine("Match: " + userHero.Name + " vs " + opponent.Name);

        DisplayMatchStats(userHero, opponent);

        Console.WriteLine("Your turn!");
        bool isAttacking = ReadUserInputAttackDefense();
        if (isAttacking)
        {
            int userAttack = userHero.GetPlayerAttack();
            opponent.HitPoints -= userAttack;
            Console.WriteLine("You chose to attack!");
        }
        else
        {
            int userDefense = userHero.GetPlayerDefense();
            userHero.HitPoints += userDefense;
            Console.WriteLine("You chose to defend!");
        }

        if (opponent.HitPoints <= 0)
        {
            Console.WriteLine("Congratulations! You won the match!");
            return;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void PlayComputerMatch(Hero computerHero, Hero opponent)
    {
        Console.WriteLine("Match: " + computerHero.Name + " vs " + opponent.Name);

        DisplayMatchStats(computerHero, opponent);

        Random random = new Random();
        bool isAttacking = random.Next(2) == 0;

        if (isAttacking)
        {
            int computerAttack = computerHero.GetPlayerAttack();
            opponent.HitPoints -= computerAttack;
            Console.WriteLine("Computer chose to attack!");
        }
        else
        {
            int computerDefense = computerHero.GetPlayerDefense();
            computerHero.HitPoints += computerDefense;
            Console.WriteLine("Computer chose to defend!");
        }

        if (opponent.HitPoints <= 0)
        {
            Console.WriteLine("Computer won the match!");
            return;
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static bool ReadUserInputAttackDefense()
    {
        while (true)
        {
            Console.WriteLine("Choose your action:");
            Console.WriteLine("1. Attack");
            Console.WriteLine("2. Defend");

            int selection = ReadUserInput(1, 2);

            if (selection == 1)
                return true;
            else if (selection == 2)
                return false;
        }
    }

    private static void DisplayMatchStats(Hero hero1, Hero hero2)
    {
        Console.WriteLine("---------------");
        Console.WriteLine(hero1.Name);
        Console.WriteLine($"Hit Points: {hero1.HitPoints}");
        Console.WriteLine("---------------");
        Console.WriteLine(hero2.Name);
        Console.WriteLine($"Hit Points: {hero2.HitPoints}");
        Console.WriteLine("---------------");
    }
}