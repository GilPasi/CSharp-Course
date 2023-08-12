using System.Dynamic;

namespace Ex02_01
{
    public class GameControl
    {
        private readonly char[] r_correctSequence;
        private List<Turn> m_turnsHistory;
        private const int k_GuessSize = 4;
        private const int k_MinimumGuessesCount = 4,k_MaximumGuessesCount = 10;
        
        public static int MinimumGuessesCount 
        {
            get
            {
                return k_MinimumGuessesCount;
            }
        }
        public static int MaximumGuessesCount 
        {
            get
            {
                return k_MaximumGuessesCount;
            }
        }

        public GameControl(uint i_MaxGuessesCount)
        {
            m_turnsHistory = new List<Turn>((int)i_MaxGuessesCount);
            r_correctSequence = generateSequence();
        }
        
        public static int GuessSize
        {
            get
            {
                return k_GuessSize;
            }
        }
        
        public List<Turn> TurnsHistory
        {
            get
            {
                return m_turnsHistory;
            }
        }
        
        public char[] CorrectSequence
        {
            get
            {
                return r_correctSequence;
            }
        }

        public static char[] generateSequence()
        {
            char[] randomSequence = new char[k_GuessSize];
            Random random = new Random();
            List<int> untakenLetters = createListInRange('A', 'H');
            
            for (int i = 0; i < k_GuessSize; i++)
            {
                int letterPosition =  random.Next(0, untakenLetters.Count);
                Console.WriteLine("letterPosition:{0} " ,letterPosition);
                Console.WriteLine("Count:{0} " ,untakenLetters.Count());

                randomSequence[i] = (char)untakenLetters[letterPosition];
                untakenLetters.RemoveAt(letterPosition);
            }

            return randomSequence;
        }

        private static List<int> createListInRange (int i_Start, int i_End)
        {
            int delta = i_End - i_Start;
            List<int> list = new List<int>(delta);
            Console.WriteLine(list.Count);
            for (int i = 0; i < delta; i++)
            {
                list.Add(i + i_Start);
            }

            return list;
        }

        public static bool CheckIfPragmaticallyValidSequence(char[] i_Sequence)
        {
            bool isValid = i_Sequence.Length == k_GuessSize;
            
            for (int i = 0; i < i_Sequence.Length; i++)
            {
                for (int j = i + 1; j < i_Sequence.Length; j++)
                {
                    isValid = isValid && i_Sequence[i] != i_Sequence[j];
                }
            }

            return isValid;
        }

        public static bool CheckIfPragmaticallyValidGuessesCount(uint i_CountOfGuesses)
        {
            return i_CountOfGuesses >= k_MinimumGuessesCount && i_CountOfGuesses <= k_MaximumGuessesCount;
        }

        public void AddTurn(char[] i_Guess)
        {
            m_turnsHistory.Add(new Turn(i_Guess, r_correctSequence));
        }
        
        private Turn getLastTurn()
        {
            int lastTurnIndex = TurnsHistory.Count - 1;
            
            return TurnsHistory[lastTurnIndex];
        }

        public eGameStatus EvaluateGameStatus()
        {
            eGameStatus currentStatus = eGameStatus.Ongoing;
            
            if (TurnsHistory.Count == TurnsHistory.Capacity)
            {
                currentStatus = eGameStatus.Defeat;
            }
            else if (getLastTurn().Bulls == eHitOptions.FourHits)
            {
                currentStatus = eGameStatus.Victory;
            }
            
            return currentStatus;
        }
    }
}