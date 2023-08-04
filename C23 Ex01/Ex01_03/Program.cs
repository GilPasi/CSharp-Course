using sapir_c23_dn_course_gil_and_david.Ex01_02;
namespace sapir_c23_dn_course_gil_and_david.Ex01_03
{
    public class Program
    {
        public static void Main()
        {
            Console.Write("Enter the height of the hourglass: ");
            int validHeight = GetValidInput();

            HourglassPrinter.PrintHourglass(validHeight);
        }

        public static int GetValidInput()
        {
            bool valid;
            string input;
            int parsedInput;

            do
            {
                input = Console.ReadLine();
                valid = int.TryParse(input, out parsedInput) && parsedInput > 0;

                if (!valid)
                {
                    Console.WriteLine("Invalid input. Please enter a valid positive integer.");
                }
            }
            while (!valid);

            return parsedInput;
        }
    }

}