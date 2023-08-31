using System;
using Ex04.Menus.Interfaces;

namespace Ex04.Menus
{
    public class Program
    {
        public static void Main()
        {
            MenuSystem interfacesMenu = new MenuSystem();
            interfacesMenu.constructInterfacesMenu();
            interfacesMenu.Initiate();
        }
        
        public static void FullfillRequest(string i_Task)
        {
            switch (i_Task)
            {
                case "show date":
                    ShowDate();
                    break;
                case "show time":
                    ShowTime();
                    break;
                case "show version":
                    ShowVersion();
                    break;
                case "count capitals":
                    CountCapitals();
                    break;
                case "no task":
                    break;
                default:
                    throw new Exception("The selected option is not " +
                                        "available on the main program");
            }
        }

        public static void ShowDate()
        {
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("dddd, dd MMMM yyyy");
            Console.WriteLine("Current date is: " + formattedDate);
        }
        
        public static void ShowTime()
        {
            DateTime currentTime = DateTime.Now;
            string formattedTime = currentTime.ToString("HH:mm:ss");
            Console.WriteLine("Current time is: " + formattedTime);
        }

        public static void ShowVersion()
        {
            const string k_Version = "23.3.4.9835";
            Console.WriteLine("Version:{0}", k_Version);
        }
        
        public static void CountCapitals()
        {
            string userSentence;
            uint capitalsCount = 0;
            
            Console.WriteLine("Please enter your sentence:");
            userSentence = Console.ReadLine();
            
            foreach (char character in userSentence)
            {
                capitalsCount += char.IsUpper(character) ? 1u : 0u;
            }
            
            Console.WriteLine("There are {0} capital letters in your sentence",capitalsCount);
        }
    }
}