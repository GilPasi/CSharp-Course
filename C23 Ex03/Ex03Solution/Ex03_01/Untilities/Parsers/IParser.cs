namespace Ex03
{
    public interface IParser
    {
        public abstract bool TryParse(string i_Input, out object o_ParsedInput);
        public abstract bool IsParseable(string i_Input);
        
        
    }
}

