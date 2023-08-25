namespace Ex03
{
    public interface IParser
    {
        public abstract bool TryParse(string i_Input, out object i_ParsedInput);
        public abstract bool IsParseable(string i_Input);
    }

    // public interface IParser<T> : IParser
    // {
    //     public abstract bool TryParse(string i_Input, out T i_ParsedInput);
    //     public abstract bool IsParseable(string i_Input);
    //     
    // }
}

