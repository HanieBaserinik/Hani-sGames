/******************************************************************************

                          Hanie_Baseri C# programming.
                      Object oriented, DR_Moghadam, 14033

*******************************************************************************/

using System;

namespace GameCollection
{
    // ***** Abstraction + Inheritance + Polymorphism
    abstract class Game //»»»Separation of Concerns	کلاس بازی از کلاس اجرای اصلی جداست
    
    {
        public abstract void Start();
        public abstract bool IsOver();
    }

    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    //***** کلاس GuessingGame از Game ارث بری 
    //*****Encapsulation	متغیرهای private در GuessingGame

    class GuessingGame : Game //Inheritance
    {
        private int targetNumber;
        private int attempts;
        private int maxAttempts;
        private int maxNumber;
        private bool hasWon;
        private Random rand;

        public GuessingGame(Difficulty difficulty)
        {
            rand = new Random();
            attempts = 0;
            hasWon = false;
         // تنظیمات سختی و آسونی بازی را قابل کنترل گرد کردم !
            switch (difficulty)
            {
                case Difficulty.Easy:
                    maxNumber = 50;
                    maxAttempts = 10;
                    break;
                case Difficulty.Medium:
                    maxNumber = 100;
                    maxAttempts = 7;
                    break;
                case Difficulty.Hard:
                    maxNumber = 200;
                    maxAttempts = 5;
                    break;
            }

            targetNumber = rand.Next(1, maxNumber + 1);
        }

        public override void Start()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"🎮 Starting Guessing Game! (1 - {maxNumber}, Attempts: {maxAttempts})");
            Console.ResetColor();

            while (!IsOver())
            {
                Console.Write("Your guess: ");
                string input = Console.ReadLine();
                int guess;

                if (int.TryParse(input, out guess))
                {
                    attempts++;
                    if (guess < targetNumber)
                        Console.WriteLine("🔻 Too Low!");
                    else if (guess > targetNumber)
                        Console.WriteLine("🔺 Too High!");
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"🎉 Correct! You guessed it in {attempts} tries.");
                        Console.ResetColor();
                        hasWon = true;
                        break;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("❌ Invalid input. Enter a number.");
                    Console.ResetColor();
                }
            }

            if (!hasWon)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"💥 Game Over! The correct number was {targetNumber}");
                Console.ResetColor();
            }
        }

        public override bool IsOver()
        {
            return hasWon || attempts >= maxAttempts;
        }
    }
// کلاس *****
    class Program
    {
        static void Main()
        {
            Console.Title = "🎯 Multi OOP Game by Hanie";

            while (true)
            {
                Console.WriteLine("🔵 Choose a game to play:");
                Console.WriteLine("1. Guessing Game");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Difficulty diff = ChooseDifficulty();
                    Game game = new GuessingGame(diff);  //  Polymorphism
                    game.Start();                        // اجرای نسخه خاص کلاس فرزند
                }
                else
                {
                    Console.WriteLine("❌ Invalid choice.");
                    continue;
                }

                Console.Write("🔁 Play again? (y/n): ");
                string again = Console.ReadLine().ToLower();
                if (again != "y") break;
                Console.Clear();
            }

            Console.WriteLine("👋 Goodbye!");
        }

        static Difficulty ChooseDifficulty()
        {
            Console.WriteLine("🧠 Choose Difficulty:");
            Console.WriteLine("1. Easy\n2. Medium\n3. Hard");

            while (true)
            {
                Console.Write("Your choice: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1": return Difficulty.Easy;
                    case "2": return Difficulty.Medium;
                    case "3": return Difficulty.Hard;
                    default:
                        Console.WriteLine("⚠️ Invalid input, try again.");
                        break;
                }
            }
        }
    }
}
