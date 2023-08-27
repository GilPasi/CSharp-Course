namespace Ex03
{
    public class AlphaBeitParser : IParser
    {
        public bool TryParse(string i_Input, out object o_InputAsAlphabeitical)
        {
            bool successfulParse = IsParseable(i_Input);
            if (successfulParse)
            {
                o_InputAsAlphabeitical = i_Input;
            }
            else
            {
                o_InputAsAlphabeitical = null;
            }

            return successfulParse;
        }

        public bool IsParseable(string i_Input)
        {
            bool isParseable = true;
            foreach(char character in i_Input)
            {
                isParseable = isParseable && char.IsLetter(character);
            }

            return isParseable;
        }
    }
}

