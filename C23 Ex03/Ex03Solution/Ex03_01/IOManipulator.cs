using System.Text.RegularExpressions;
namespace Ex03
{
    public static class IOManipulator
    {
        public static bool IsItemInRange<T>(T i_ExaminedValue, T i_BottomBound, T i_TopBound) where T : IComparable<T>
        {
            return i_ExaminedValue.CompareTo(i_BottomBound) >= 0 && i_ExaminedValue.CompareTo(i_TopBound) <= 0  ;
        }
        

        public static bool IsAlphabeiticalString(string i_String)
        {
            bool result = true;
            foreach (char character in i_String)
            {
                result = result && char.IsLetter(character);
            }

            return result;
        }

        public static bool IsNumericalString(string i_String)
        {
            bool result = true;
            foreach (char character in i_String)
            {
                result = result && char.IsDigit(character);
            }

            return result;
        }

        public static bool ContainsSpecialCharacters(string i_String)
        {
            bool result = false;
            foreach (char character in i_String)
            {
                result = result || (!char.IsDigit(character) && !char.IsLetter(character));
            }

            return result;
        }

        public static string ForceAlphabeiticalInput(string i_WarningText)
        {
            string alphabeticalString = String.Empty;
            string userInput;
            bool isAlphabeitical = true;
       
            loop: userInput = Console.ReadLine();
            isAlphabeitical = IsAlphabeiticalString(userInput);

            if (!isAlphabeitical)
            {
                Console.WriteLine(i_WarningText);
                goto loop;
            }
            
            return alphabeticalString;
        }
        
        public static string ForceNumericalInput(string i_WarningText)
        {
            string alphabeticalString = String.Empty;
            string userInput;
            bool isAlphabeitical = true;
       
            loop: userInput = Console.ReadLine();
            isAlphabeitical = IsNumericalString(userInput);

            if (!isAlphabeitical)
            {
                Console.WriteLine(i_WarningText);
                goto loop;
            }
            
            return alphabeticalString;
        }
        
        public static string ConvertToUniformFormat(string i_String)
        {
            return i_String.Replace(" ", string.Empty)
                .Replace(".", string.Empty)
                .ToUpper();
        }
        
        public static string PascalCaseToHumanReadable(string i_PascalCaseText)
        {
            return Regex.Replace(i_PascalCaseText, "([a-z])([A-Z])", "$1 $2");
        }
    }
}