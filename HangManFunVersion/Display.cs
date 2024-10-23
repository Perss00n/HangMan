namespace HangManFunVersion;
public class Display
{
    public void ShowMenu()
    {
        Logo();

        while (true)
        {
            Console.Clear();
            Logo();

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

            Logo();
            Console.WriteLine("Please enter a valid integer from 1-3. Try again...");
            Thread.Sleep(1500);
        }
    }

    public void StartSinglePlayerGame()
    {
        Game game = new Game();
        game.CreateNewUser();
        game.PlaySinglePlayerRound();
    }

    public void StartTwoPlayerGame()
    {
        Game game = new Game();
        game.StartTwoPlayerRound();
    }


    public static string BuildHangMan(int step)
    {
        return step switch
        {
            1 => "  +---+\r\n      |\r\n      |\r\n      |\r\n      |\r\n      |\r\n=========\r\n",
            2 => "  +---+\r\n  |   |\r\n      |\r\n      |\r\n      |\r\n      |\r\n=========\r\n",
            3 => "  +---+\r\n  |   |\r\n  O   |\r\n      |\r\n      |\r\n      |\r\n=========\r\n",
            4 => "  +---+\r\n  |   |\r\n  O   |\r\n  |   |\r\n      |\r\n      |\r\n=========\r\n",
            5 => "  +---+\r\n  |   |\r\n  O   |\r\n /|   |\r\n      |\r\n      |\r\n=========\r\n",
            6 => "  +---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n      |\r\n      |\r\n=========\r\n",
            7 => "  +---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n /    |\r\n      |\r\n=========\r\nTHIS IS YOUR LAST CHANCE!\r\n",
            8 => "  +---+\r\n  |   |\r\n  O   |\r\n /|\\  |\r\n / \\  |\r\n      |\r\n=========\r\nYOU ARE DEAD! GAME OVER!\r\n",
            9 => "  +---+\r\n      |\r\n \\O/  |\r\n  |   |\r\n / \\  |\r\n      |\r\n=========\r\nCONGRATULATIONS, YOU BROKE FREE!\r\n",
            _ => "Error! Something went wrong..."
        };
    }



    public static void Logo()
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