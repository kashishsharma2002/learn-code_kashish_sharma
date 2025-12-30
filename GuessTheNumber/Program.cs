using System;

namespace GuessTheNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayGuessingGame();
        }
        
        static void PlayGuessingGame()
        {
            int numberToGuess = GetRandomNumber();
            int guessCounter = 0;
            bool hasGuessedCorrectly = false;

            Console.WriteLine("Guess a number between 1 and 100:");

            while (!hasGuessedCorrectly)
            {
                int userGuess = ReadValidUserGuess();
                guessCounter++;

                hasGuessedCorrectly = CheckGuess(userGuess, numberToGuess);

                if (hasGuessedCorrectly)
                {
                    Console.WriteLine($"You guessed it in {guessCounter} guesses.");
                }
            }
        }

        static int GetRandomNumber()
        {
            Random random = new Random();
            return random.Next(1, 101);
        }

        static int ReadValidUserGuess()
        {
            while (true)
            {
                string userInput = Console.ReadLine();

                if (!IsValidNumber(userInput))
                {
                    Console.WriteLine("I won't count this one. Please enter a number between 1 and 100:");
                    continue;
                }

                return int.Parse(userInput);
            }
        }

        static bool IsValidNumber(string input)
        {
            return int.TryParse(input, out int number) && IsInRange(number);
        }

        static bool IsInRange(int number)
        {
            return number >= 1 && number <= 100;
        }

        static bool CheckGuess(int userGuess, int numberToGuess)
        {
            if (userGuess < numberToGuess)
            {
                Console.WriteLine("Too low! Try again:");
                return false;
            }
            else if (userGuess > numberToGuess)
            {
                Console.WriteLine("Too high! Try again:");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
