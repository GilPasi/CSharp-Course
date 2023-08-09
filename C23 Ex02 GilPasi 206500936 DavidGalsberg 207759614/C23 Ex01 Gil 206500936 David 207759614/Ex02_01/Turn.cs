using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace sapir_c23_dn_course_gil_and_david.Ex02_01
{
    public class Turn
    {
        private const int k_GuessSize = 4;
        private static Turn sr_HiddenSequenceTemplate;
        private static Turn sr_EmptySequenceTemplate;
        private static char[] s_correctSequence = new char[k_GuessSize];
        private char[] m_guess;
        private eHitOptions m_bulls;
        private eHitOptions m_cows;

        static Turn()
        {
            char[] hiddenSequenceValues = new char[k_GuessSize] { '#', '#', '#', '#' };
            char[] emptySequenceValues = new char[k_GuessSize] { ' ', ' ', ' ', ' ' };
            sr_HiddenSequenceTemplate = new Turn(hiddenSequenceValues);
            sr_EmptySequenceTemplate = new Turn(emptySequenceValues);

        }

        public static char[] CorrectSequence
        {
             get
            {
                char[] correctSequenceCopy = new char[k_GuessSize];
                Array.Copy(s_correctSequence, correctSequenceCopy, k_GuessSize);
                return correctSequenceCopy;
            }
            set
            {
                if (isValidSequence(value))
                {
                    Array.Copy(value, s_correctSequence,k_GuessSize);
                }
            }
        }
        
        public char[] Guess
        {
            get
            {
                char[] guessCopy = new char[k_GuessSize];
                Array.Copy(m_guess, guessCopy, k_GuessSize);
                return guessCopy;
            }
        }

        public eHitOptions Cows
        {
            get
            {
                return m_cows;
            }
        }
        public eHitOptions Bulls
        {
            get
            {
                return m_bulls;
            }
        }

        public Turn(char[] i_Guess)
        {
            m_guess = i_Guess;
            m_bulls = 0;
            m_cows = 0;
            inferBullsAndCows();
        }
        
        public Turn(string i_Guess):this(i_Guess.ToCharArray()){ }                          
        
        private void inferBullsAndCows()
        {
            for (int i = 0; i < k_GuessSize; i++)
            {
                for (int j = 0; j < k_GuessSize; j++)
                {
                    grantPointsForCurrentPinGuess(i, j);
                }
            }
        }
        
        private void grantPointsForCurrentPinGuess(int i_GuessPin, int i_CorrectSequencePin)
        {
            if (m_guess[i_GuessPin] == s_correctSequence[i_CorrectSequencePin])
            {
                if (i_CorrectSequencePin == i_GuessPin)
                {
                    m_bulls++;
                }
                else
                {
                    m_cows++;
                }
            }
        }
        
        public bool containsValidGuess()
        {
            return isValidSequence(m_guess);
        }
        private static bool isValidSequence(char[] i_Guess)
        {
            bool result = i_Guess.Length == k_GuessSize;
            List<char> takenLetters = new List<char>();
            foreach (char character in i_Guess)
            {
                result = result && char.IsLetter(character) && !takenLetters.Contains(character);
                takenLetters.Add(character);
            }
            
            return result;
        }
        
        public static string GetHiddenSequenceTemplate()
        {
            return sr_HiddenSequenceTemplate.ToString();
        }        
        
        public static string GetEmptySequenceTemplate()
        {
            return sr_EmptySequenceTemplate.ToString();
        }                                             

    }    
}

