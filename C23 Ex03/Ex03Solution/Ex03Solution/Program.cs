namespace Ex03
{
    class Program {         
        static void Main(string[] args)
        {
            throw new ValueOutOfRangeException(new Exception(),45, 30);
        }
    }
}