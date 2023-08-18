namespace Ex02
{
    public class Turn
    {
        private readonly char[] r_guess;
        private eHitOptions r_bulls;
        private eHitOptions r_cows;
        
        public Turn(char[] i_Guess, char[] i_CorrectSequence = null)
        {
            r_guess = i_Guess;
            r_bulls = 0;
            r_cows = 0;
            
            if (i_CorrectSequence != null)
            {
                inferBullsAndCows(i_CorrectSequence);
            }
            //Otherwise nothing was sent and there is no point in inferring bulls and cows.
            //When a turn has no other turn to be compared to, this means that the former 
            //Is actually the correct sequence which was generated by the GameControl class.
            //This mechanism prevents code duplications.
        }
        
        public Turn(string i_Guess, string i_CorrectSequence)
            :this(i_Guess.ToCharArray(), i_CorrectSequence.ToCharArray()){ }                          
        
        private void inferBullsAndCows(char[] i_CorrectSequence)
        {
            for (int i = 0; i < GameControl.GuessSize; i++)
            {
                for (int j = 0; j < GameControl.GuessSize; j++)
                {
                    grantPointsForCurrentPinGuess(i, j, i_CorrectSequence);
                }
            }
        }
        
        public char[] Guess
        {
            get
            {
                char[] guessCopy = new char[GameControl.GuessSize];
                Array.Copy(r_guess, guessCopy, GameControl.GuessSize);
                return guessCopy;
            }
        }

        public eHitOptions Cows
        {
            get
            {
                return r_cows;
            }
        }
        
        public eHitOptions Bulls
        {
            get
            {
                return r_bulls;
            }
        }
        
        private void grantPointsForCurrentPinGuess(int i_GuessPin, int i_CorrectSequencePin, char[] i_CorrectSequence)
        {
            if (r_guess[i_GuessPin] == i_CorrectSequence[i_CorrectSequencePin])
            {
                if (i_CorrectSequencePin == i_GuessPin)
                {
                    r_bulls++;
                }
                else
                {
                    r_cows++;
                }
            }
        }
        
        public bool containsValidGuess()
        {
            return isValidSequence(r_guess);
        }
        private static bool isValidSequence(char[] i_Guess)
        {
            bool result = i_Guess.Length == GameControl.GuessSize;
            List<char> takenLetters = new List<char>();
            
            foreach (char character in i_Guess)
            {
                result = result && char.IsLetter(character) && !takenLetters.Contains(character);
                takenLetters.Add(character);
            }
            
            return result;
        }
    }    
}
