using System.Text;
using sapir_c23_dn_course_gil_and_david;
namespace sapir_c23_dn_course_gil_and_david.Ex01_05
{
    public class Program
    {
        public static void Main()
        {
            const int k_BufferSize = 50;

            Console.WriteLine("Please enter a 6 digits long number");
            string userInput = GetValidInput();

            StringBuilder numberInfo = new ("=== Number Stats ===", k_BufferSize);
            numberInfo.Append(Environment.NewLine);
            numberInfo.Append(string.Format(
                "There are {0} digits that are greater than the ones digit", CountDigitsThatAreGreaterThenTheOnesDigit(userInput)));
            numberInfo.Append(Environment.NewLine);
            numberInfo.Append(string.Format("Minimum digit is {0}", FindMinimumDigit(userInput)));
            numberInfo.Append(Environment.NewLine);
            numberInfo.Append(string.Format("There are {0} digits that are divisible by 3", CountMultiplesOf3(userInput)));
            numberInfo.Append(Environment.NewLine);
            numberInfo.Append(string.Format("The digits average is {0}", CalculateDigitsAverage(userInput)));
            numberInfo.Append(Environment.NewLine);
            Console.WriteLine(numberInfo.ToString());
        }

        public static string GetValidInput()
        {
            bool valid;
            string input;

            do
            {
                input = Console.ReadLine();
                valid = int.TryParse(input, out int parsedInput) && input.Length == 6;

                if(!valid)
                {
                    Console.WriteLine("Invalid input. Please enter an integer with 6 digits.");
                }
            }
            while (!valid);

            return input;
        }

        public static int CountDigitsThatAreGreaterThenTheOnesDigit(string i_NumberAsString)
        {
            int counter = 0;
            char onesDigit = i_NumberAsString[i_NumberAsString.Length - 1];

            for( int i = 1; i < i_NumberAsString.Length; i++)
            {
                if( i_NumberAsString[i] > onesDigit)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static char FindMinimumDigit(string i_NumberAsString)
        {
            char minChar = '9';

            for(int i = 0; i < i_NumberAsString.Length; i++)
            {
                minChar = minChar < i_NumberAsString[i] ? minChar : i_NumberAsString[i];
            }
            return minChar;
        }

        public static int CountMultiplesOf3(string i_NumberAsString)
        {
            int counter = 0;

            for(int i = 0; i < i_NumberAsString.Length; i++)
            {
                int parsedDigit = int.Parse(i_NumberAsString[i].ToString());
                if (parsedDigit % 3 == 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        public static float CalculateDigitsAverage(string i_NumberAsString)
        {
            float average = 0;

            for (int i = 0; i < i_NumberAsString.Length; i++)
            {
                int ParsedDigit = int.Parse(i_NumberAsString[i].ToString());
                average += ParsedDigit;
            }

            average /= i_NumberAsString.Length;

            return average;
        }
    }
}