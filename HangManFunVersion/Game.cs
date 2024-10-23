namespace HangManFunVersion
{
    public class Game
    {
        private User? user;
        public string? SecretWord { get; private set; }
        public char[]? CorrectLetters { get; private set; }
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
                wordsToGuess = new string[] { "ocean", "mountain", "laptop", "sunshine", "bicycle", "elephant", "pizza", "strawberry",
                                              "jungle", "library", "universe", "airplane", "football", "butterfly", "spaceship" };
            }
            SecretWord = wordsToGuess[Random.Shared.Next(wordsToGuess.Length)];
            CorrectLetters = new char[SecretWord.Length];
            Array.Fill(CorrectLetters, '_');
        }

        public void PlaySinglePlayerRound()
        {
            MakeSecretWord();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Welcome, {user!.UserName}!\nYou have 8 chances to guess the letters and save the man from his grim fate.\n" +
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

                if (input.Length > 1 || String.IsNullOrEmpty(input) || !char.IsLetter(input[0]))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (user.WrongAnswers > 0)
                        Console.WriteLine(Display.BuildHangMan(user.WrongAnswers));
                    Console.WriteLine("Error! Guess can't be empty and must contain at least 1 letter, and only letters! Try again...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    continue;
                }

                char userGuess = input[0];

                CheckGuess(userGuess, user);

                CheckForWin(user);
                if (user.WrongAnswers == 8)
                    break;

            } while (!user.HasGuessedTheCorrectWord);

            bool playAgain = AskPlayAgainSinglePlayer();
        }

        public bool AskPlayAgainSinglePlayer()
        {
            Console.Write("Do you want to play again? (y/n): ");

            if (Console.ReadLine()?.Trim().ToLower() == "y")
            {
                Console.Clear();
                user!.ResetRound();
                PlaySinglePlayerRound();
                return true;
            }
            else
            {
                Console.WriteLine($"Thank you for playing, {user!.UserName}!");
                Console.WriteLine($"Your total score was '{user.Score}' See ya around!");
                return false;
            }
        }

        public void CheckGuess(char guess, User guesser)
        {
            if (guesser.GuessedLetters.Contains(guess))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                if (guesser.WrongAnswers > 0)
                    Console.WriteLine(Display.BuildHangMan(guesser.WrongAnswers));
                Console.WriteLine($"You have already guessed the letter '{guess}'");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                return;
            }

            bool isGuessFound = false;

            for (int i = 0; i < SecretWord!.Length; i++)
            {
                if (guess == SecretWord[i])
                {
                    CorrectLetters![i] = guess;
                    isGuessFound = true;
                }
            }

            if (!isGuessFound)
            {
                guesser.IncrementWrongAnswers();
                guesser.AddGuessedLetter(guess);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Display.BuildHangMan(guesser.WrongAnswers));
                Console.WriteLine($"{(guesser.WrongAnswers == 8 ? "" : $"{char.ToUpper(guess)} is not correct! Please try again...")}");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
            }
            else
            {
                Console.Clear();
                guesser.AddGuessedLetter(guess);
                Console.ForegroundColor = ConsoleColor.Green;
                if (guesser.WrongAnswers > 0)
                    Console.WriteLine(Display.BuildHangMan(guesser.WrongAnswers));
                Console.WriteLine($"{Char.ToUpper(guess)} was right! Please continue guessing...");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void CheckForWin(User guesser)
        {
            if (!CorrectLetters!.Contains('_'))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(Display.BuildHangMan(9));
                Console.WriteLine(String.Join(" ", CorrectLetters!));
                Console.ForegroundColor = ConsoleColor.White;
                guesser.UpdateHasGuessedTheCorrectWord();
                guesser.IncreaseScore();
            }
            else if (guesser.WrongAnswers == 8)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Display.BuildHangMan(8));
                Console.WriteLine($"The word was: {SecretWord}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void CreateNewUser()
        {
            string input;
            const int minUserNameLength = 2;
            bool isInvalidInput;
            user = new User();
            Display.Logo();

            do
            {
                Console.Write("Please enter your username: ");
                input = Console.ReadLine()!.Trim();
                isInvalidInput = string.IsNullOrEmpty(input) || input.Length < minUserNameLength;

                if (isInvalidInput)
                {
                    Console.Clear();
                    Display.Logo();
                    Console.WriteLine($"Error! Name can't be empty and must contain at least {minUserNameLength} characters! Please try again...");
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.Clear();
                    user.UserName = input;
                }
            } while (isInvalidInput);
        }

        public void StartTwoPlayerRound()
        {
            User player1 = new User();
            User player2 = new User();

            Console.Clear();
            Display.Logo();
            Console.WriteLine("Welcome to the two-player mode!\n");

            string input;
            bool isInvalidInputUser1;
            bool isInvalidInputUser2;
            const int minUserNameLength = 2;

            do
            {
                Console.Write("Player 1, please enter your username: ");
                input = Console.ReadLine()!.Trim();
                isInvalidInputUser1 = string.IsNullOrEmpty(input) || input.Length < minUserNameLength;
                if (isInvalidInputUser1)
                {
                    Console.Clear();
                    Display.Logo();
                    Console.WriteLine($"Error! Name cannot be empty and must contain at least {minUserNameLength} characters! Please try again...");
                    Thread.Sleep(1000);
                }
            } while (isInvalidInputUser1);

            player1.UserName = input;

            string input2;
            do
            {
                Console.Write("Player 2, please enter your username: ");
                input2 = Console.ReadLine()!.Trim();
                isInvalidInputUser2 = string.IsNullOrEmpty(input2) || input2.Length < minUserNameLength;
                if (isInvalidInputUser2)
                {
                    Console.Clear();
                    Display.Logo();
                    Console.WriteLine($"Error! Name cannot be empty and must contain at least {minUserNameLength} characters! Please try again...");
                    Thread.Sleep(1000);
                }
            } while (isInvalidInputUser2);

            player2.UserName = input2;

            bool playAgain;
            bool isInvalidSecretWordP1;
            bool isInvalidSecretWordP2;
            do
            {
                Console.Clear();
                Console.WriteLine($"{player1.UserName}, it's your turn to set the secret word for {player2.UserName}.");
                do
                {
                    Console.Write("Enter the secret word: ");
                    SecretWord = Console.ReadLine()!.Trim().ToLower();
                    isInvalidSecretWordP1 = string.IsNullOrEmpty(SecretWord) || SecretWord.Length < 5 || !SecretWord.All(char.IsLetter);
                    if (isInvalidSecretWordP1)
                    {
                        Console.Clear();
                        Console.WriteLine("Error! The secret word can't be empty and must contain at least 5 letters, and only letters! Try again...");
                        Thread.Sleep(1000);
                    }
                } while (isInvalidSecretWordP1);

                Console.Clear();
                Console.WriteLine($"{player2.UserName}, it's your turn to guess {player1.UserName}'s word.");
                PlayTwoPlayerRound(player2);

                Console.Clear();
                Console.WriteLine($"{player2.UserName}, it's your turn to set the secret word for {player1.UserName}.");
                do
                {
                    Console.Write("Enter the secret word: ");
                    SecretWord = Console.ReadLine()!.Trim().ToLower();
                    isInvalidSecretWordP2 = string.IsNullOrEmpty(SecretWord) || SecretWord.Length < 5 || !SecretWord.All(char.IsLetter);
                    if (isInvalidSecretWordP2)
                    {
                        Console.Clear();
                        Console.WriteLine("Error! The secret word can't be empty and must contain at least 5 letters, and only letters! Try again...");
                        Thread.Sleep(1000);
                    }
                } while (isInvalidSecretWordP2);

                Console.Clear();
                Console.WriteLine($"{player1.UserName}, it's your turn to guess {player2.UserName}'s word.");
                PlayTwoPlayerRound(player1);

                player1.ResetRound();
                player2.ResetRound();

                playAgain = AskPlayAgainTwoPlayer();

            } while (playAgain);

            Console.WriteLine($"\nThank you for playing, {player1.UserName} and {player2.UserName}!");
            Console.WriteLine($"Final score: {player1.UserName}: {player1.Score} points, {player2.UserName}: {player2.Score} points.");
            Console.WriteLine(player1.Score > player2.Score ? $"{player1.UserName} is the winner!" :
                              player2.Score > player1.Score ? $"{player2.UserName} is the winner!" :
                              "The game is a draw!");
        }

        public void PlayTwoPlayerRound(User guesser)
        {
            Console.Clear();
            CorrectLetters = new char[SecretWord!.Length];
            Array.Fill(CorrectLetters, '_');

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine($"Welcome, {guesser.UserName}!\nYou have 8 chances to guess the letters and save the man from his grim fate.\n" +
                              $"Each incorrect guess brings him closer to doom. Choose wisely!");
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(2000);

            do
            {
                Console.WriteLine(String.Join(" ", CorrectLetters));
                if (guesser.WrongAnswers > 0)
                    Console.WriteLine($"Guessed Letters: {String.Join(", ", guesser.GuessedLetters)}");

                Console.Write("Enter your guess: ");
                string input = Console.ReadLine()!.Trim().ToLower();

                if (input.Length > 1 || String.IsNullOrEmpty(input) || !char.IsLetter(input[0]))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (guesser.WrongAnswers > 0)
                        Console.WriteLine(Display.BuildHangMan(guesser.WrongAnswers));
                    Console.WriteLine("Error! Guess can't be empty and must contain at least 1 letter, and only letters! Try again...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Thread.Sleep(1000);
                    continue;
                }

                char userGuess = input[0];

                CheckGuess(userGuess, guesser);

                CheckForWin(guesser);
                if (guesser.WrongAnswers == 8)
                    break;

            } while (!guesser.HasGuessedTheCorrectWord);
        }

        private bool AskPlayAgainTwoPlayer()
        {
            Console.Write("Do you want to play another round? (y/n): ");
            return Console.ReadLine()?.Trim().ToLower() == "y";
        }
    }
}