namespace HangManFunVersion
{
    public class Game
    {
        public string? SecretWord { get; private set; }
        public char[]? CorrectLetters { get; private set; }
        public bool IsGuessFound { get; private set; }

        public void MakeSecretWord()
        {
            string[] wordsToGuess;
            try
            {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = Path.Combine(exeDirectory, "words.txt");
                wordsToGuess = File.ReadAllLines(filePath);
            }
            catch (FileNotFoundException)
            {
                wordsToGuess = [ "ocean", "mountain", "laptop", "sunshine", "bicycle", "elephant", "pizza", "strawberry",
                                "jungle", "library", "universe", "airplane", "football", "butterfly", "spaceship" ];
            }
            SecretWord = wordsToGuess[Random.Shared.Next(wordsToGuess.Length)];
            CorrectLetters = new char[SecretWord.Length];

            Array.Fill(CorrectLetters, '_');
        }

        public void PlaySinglePlayerRound(User user)
        {
            MakeSecretWord();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Welcome, {user.UserName}!\nYou have 8 chances to guess the letters and save the man from his grim fate.\n" +
                              $"Each incorrect guess brings him closer to doom. Choose wisely!");
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);
            do
            {
                Console.WriteLine(String.Join(" ", CorrectLetters!));
                if (user.WrongAnswers > 0)
                    Console.WriteLine($"Guessed Letters: {String.Join(", ", user.GuessedLetters)}");
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine()!.Trim().ToLower();

                if (input.Length > 1 || String.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (user.WrongAnswers > 0)
                        Console.WriteLine(Display.BuildHangMan(user.WrongAnswers));
                    Console.WriteLine("Guess can't be empty and can only contain 1 letter! Try again...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    continue;
                }



                char userGuess = input[0];
                IsGuessFound = false;
                CheckGuess(userGuess, user);
                CheckForWin(user);
                if (user.WrongAnswers == 8)
                    break;

            } while (!user.HasGuessedTheCorrectWord);

            PlayAgain(user);

        }

        public void PlayTwoPlayerRound()
        {

        }





        private void PlayAgain(User user)
        {
            Console.Write("Do you want to play again? (y/n): ");

            if (Console.ReadLine()!.Trim().ToLower() == "y")
            {
                Console.Clear();
                user.ResetWrongAnswers();
                user.ResetGuessedLetters();
                user.ResetHasGuessedTheCorrectWord();
                PlaySinglePlayerRound(user);

            }
            else
            {
                Console.WriteLine($"Thank you for playing, {user.UserName}!");
                Console.WriteLine($"You'r total score was '{user.Score}' See ya around!");
            }
        }

        public void CheckGuess(char guess, User user)
        {

            if (user.GuessedLetters.Contains(guess))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                if (user.WrongAnswers > 0)
                    Console.WriteLine(Display.BuildHangMan(user.WrongAnswers));
                Console.WriteLine($"You have already guessed the letter '{guess}'");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                return;
            }


            for (int i = 0; i < SecretWord!.Length; i++)
            {
                if (guess == SecretWord[i])
                {
                    CorrectLetters![i] = guess;
                    IsGuessFound = true;
                }
            }

            if (!IsGuessFound)
            {
                user.WrongAnswers++;
                user.AddGuessedLetter(guess);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                if (user.WrongAnswers > 0)
                    Console.WriteLine(Display.BuildHangMan(user.WrongAnswers));
                Console.WriteLine($"{(user.WrongAnswers == 8 ? "" : $"{char.ToUpper(guess)} is not correct! Please try again...")}");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);

            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                if (user.WrongAnswers > 0)
                    Console.WriteLine(Display.BuildHangMan(user.WrongAnswers));
                Console.WriteLine($"{Char.ToUpper(guess)} was right! Please continue guess...");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void CheckForWin(User user)
        {
            if (!CorrectLetters!.Contains('_'))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Display.BuildHangMan(9));
                Console.WriteLine(String.Join(" ", CorrectLetters!));
                Console.ForegroundColor = ConsoleColor.White;
                user.HasGuessedTheCorrectWord = true;
                user.IncreaseScore();
            }
        }


        public User CreateNewUser()
        {
            string input;
            const int minUserNameLength = 2;
            Display.DisplayLogo();
            User user = new User();
            while (true)
            {
                Console.Write("Please enter your username: ");
                input = Console.ReadLine()!.Trim();

                if (string.IsNullOrEmpty(input) || input.Length < minUserNameLength)
                {
                    Console.Clear();
                    Display.DisplayLogo();
                    Console.WriteLine($"Error! Name cannot be empty and must contain at least {minUserNameLength} characters! Please try again...");
                    Thread.Sleep(1000);
                    continue;
                }
                Console.Clear();
                user.UserName = input;
                break;
            }

            return user;
        }


    }
}