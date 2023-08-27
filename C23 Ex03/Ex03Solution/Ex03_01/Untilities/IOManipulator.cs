using System.Text.RegularExpressions;
namespace Ex03
{
    public static class IOManipulator
    {
        public static bool ContainsSpecialCharacters(string i_String)
        {
            bool result = false;
            foreach (char character in i_String)
            {
                result = result || (!char.IsDigit(character) && !char.IsLetter(character));
            }

            return result;
        }
        
        public static T ForceParseableInput<T>(string i_WarningText, IParser i_Parser)
        {
            bool isValidInput = false;
            T parsedInput = default(T);
            while (!isValidInput)
            {
                try
                {
                    parsedInput = ParseInput<T>(i_WarningText, i_Parser);
                    isValidInput = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return parsedInput;
        }
        
        public static T ParseInput<T>(string i_WarningText, IParser i_Parser)
        {
            object inputAsT;
            string userInput;
            
            userInput = Console.ReadLine();
            if (!i_Parser.TryParse(userInput, out inputAsT))
            {
                throw new FormatException(i_WarningText);
            }

            return (T)inputAsT;
        }
        
        public static string PascalCaseToHumanReadable(string i_PascalCaseText)
        {
            return Regex.Replace(i_PascalCaseText, "([a-z])([A-Z])", "$1 $2");
        }
    }
}