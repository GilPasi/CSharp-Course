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

        public string ToString()
        {
            return TurnStringifier.ToString(m_guess, m_bulls, m_cows);
        }
        
        public static string GetHiddenSequenceTemplate()
        {
            return sr_HiddenSequenceTemplate.ToString();
        }        
        
        public static string GetEmptySequenceTemplate()
        {
            return sr_EmptySequenceTemplate.ToString();
        }                                             

        private class TurnStringifier
        {
            private const int k_ToStringBufferSize = 50;
            const string k_Space = " ";
            private const string k_BullSign = "X";
            private const string k_CowSign = "V";
            
            public static string ToString(char[] i_Guess, eHitOptions i_Bulls, eHitOptions i_Cows)
            {
                return string.Format("{0}|{1}", guessToString(i_Guess), guessOutcomeToString(i_Bulls, i_Cows));
            }

            private static string guessToString(char[] i_Guess)
            { 
                StringBuilder guessAsString = new StringBuilder("",k_ToStringBufferSize);

                for (int i = 0; i < k_GuessSize; i++)
                {
                    guessAsString.Append(k_Space);
                    guessAsString.Append(i_Guess[i]);
                }
                guessAsString.Append(k_Space);

                return guessAsString.ToString();
            }

            private static string guessOutcomeToString(eHitOptions i_Bulls, eHitOptions i_Cows)
            { 
                const string k_Space = " ";
                StringBuilder GuessOutcomeAsString = new StringBuilder("",k_ToStringBufferSize);

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

