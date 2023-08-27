namespace Ex03
{
    public class BooleanParser : IParser
    {
        public bool TryParse(string i_Input, out object o_ParsedInput)
        {
            bool successfulyParsed = false;
            o_ParsedInput = default(bool);
            i_Input = i_Input.ToLower();
            
            if (i_Input == "yes" || i_Input == "y" || i_Input == "true" || i_Input == "t" || i_Input == "1")
            {
                o_ParsedInput = true;
                successfulyParsed = true;
            }
            else if (i_Input == "no" || i_Input == "n" || i_Input == "false" || i_Input == "f" || i_Input == "2")
            {
                o_ParsedInput = false;
                successfulyParsed = true;
            }

            return successfulyParsed;
        }

        public bool IsParseable(string i_Input)
        {
            return (i_Input == "yes" || i_Input == "y" || i_Input == "true" || i_Input == "t" || i_Input == "1"
            || i_Input == "no" || i_Input == "n" || i_Input == "false" || i_Input == "f" || i_Input == "2");
        }
    }
}

