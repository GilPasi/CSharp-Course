namespace Ex02_01
{
    public class GameManager
    {
        private GameControl m_controller;

        public void Initiate()
        {
            eGameStatus currentStatus;
            uint validGuessCount;
            
            GameBoard.WelcomePlayer();
            validGuessCount = getValidGuessesCount();
            m_controller = new GameControl(validGuessCount);
            GameBoard.PrintState(m_controller.TurnsHistory);

            do
            {
                play();
                GameBoard.PrintState(m_controller.TurnsHistory);
                currentStatus = m_controller.EvaluateGameStatus();

            }while (currentStatus == eGameStatus.Ongoing);

            finishGame();
        }

        private void play()
        {
            char[] validGuess;
            
            Console.WriteLine("Enter your guess 🤔");
            validGuess = getValidGuess();
            m_controller.AddTurn(validGuess);
        }

        private char[] getValidGuess()
        {
            bool inputIsPragmaticallyValid;
            char[] userGuess;

            do
            {
                userGuess = GameBoard.GetSyntacticallyValidGuess();
                inputIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidSequence(userGuess);
                if (!inputIsPragmaticallyValid)
                {
                    Console.WriteLine("This guess is not valid. A valid guess has exactly {0} none repeating letters",
                        GameControl.GuessSize);
                }
            } while (!inputIsPragmaticallyValid);

            return userGuess;
        }

        private uint getValidGuessesCount()
        {
            uint guessesCount;
            bool guessesCountIsPragmaticallyValid;
            
            do
            {
                const char bottomBound = '4', topBound = '8';
                guessesCount = GameBoard.GetSyntacticallyValidGuessesCount(bottomBound, topBound);
                guessesCountIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidGuessesCount(guessesCount);
                if (!guessesCountIsPragmaticallyValid)
                {
                    Console.WriteLine("This number is out of range");
                }
            } while (!guessesCountIsPragmaticallyValid);

            return guessesCount;
        }

        private void finishGame()
        {
            
            GameBoard.PrintState(m_controller.TurnsHistory, GameBoard.TurnStringifier.TurnToString(
                new Turn(m_controller.CorrectSequence, null)));
            if (m_controller.EvaluateGameStatus() == eGameStatus.Defeat)
            {
                GameBoard.InformDefeat();
            }
            else
            {
                GameBoard.InformVictory();
            }
        }
    }
}