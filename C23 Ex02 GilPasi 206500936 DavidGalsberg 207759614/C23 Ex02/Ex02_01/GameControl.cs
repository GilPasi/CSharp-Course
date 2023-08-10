namespace Ex02_01
{
    public class GameControl
    {
        private readonly char[] r_correctSequence;
        private List<Turn> m_turnsHistory;
        private const int k_GuessSize = 4;

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
        
        public GameControl(uint i_MaxGuessesCount)
        {
            m_turnsHistory = new List<Turn>((int)i_MaxGuessesCount);
            r_correctSequence = generateSequence();
        }

        public static char[] generateSequence()
        {
            char[] randomSequence = new char[k_GuessSize];
            Random random = new Random();

            for (int i = 0; i < k_GuessSize; i++)
            {
                randomSequence[i] = (char)random.Next('a', 'z');
            }

            if (!CheckIfPragmaticallyValidSequence(randomSequence))
            {
                //If the generation was unsuccessful try again
                randomSequence = generateSequence();
            }

            return randomSequence;
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
            const int k_BottomBound = 4, k_TopBound = 8;
            
            return i_CountOfGuesses >= k_BottomBound && i_CountOfGuesses <= k_TopBound;
        }

        public void AddTurn(char[] i_Guess)
        {
            m_turnsHistory.Add(new Turn(i_Guess, r_correctSequence));
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

        private Turn getLastTurn()
        {
            int lastTurnIndex = TurnsHistory.Count - 1;
            
            return TurnsHistory[lastTurnIndex];
        }
    }
}