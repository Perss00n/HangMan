namespace HangManFunVersion
{
    public class User
    {
        private string _userName = string.Empty;

        public string UserName
        {
            get { return char.ToUpper(_userName[0]) + _userName[1..].ToLower(); }
            set { _userName = value; }
        }

        public List<char> GuessedLetters { get; private set; } = new List<char>();
        public int WrongAnswers { get; set; } = 0;

        public int Score { get; private set; } = 0;

        public bool HasGuessedTheCorrectWord { get; set; }

        public void AddGuessedLetter(char letter)
        {
            GuessedLetters.Add(letter);
        }

        public void IncreaseScore()
        {
            Score++;
        }

        public void ResetGuessedLetters()
        {
            GuessedLetters.Clear();
        }

        public void ResetWrongAnswers()
        {
            WrongAnswers = 0;
        }

        public void ResetHasGuessedTheCorrectWord()
        {
            HasGuessedTheCorrectWord = false;
        }

    }
}