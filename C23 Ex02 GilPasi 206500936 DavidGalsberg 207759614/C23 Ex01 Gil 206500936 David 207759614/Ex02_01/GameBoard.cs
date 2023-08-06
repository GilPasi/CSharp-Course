using System.Net;
using System.Text;
namespace sapir_c23_dn_course_gil_and_david.Ex02_01
{
    public class GameBoard
    {
        private const string k_verticalDelimiter = "|";
        private const string k_horizonalDelimiter = "|=========|=========|";
        private const int k_QunatityOfNeededCharactersForPrintingOneTurn = 50;
        const string k_QuitMessage = "Q";
        private const int k_GuessSize = 4;

        private  readonly uint r_CurrentStateAsStringSize;
        private StringBuilder m_currentStateAsString;
        private List <Turn> m_historyTurns = new List<Turn>();
        private readonly uint r_maxTries;

        public GameBoard(uint i_MaxTries)
        {
            r_maxTries = i_MaxTries;
            r_CurrentStateAsStringSize = (1 + i_MaxTries) * k_QunatityOfNeededCharactersForPrintingOneTurn;
            m_currentStateAsString = new StringBuilder((int)r_CurrentStateAsStringSize);
        }

        public void AddTurn(Turn i_NewTurn)
        {
            m_historyTurns.Add(i_NewTurn);
        }

        public void PrintCurrentState()
        {
            m_currentStateAsString.Append(string.Format("Current board status:{0}", Environment.NewLine));
            m_currentStateAsString.Append(string.Format("{0}Pins:    {0}Result:  {0}{1}",k_verticalDelimiter, Environment.NewLine));
            m_currentStateAsString.Append(string.Format("{0}{1}", k_horizonalDelimiter, Environment.NewLine));
            m_currentStateAsString.Append(string.Format("|{0}|{1}" ,Turn.GetHiddenSequenceTemplate(), Environment.NewLine));
            m_currentStateAsString.Append(string.Format("{0}{1}", k_horizonalDelimiter, Environment.NewLine));

            int i = 0;
            foreach (Turn passedTurn in m_historyTurns)
            {
                m_currentStateAsString.Append(string.Format("|{0}|{1}" ,passedTurn.ToString(), Environment.NewLine));
                i++;
            }

            for (; i < r_maxTries; i++)
            {
                m_currentStateAsString.Append(string.Format("|{0}|{1}{2}{1}"
                    ,Turn.GetEmptySequenceTemplate(), Environment.NewLine, k_horizonalDelimiter));
            }

            Console.WriteLine(m_currentStateAsString.ToString());
        }

        public static void WelcomePlayer()
        {
            Console.WriteLine("Welcome to bulls and cows!٩(^‿^)۶");
            Console.WriteLine("Restrictions:");
            Console.WriteLine("1.Every guess must have exactly {0} characters",k_GuessSize);
            Console.WriteLine("2.No letter may repeat itself");
            Console.WriteLine("3.At any moment, enter 'Q' to quit");
        }

        public static uint GetValidGuessesCount()
        {
            int parsedUserChoice;
            string userChoice;
            const int k_LowerBound = 4, k_UpperBound = 8;
            bool syntacticValidity, pragmaticValidity;
            do
            {
                Console.WriteLine("Please enter a the requested guess count in range {0}-{1}", k_LowerBound, k_UpperBound);
                userChoice = Console.ReadLine();
                if (userChoice.ToUpper() == k_QuitMessage)
                {
                    abandonGame();
                }

                syntacticValidity = int.TryParse(userChoice, out parsedUserChoice);
                pragmaticValidity = parsedUserChoice <= k_UpperBound && parsedUserChoice >= k_LowerBound;
                if (!syntacticValidity)
                {
                    Console.WriteLine("This input is not an integer");
                }
                else if (!pragmaticValidity)
                {
                    Console.WriteLine("This number is not in range");
                }
            } while (!(syntacticValidity && pragmaticValidity));
            
            return (uint)parsedUserChoice;
        }

        public static char[] GetValidGuess()
        {
            string userGuess;
            bool syntacticValidity,pragmaticValidity;
            do
            {
                Console.WriteLine("Try to guess the sequence:");
                userGuess = Console.ReadLine();
                if (userGuess.ToUpper() == k_QuitMessage)
                {
                    abandonGame();
                }

                pragmaticValidity = userGuess.Length == k_GuessSize;
                syntacticValidity = true;
                List<char> takenChars = new List<char>();
                foreach (char character in userGuess)
                {
                    syntacticValidity = syntacticValidity && char.IsLetter(character);
                    pragmaticValidity = !takenChars.Contains(character);
                    if (pragmaticValidity)
                    {
                        takenChars.Add(character);
                    }
                }
                if(!syntacticValidity)
                {
                    Console.WriteLine("Your guess contains none alphabetical characters");
                }

                if (!pragmaticValidity)
                {
                    Console.WriteLine("Your guess has incorrect length or repeating characters");
                }


            } while (!(pragmaticValidity && syntacticValidity));
            
            return userGuess.ToCharArray();
        }

        public static void abandonGame()
        {
            Console.WriteLine("	＼(＾O＾)／  Thank you for playing! Bye!  ＼(＾O＾)／");
            System.Environment.Exit(0);  

        }

    }
}



