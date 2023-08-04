using System.Text;
namespace sapir_c23_dn_course_gil_and_david.Ex01_01
{
    class Program
    {
        static void Main()
        {
            const int k_BinaryNumbersSize = 7;
            const int k_UsersInputsCount = 3;
            float averageOnesCount = 0;
            float avergeZeroesCount = 0;
            int quantityOfPowersOf2 = 0;
            int qunatityOfNumbersWithAscendingDecimalRepreasantation = 0;
            int maxNumber = int.MinValue;
            int minNumber = int.MaxValue;
            const int BufferMaxSize = 50;

            for (int i = 0; i < k_UsersInputsCount; i++)
            {
                string ValidInput = GetValidNumber(k_BinaryNumbersSize);
                int InputAsDecimal = BinaryToDecimal(ValidInput);
                Console.WriteLine(string.Format("Your number as a decimal is: {0}",InputAsDecimal));
                averageOnesCount += CountOnes(ValidInput);
                avergeZeroesCount += CountZeroes(ValidInput);
                if (CheckIfIsAPowerOf2(ValidInput))
                {
                    quantityOfPowersOf2++;
                }

                if(CheckIfIsAscendingSequence(InputAsDecimal))
                {
                    qunatityOfNumbersWithAscendingDecimalRepreasantation++;
                }
                maxNumber = Max(maxNumber, InputAsDecimal);
                minNumber = Min(minNumber, InputAsDecimal);

            }

            averageOnesCount /= k_UsersInputsCount;
            avergeZeroesCount /= k_UsersInputsCount;
            StringBuilder Stats = new StringBuilder("=== Stats ===", BufferMaxSize);
            Stats.Append(Environment.NewLine);
            Stats.Append(string.Format("Zeroes / Ones Average : {0} / {1}", avergeZeroesCount, averageOnesCount));
            Stats.Append(Environment.NewLine);
            Stats.Append(string.Format("There are {0} numbers which represant a power of 2", quantityOfPowersOf2));
            Stats.Append(Environment.NewLine);
            Stats.Append(string.Format("There are {0} numbers with ascending decimal repreanstation ",
                qunatityOfNumbersWithAscendingDecimalRepreasantation));
            Stats.Append(Environment.NewLine);
            Stats.Append(string.Format("The max number is {0} and the min number is {1}", maxNumber, minNumber));


            Console.Write(Stats.ToString());
        }


        public static string GetValidNumber(int i_RequestedLength)
        {
            bool validInput;
            string userInput;

            do
            {
                Console.WriteLine(string.Format("Please enter a {0} digits binary number", i_RequestedLength));
                userInput = Console.ReadLine();
                validInput = CheckIfHasValidSize(userInput, i_RequestedLength) && CheckIfBinary(userInput);
                if (!validInput)
                {
                    Console.WriteLine("Invalid input");
                }
            }
            while (!validInput);

            return userInput;
        }

        public static bool CheckIfHasValidSize(string i_NumberAsString, int i_RequestedLength)
        {
            return i_NumberAsString.Length == i_RequestedLength;
        }

        public static bool CheckIfBinary(string i_NumberAsString)
        {
            bool result = true;
            for (int i = 0; i < i_NumberAsString.Length && result; i++)
            {
                result = i_NumberAsString[i] == '0' || i_NumberAsString[i] == '1';
            }

            return result;
        }

        public static int BinaryToDecimal(string i_BinaryNumberAsString)
        {

            int numberAsDecimal = 0;
            int radix = 1;

            for (int i = i_BinaryNumberAsString.Length - 1; i >= 0; i--)
            {

                if (i_BinaryNumberAsString[i] == '1')
                {
                    numberAsDecimal += radix;
                }

                radix *= 2;
            }

            return numberAsDecimal;
        }

        public static int CountZeroes(string i_BinaryString)
        {
            return i_BinaryString.Length - CountOnes(i_BinaryString);
            //In a binary format any character that is not 1 is necessarily 0.
        }

        public static int CountOnes(string i_BinaryString)
        {
            int counter = 0;

            for (int i = 0; i < i_BinaryString.Length; i++)
            {
                if (i_BinaryString[i] == '1')
                {
                    counter++;
                }
            }

            return counter;
        }

        public static bool CheckIfIsAPowerOf2(string i_BinaryNumber)
        {
            return CountOnes(i_BinaryNumber) == 1;
            //A power of 2 has only one digit of 1 due to the binary format structure.
        }

        public static bool CheckIfIsAscendingSequence(int i_Number)
        {
            bool result = true;
            string numberAsString = i_Number.ToString();

            for (int i = 1; i < numberAsString.Length && result; i++)
            {
                result = numberAsString[i] > numberAsString[i - 1];
            }

            return result;
        }

        public static int Max(int i_CurrentMax, int i_ExaminedValue)
        {
            return Math.Max(i_CurrentMax,i_ExaminedValue);
        }

        public static int Min(int i_CurrentMin, int i_ExaminedValue)
        {
            return Math.Min(i_CurrentMin, i_ExaminedValue);
        }
    }
}