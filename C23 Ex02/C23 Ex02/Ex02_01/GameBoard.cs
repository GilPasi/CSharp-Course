using System.Collections.Generic;
using System;
using System.Text;
namespace Ex02
{
    public class GameBoard
    {
        private const string k_verticalDelimiter = "|";
        private const string k_horizonalDelimiter = "|=========|=========|";
        private const string k_QuitMessage = "Q";
        private const string k_AgreementMessage = "Y";
        private static readonly string sr_emptySequence;
        private static readonly string sr_hiddenSequence;

        static GameBoard()
        {
            sr_hiddenSequence = string.Format("{0} # # # # {0}         {0}",
                k_verticalDelimiter);
            sr_emptySequence = string.Format("{0}         {0}         {0}",
                k_verticalDelimiter);
        }

        public static void PrintState(List<Turn> i_Turns, string i_CorrectSequence = null)
        {
            int i;

            Console.WriteLine("Current board status:");
            Console.WriteLine("{0}Pins:    {0}Result:  {0}", k_verticalDelimiter);
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

        public static uint GetSyntacticallyValidGuessesCount(int i_BottomBound,
            int i_TopBound, out bool o_UserWantToQuit)
        {
            int parsedUserChoice = 0;
            string userChoice;
            bool isValid;

            do
            {
                Console.WriteLine("Please enter a the requested guesses count in range {0}-{1}", i_BottomBound, i_TopBound);
                userChoice = Console.ReadLine();
                o_UserWantToQuit = userChoice.ToUpper() == k_QuitMessage;
                isValid = int.TryParse(userChoice, out parsedUserChoice);
                if (!isValid && !o_UserWantToQuit)
                {
                    Console.WriteLine("This is not a number");
                }
            }

            while (!isValid && !o_UserWantToQuit);

            return (uint)parsedUserChoice;
        }

        public static char[] GetSyntacticallyValidGuess(out bool o_UserWantQuit)
        {
            string userGuess;
            bool syntacticValidity;

            do
            {
                Console.WriteLine("Try to guess the sequence:");
                userGuess = Console.ReadLine();
                userGuess = formatStringAsCapitalsWithNoSpaces(userGuess);
                o_UserWantQuit = userGuess == k_QuitMessage;
                syntacticValidity = true;
                foreach (char character in userGuess)
                {
                    syntacticValidity = syntacticValidity && isLetterOneOfTheFirst8LettersInEnglish(character);
                }

                if (!syntacticValidity && !o_UserWantQuit)
                {
                    Console.WriteLine("Your guess contains characters that are not a-h letters");
                }
            }

            while (!syntacticValidity && !o_UserWantQuit);

            return userGuess.ToCharArray();
        }

        private static bool isLetterOneOfTheFirst8LettersInEnglish(char i_Letter)
        {
            return i_Letter >= 'A' && i_Letter <= 'H';
        }

        private static string formatStringAsCapitalsWithNoSpaces(string i_String)
        {
            return i_String.Replace(" ", string.Empty).ToUpper();
        }

        public static void WelcomePlayer()
        {
            Console.WriteLine("Welcome to bulls and cows!");
            Console.WriteLine(@"
                ((...))         ((...))         ((...))           L...L  
                ( o o )         ( x x )         ( O O )          < o o >
                 \   /           \   /           \   /            \   /
                  ^_^             ^_^            (`_`)             ^_^  ");
            Console.WriteLine("Restrictions:");
            Console.WriteLine("1.Every guess must have exactly {0} characters", GameControl.GuessSize);
            Console.WriteLine("2.No letter may repeat itself");
            Console.WriteLine("3.At any moment, enter 'Q' to quit");
        }

        public static void InformUserAboutDefeat()
        {
            Console.WriteLine("No more guesses allowed. You lost.");
        }

        public static void InformUserAboutVictory(int i_Steps)
        {
            Console.WriteLine("You guessed after {0} steps!", i_Steps);
        }

        public static void InformUserAboutQuit()
        {
            Console.WriteLine("Thank you for playing! Bye!");
        }

        public static bool AskForAnotherRun()
        {
            Console.WriteLine("Would you like to start a new game? <Y/N>");

            return Console.ReadLine().ToUpper() == k_AgreementMessage;
        }

        public static void GeneralMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public static void CleanScreen() 
        {
            try
            {
                ConsoleUtils.Screen.Clear();
            }
            catch (Exception e){}
        }

        internal class TurnStringifier
        {
            const string k_Space = " ";
            private const string k_BullSign = "V";
            private const string k_CowSign = "X";
            private const int k_QunatityOfNeededCharactersForPrintingOneTurn = 50;

            public static string TurnToString(Turn i_Turn)
            {
                return string.Format("{0}{1}{0}{2}{0}", k_verticalDelimiter,
                    guessToString(i_Turn.Guess), guessOutcomeToString(i_Turn.Bulls, i_Turn.Cows));
            }

            private static string guessToString(char[] i_Guess)
            {
                StringBuilder guessAsString = new StringBuilder("", k_QunatityOfNeededCharactersForPrintingOneTurn);

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
                StringBuilder GuessOutcomeAsString = new StringBuilder("", k_QunatityOfNeededCharactersForPrintingOneTurn);
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