namespace Ex03
{
    public class UFloatParser : IParser
    {
        public bool TryParse(string i_Input, out object o_ParsedInput)
        {
            bool isParseable = IsParseable(i_Input);
            if (isParseable)
            {
                o_ParsedInput = float.Parse(i_Input);
            }
            else
            {
                o_ParsedInput = default(float);
            }

            return isParseable;
        }

        public bool IsParseable(string i_Input)
        {
            float floatParsedValue;
            bool isParseableToFloat = float.TryParse(i_Input, out floatParsedValue);
            return isParseableToFloat && floatParsedValue >= 0;
        }
    }
}

