using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Ex02_01
{
    public class GameBoard
    {
        private const string k_verticalDelimiter = "|";
        private const string k_horizonalDelimiter = "|=========|=========|";
        private const string k_QuitMessage = "Q";
        private static readonly string sr_emptySequence;
        private static readonly string sr_hiddenSequence;

        static GameBoard()
        {
            sr_hiddenSequence = string.Format("{0} # # # # {0}         {0}",
                k_verticalDelimiter);
            sr_emptySequence =  string.Format("{0}         {0}         {0}",
                k_verticalDelimiter);
        }

        public static void PrintState(List<Turn> i_Turns, string i_CorrectSequence = null)
        {
            int i;
            
            Console.WriteLine("Current board status:");
            Console.WriteLine("{0}Pins:    {0}Result:  {0}",k_verticalDelimiter);
            Console.WriteLine(k_horizonalDelimiter);
            if (i_CorrectSequence == null)
                //Meaning no string was sent as a parameter
            {
                Console.WriteLine(sr_hiddenSequence);
            }
            else
            {
                Console.WriteLine(i_CorrectSequence);
            }

            Console.WriteLine(k_horizonalDelimiter);
            i = 0;
            foreach (Turn passedTurn in i_Turns)
            {
                Console.WriteLine(TurnStringifier.TurnToString(passedTurn));
                Console.WriteLine(k_horizonalDelimiter);
                i++;
            }
            
            for (; i < i_Turns.Capacity; i++)
            {
                Console.WriteLine(sr_emptySequence);
                Console.WriteLine(k_horizonalDelimiter);
            }
        }
        
        public static void WelcomePlayer()
        {
            Console.WriteLine("Welcome to bulls and cows!٩(^‿^)۶");
            Console.WriteLine("Restrictions:");
            Console.WriteLine("1.Every guess must have exactly {0} characters",GameControl.GuessSize);
            Console.WriteLine("2.No letter may repeat itself");
            Console.WriteLine("3.At any moment, enter 'Q' to quit");
        }

        public static uint GetSyntacticallyValidGuessesCount(char i_BottomBound, char i_TopBound)
        {
            int parsedUserChoice;
            string userChoice;
            bool isValid;
            
            do
            {
                Console.WriteLine("Please enter a the requested guesses count in range {0}-{1}",i_BottomBound ,i_TopBound);
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == k_QuitMessage)
                {
                    abandonGame();
                }

                isValid = int.TryParse(userChoice,  out parsedUserChoice);

                if (!isValid)
                {
                    Console.WriteLine("This is not a number");
                }

            } while (!isValid);
            
            return (uint)parsedUserChoice;
        }

        public static char[] GetSyntacticallyValidGuess()
        {
            string userGuess;
            bool syntacticValidity;
            
            do
            {
                Console.WriteLine("Try to guess the sequence:");
                userGuess = Console.ReadLine();
                if (userGuess.ToUpper() == k_QuitMessage)
                {
                    abandonGame();
                }

                syntacticValidity = true;
                foreach (char character in userGuess)
                {
                    syntacticValidity = syntacticValidity && char.IsLetter(character);
                }
                
                if(!syntacticValidity)
                {
                    Console.WriteLine("Your guess contains none alphabetical characters");
                }
            } while (!syntacticValidity);
            
            return userGuess.ToCharArray();
        }

        public static void abandonGame()
        {
            Console.WriteLine("	＼(＾O＾)／  Thank you for playing! Bye!  ＼(＾O＾)／");
            System.Environment.Exit(0);
        }
        
        public static void InformDefeat()
        {
            Console.WriteLine("You are out of tries! maybe next time (ㅠ﹏ㅠ)");
        }
            
        public static void InformVictory()
        {
            Console.WriteLine("Congratulations! You got it! (⌒ ▽ ⌒)");
        }

        internal class TurnStringifier
        {
            const string k_Space = " ";
            private const string k_BullSign = "X";
            private const string k_CowSign = "V";
            private const int k_QunatityOfNeededCharactersForPrintingOneTurn = 50;
            
            public static string TurnToString(Turn i_Turn)
            {
                return string.Format("{0}{1}{0}{2}{0}", k_verticalDelimiter,
                    guessToString(i_Turn.Guess), guessOutcomeToString(i_Turn.Bulls, i_Turn.Cows));
            }

            private static string guessToString(char[] i_Guess)
            { 
                StringBuilder guessAsString = new StringBuilder("",k_QunatityOfNeededCharactersForPrintingOneTurn);

                for (int i = 0; i < GameControl.GuessSize; i++)
                {
                    guessAsString.Append(k_Space);
                    guessAsString.Append(i_Guess[i]);
                }
                guessAsString.Append(k_Space);

                return guessAsString.ToString();
            }

            private static string guessOutcomeToString(eHitOptions i_Bulls, eHitOptions i_Cows)
            { 
                StringBuilder GuessOutcomeAsString = new StringBuilder("",k_QunatityOfNeededCharactersForPrintingOneTurn);
                eHitOptions j = eHitOptions.NoHit;
                
                for (eHitOptions i = eHitOptions.NoHit; i < i_Cows; i++)
                {
                    GuessOutcomeAsString.Append(k_Space);
                    GuessOutcomeAsString.Append(k_CowSign);
                    j++;
                }
                
                for (eHitOptions i = eHitOptions.NoHit; i < i_Bulls; i++)                  
                {                                                          
                    GuessOutcomeAsString.Append(k_Space);                  
                    GuessOutcomeAsString.Append(k_BullSign);
                    j++;
                }
                
                for (; j < eHitOptions.FourHits; j++)   
                {                                           
                    GuessOutcomeAsString.Append(k_Space);   
                    GuessOutcomeAsString.Append(k_Space);   

                }  
                
                GuessOutcomeAsString.Append(k_Space);
                
                return GuessOutcomeAsString.ToString();
            }
        }
    }
}



