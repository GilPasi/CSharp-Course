namespace sapir_c23_dn_course_gil_and_david.Ex01_02
{
    public class HourglassPrinter
    {
        public static void Main()
        {
            const int k_RequiredHeight = 5;

            PrintHourglass(k_RequiredHeight);

        }

        public static void PrintHourglass(int i_RequestedHeight)
        {
            int inferedHeight = i_RequestedHeight + ((i_RequestedHeight + 1) % 2);

            PrintHourglass(inferedHeight, inferedHeight);
        }

        public static void PrintHourglass(int i_RowSize, int i_MaxRow)
        {
            if (i_RowSize > 2)
            {
                //Upper triangle start
                PrintSpaces((i_MaxRow - i_RowSize) / 2);
                PrintStars(i_RowSize);
                Console.WriteLine("");
                //Upper triangle end
                PrintHourglass(i_RowSize - 2, i_MaxRow);
                //Lower triangle start
                PrintSpaces((i_MaxRow - i_RowSize) / 2);
                PrintStars(i_RowSize);
                Console.WriteLine("");
                //Lower triangle end
            }
            else
            {
                PrintSpaces((i_MaxRow - i_RowSize) / 2);
                PrintStars(i_RowSize);
                Console.WriteLine("");
            }

        }

        public static void PrintSpaces(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write(" ");
            }
        }

        public static void PrintStars(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.Write("*");
            }
        }
    }
}

