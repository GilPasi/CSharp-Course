namespace Ex03
{
    public class DigitsParser : IParser
    {
        public bool TryParse(string i_Input, out object o_ParsedInpt)
        {
            int inputAsInt;
            bool successfulParse = int.TryParse(i_Input, out inputAsInt);
            
            o_ParsedInpt = inputAsInt.ToString();
            return successfulParse;
        }
        
        public bool IsParseable(string i_Input)
        {
            return int.TryParse(i_Input, out int placeholder);
        }
    }
}