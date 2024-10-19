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

        public List<char> GuessedLetters { get; } = new List<char>();
        public int WrongAnswers { get; private set; }

        public int Score { get; private set; }

        public bool HasGuessedTheCorrectWord { get; private set; }

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

        public void UpdateHasGuessedTheCorrectWord()
        {
            HasGuessedTheCorrectWord = true;
        }

        public void IncrementWrongAnswers()
        {
            WrongAnswers++;
        }

    }
}