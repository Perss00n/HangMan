namespace HangManFunVersion;
public class Display
{
    public void ShowMenu()
    {
        DisplayLogo();

        while (true)
        {
            Console.Clear();
            DisplayLogo();

            Console.WriteLine("1. Play Game (single player)");
            Console.WriteLine("2. Play Game (two players)");
            Console.WriteLine("3. Quit Game");
            Console.Write("\nPlease select an option from the list: ");

            if (Int32.TryParse(Console.ReadLine(), out int input))
            {
                switch (input)
                {
                    case 1:
                        Console.Clear();
                        StartSinglePlayerGame();
                        return;
                    case 2:
                        Console.Clear();
                        StartTwoPlayerGame();
                        return;
                    case 3:
                        Console.WriteLine("Exiting the game...");
                        return;
                    default:
                        break;
                }
            }

            DisplayLogo();
            Console.WriteLine("Please enter a valid integer from 1-3. Try again...");
            Thread.Sleep(1500);
        }
    }

    public void StartSinglePlayerGame()
    {
        Game game = new Game();
        game.CreateNewUser();
        game.PlayRoundSinglePlayer();
    }

    public void StartTwoPlayerGame()
    {
        Game game = new Game();
        game.StartTwoPlayerRound();
    }


    public static string BuildHangMan(int step)
    {

        switch (step)
        {
            case 1:
                return "  +---+\r\n      |\r\n      |\r\n      |\r\n      |\r\n      |\r\n=========\r\n";
            case 2:
                return "  +---+\r\n  |   |\r\n      |\r\n      |\r\n      |\r\n      |\r\n=========\r\n";
            case 3:
                return "  +---+\r\n  |   |\r\n  O   |\r\n      |\r\n      |\r\n      |\r\n=========\r\n";
            case 4:
                return "  +---+\r\n  |   |\r\n  O   |\r\n  |   |\r\n      |\r\n      |\r\n=========\r\n";
            case 5:
                return "  +---+\r\n  |   |\r\n  O   |\r\n /|   |\r\n      |\r\n      |\r\n=========\r\n";
            case 6:
                return "  +---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n      |\r\n      |\r\n=========\r\n";
            case 7:
                return "  +---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n /    |\r\n      |\r\n=========\r\nTHIS IS YOUR LAST CHANCE!\r\n";
            case 8:
                return "  +---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n / \\  |\r\n      |\r\n=========\r\nYOU ARE DEAD! GAME OVER!\r\n";
            case 9:
                return "  +---+\r\n      |\r\n \\O/  |\r\n  |   |\r\n / \\  |\r\n      |\r\n=========\r\nCONGRATULATIONS, YOU BROKE FREE!\r\n";
            default:
                return "Error! Something went wrong...";
        }

    }

    public static void DisplayLogo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"################################################");
        Console.WriteLine(@"#            H A N G M A N   1 . 0             #");
        Console.WriteLine(@"#                                              #");
        Console.WriteLine(@"#           ________                           #");
        Console.WriteLine(@"#           |      |                           #");
        Console.WriteLine(@"#           |      |                           #");
        Console.WriteLine(@"#           |      O                           #");
        Console.WriteLine(@"#           |     /|\                          #");
        Console.WriteLine(@"#           |     / \                          #");
        Console.WriteLine(@"#           |                                  #");
        Console.WriteLine(@"#        ___|___        Coded by Marcus Lehm   #");
        Console.WriteLine(@"#                                              #");
        Console.WriteLine(@"################################################");
        Console.ForegroundColor = ConsoleColor.White;
    }
}