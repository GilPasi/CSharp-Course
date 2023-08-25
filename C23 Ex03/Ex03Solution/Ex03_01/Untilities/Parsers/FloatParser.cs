namespace Ex03
{
    public class FloatParser : IParser
    {
        public bool TryParse(string i_Input, out object o_ParsedInput)
        {
            float inputAsFloat;
            bool successfulParse = float.TryParse(i_Input, out inputAsFloat);
            
            o_ParsedInput = inputAsFloat;
            return successfulParse;
        }

        public bool IsParseable(string i_Input)
        {
            return float.TryParse(i_Input, out float placeholder);
        }
    }
}
