using System.Text;
namespace sapir_c23_dn_course_gil_and_david.Ex01_04
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Please enter a word or a number.It must be 8 characters long. ");
            string userInput = GetValidInput();
            const int k_BufferSize = 50;

            StringBuilder stringInfo = new StringBuilder("=== string analysis ===", k_BufferSize);
            stringInfo.Append(Environment.NewLine);
            stringInfo.Append(string.Format("This string is a palindrome: {0}", FindIfPalindrome(userInput)));
            stringInfo.Append(Environment.NewLine);

            if (int.TryParse(userInput, out int ParsedInput))
            {
                stringInfo.Append(string.Format("This number is divisible by 4: {0} ", CheckIfDivisibleBy4(ParsedInput)));
            }
            else
            {
                stringInfo.Append(string.Format("This word has {0} capital letters ", userInput.Count(char.IsUpper)));
            }

            Console.WriteLine(stringInfo.ToString());

        }

        public static bool CheckIfDivisibleBy4(int i_Number)
        {
            return i_Number % 4 == 0;
        }

        public static bool FindIfPalindrome(string i_String)
        {
            bool result = i_String.Length < 1;
            int lastChar = i_String.Length - 1;
            if (!result)
            {
                result = (i_String[0] == i_String[lastChar]) && FindIfPalindrome(i_String[1..lastChar]);

            }

            return result;
        }

        public static string GetValidInput()
        {
            bool valid;
            string input;

            do
            {
                input = Console.ReadLine();
                valid = CheckIfValidString(input);
                if (!valid)
                {
                    Console.WriteLine("Invalid input.The string must be letter only or digits only.{0}Also it must be 8 characters long.",
                        Environment.NewLine);
                }
            }
            while (!valid);

            return input;
        }

        public static bool CheckIfValidString(string i_str)
        {
            const int k_RequestedLength = 8;
            bool containsLetter = false;
            bool containsDigit = false;
            bool containsInvalidChars = false;

            for (int i = 0; i < i_str.Length; i++)
            {
                char currentChar = i_str[i];
                containsLetter = containsLetter || char.IsLetter(currentChar);
                containsDigit = containsDigit || char.IsDigit(currentChar);
                containsInvalidChars = containsInvalidChars || IsSpecialCharacter(currentChar);
            }

            bool result = containsDigit ^ containsLetter;
            result = result && !containsInvalidChars;
            result = result && i_str.Length == k_RequestedLength;

            return result;
        }

        public static bool IsSpecialCharacter(char i_Char) {

            return !char.IsDigit(i_Char) && !char.IsLetter(i_Char);
        }
    }
}