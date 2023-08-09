namespace sapir_c23_dn_course_gil_and_david.Ex02_01;

public class GameManager
{
    private GameControl m_controller;
    
    public void Initiate()
    {
        
        GameBoard.WelcomePlayer();
        uint validGuessCount = getValidGuessCount();
        m_controller = new GameControl(validGuessCount);
        eGameStatus currentStatus;
        do
        {
            GameBoard.PrintState(m_controller.TurnsHistory);
            play();
            currentStatus = m_controller.EvaluateGameStatus();

            if (currentStatus == eGameStatus.Defeat)
            {
                GameBoard.InformDefeat();
            }
            else if (currentStatus == eGameStatus.Victory)
            {
                GameBoard.InformVictory();
            }
        } while (currentStatus == eGameStatus.Ongoing);
        
        
    }

    private void play()
    {
        Console.WriteLine("Enter your guess 🤔");
        char[] validGuess = getValidGuess();
        m_controller.AddTurn(validGuess);
    }

    private char[] getValidGuess()
    {
        bool inputIsPragmaticallyValid;
        char[] userGuess;
        
        do
        {
            userGuess  = GameBoard.GetSyntacticallyValidGuess();
            inputIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidSequence(userGuess);
            if (!inputIsPragmaticallyValid)
            {
                Console.WriteLine();
            }

        } while (!inputIsPragmaticallyValid);
        return userGuess;
    }

    private uint getValidGuessCount()
    {
        uint guessesCount;
        bool guessesCountIsPragmaticallyValidGuessesCount;
        do
        {
            const char bottomBound = '4', topBound = '8';
            guessesCount = GameBoard.GetSyntacticallyValidGuessesCount(bottomBound, topBound);
            guessesCountIsPragmaticallyValidGuessesCount = GameControl.CheckIfPragmaticallyValidGuessesCount(guessesCount);
            if (!guessesCountIsPragmaticallyValidGuessesCount)
            {
                Console.WriteLine("This number is out of range");
            }
        } while (!guessesCountIsPragmaticallyValidGuessesCount);

        return guessesCount;
    }
}