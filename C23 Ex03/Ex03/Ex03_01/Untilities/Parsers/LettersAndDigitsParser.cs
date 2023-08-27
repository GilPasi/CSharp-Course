namespace Ex03
{
    public class LettersAndDigitsParser : IParser
    {
        public bool TryParse(string i_Input, out object i_ParsedInput)
        {
            bool successfulParse = IsParseable(i_Input);
            
            if (successfulParse)
            {
                i_ParsedInput = i_Input;
            }
            else
            {
                i_ParsedInput = null;
            }

            return successfulParse;
        }

        public bool IsParseable(string i_Input)
        {
            bool result = true;
            foreach (char character in i_Input)
            {
                result = result && (char.IsDigit(character) || char.IsLetter(character));
            }

            return result;
        }
    }
}

