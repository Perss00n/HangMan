namespace HangManFunVersion;

internal class Program
{
    static void Main(string[] args)
    {
        Display display = new Display();
        display.ShowMenu();
        Console.ReadLine();
    }
}