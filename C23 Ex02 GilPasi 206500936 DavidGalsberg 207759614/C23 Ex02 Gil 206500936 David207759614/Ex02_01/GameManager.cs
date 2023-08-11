namespace Ex02_01
{
    /*Architecture choice: splitting the game UI into two sections:
     1. Game Manager
     2. Game Board
     
     The Game Board class aggregate all the passive user-interface mechanisms.
     Meaning it is not independent and can be activated upon the Game manager's need.
     In other words the GameBoard act as an "improved console" and oriented to the Bulls & Cows game.
     The Game Manager class is somewhat an independent entity that drives the whole game.It has a completely 
     different role and it provide a high level blueprint of the game.
      */
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
                currentStatus = m_controller.EvaluateGameStatus();
            }
            
            while (currentStatus == eGameStatus.Ongoing);

            finishGame();
        }

        private void playTurn()
        {
            char[] validGuess;

            GameBoard.GeneralMessage("Please type your next guess <A B C D> or 'Q' to Quit");
            validGuess = getValidGuess();
            m_controller.AddTurn(validGuess);
            GameBoard.PrintState(m_controller.TurnsHistory);
            
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
  
            ExitGame: GameBoard.InformUserAboutQuit();
        }
        
        private char[] getValidGuess()
        {
            bool inputIsPragmaticallyValid;
            char[] userGuess;
            bool userWantToKeepPlaying;

            do
            {
                userGuess = GameBoard.GetSyntacticallyValidGuess(out userWantToKeepPlaying);
                 referUserQuit(userWantToKeepPlaying);
                inputIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidSequence(userGuess);

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
            
            do
            {
                guessesCount = GameBoard.GetSyntacticallyValidGuessesCount(GameControl.MinimumGuessesCount,
                    GameControl.MaximumGuessesCount, out bool userWantToQuit);
                referUserQuit(userWantToQuit);

                guessesCountIsPragmaticallyValid = GameControl.CheckIfPragmaticallyValidGuessesCount(guessesCount);
                if (!guessesCountIsPragmaticallyValid)
                {
                    GameBoard.GeneralMessage("This number is out of range");
                }
            } 
            
            while (!guessesCountIsPragmaticallyValid);

            return guessesCount;
        }

        public void referUserQuit(bool i_UserWantToQuit)
        {
            /*Architecture choice: using Environment.Exit instead of breaking all loops:
            Note that implicitly the game manager use 3 nested loops each turn:
            InitiateGame() LOOP => play() => getValidGuess() LOOP => GetSyntacticallyValidGuess LOOP
            Meaning that if a player want to quit, the message will have to climb the call stack all the way up.
            It makes more sense to cut the game flow on the firs game-manager level. This form will diminish
            future bugs and maintenance as well as providing a simpler software model.*/
            if (i_UserWantToQuit)
            {
                GameBoard.InformUserAboutQuit();
                Environment.Exit(0);
            }
        }
    }
}