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
                playTurn();
                GameBoard.PrintState(m_controller.TurnsHistory);
                currentStatus = m_controller.EvaluateGameStatus();

            }
            
            while (currentStatus == eGameStatus.Ongoing);
            finishGame();
        }

        private void playTurn()
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
            bool playerWantToQuit;

            do
            {
                userGuess = GameBoard.GetSyntacticallyValidGuess(out playerWantToQuit);
                referPlayerQuit(playerWantToQuit);
                inputIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidSequence(userGuess);
                if (playerWantToQuit)
                {
                    GameControl.AbandonGame();
                }

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
            const char bottomBound = '4', topBound = '8';
            
            do
            {
                
                guessesCount = GameBoard.GetSyntacticallyValidGuessesCount(bottomBound, topBound, out bool playerWantToQuit);
                referPlayerQuit(playerWantToQuit);
                guessesCountIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidGuessesCount(guessesCount);
                if (!guessesCountIsPragmaticallyValid)
                {
                    Console.WriteLine("This number is out of range");
                }
            } 
            
            while (!guessesCountIsPragmaticallyValid);

            return guessesCount;
        }

        private void referPlayerQuit(bool i_PlayerWantToQuit)
        {
            if (i_PlayerWantToQuit)
            {
                GameBoard.InformUserAboutQuit();
                GameControl.AbandonGame();
            }
        }

        private void finishGame()
        {
            //Treat the correct sequence as if it was a turn
            GameBoard.PrintState(m_controller.TurnsHistory, GameBoard.TurnStringifier.TurnToString(
                new Turn(m_controller.CorrectSequence)));
            if (m_controller.EvaluateGameStatus() == eGameStatus.Defeat)
            {
                GameBoard.InformUserAboutDefeat();
            }
            else
            {
                GameBoard.InformUserAboutVictory();
            }

            if (GameBoard.AskForAnotherRun())
            {
                Initiate();
            }
        }
    }
}