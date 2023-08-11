namespace Ex02_01
{
    /*Architeture choice: splitting the game logic into two sections:
     1. Game Control
     2. Game Manager
     
     The Game Control class is an aggregation of all the data and actions of the game.
     Nevertheless The GameControl is not independent and it does not drive the game.
     On the other hand the Game Manager is the class that drives the whole game.It has
     to manage different resources and methods. For this reason, any action that involves both 
     business logic and UI will be executed here.*/
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

            GameBoard.GeneralMessage("Please type your next guess <ABCD> or 'Q' to Quit");
            validGuess = getValidGuess();
            m_controller.AddTurn(validGuess);
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
                GameBoard.InformUserAboutVictory(m_controller.TurnsHistory.Count);
            }

            if (GameBoard.AskForAnotherRun())
            {
                Initiate();
            }
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
                    GameBoard.GeneralMessage(string.Format(
                        "This guess is not valid. A valid guess has exactly {0} none repeating letters", GameControl.GuessSize));
                }
            } 
            
            while (!inputIsPragmaticallyValid);

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
                    GameBoard.GeneralMessage("This number is out of range");
                }
            } 
            
            while (!guessesCountIsPragmaticallyValid);

            return guessesCount;
        }
    }
}