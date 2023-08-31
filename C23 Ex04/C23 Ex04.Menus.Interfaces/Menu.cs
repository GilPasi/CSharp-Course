using System;
namespace Ex04.Menus.Interfaces
{
    public class Menu
    {
        /// <summary>
        /// Important note: every none-root node in the menus tree is
        /// possible option to choose.Specifically leaf-menus are nodes with no
        /// sub menus == final option.
        /// </summary>
        private string m_Task = "no task";
        private string m_MenuName;
        private string m_MenuText;
        private List<Menu> m_SubMenus = new List<Menu>();
        private List<ISelectionObserver> m_Observers = new List<ISelectionObserver>();
        
        //#Properties:
        public string Text
        {
            get
            {
                return m_MenuText;
            }
            set
            {
                m_MenuText = value;
            }
        }
        
        public string Name
        {
            get
            {
                return m_MenuName;
            }
            set
            {
                m_MenuName = value;
            }
        }

        public string Task
        {
            get
            {
                return m_Task;
            }
            set
            {
                m_Task = value;
            }
        }

        public void InferName()
        {
            if (Name == null && Text != null)
            {
                Name = Text;
            }
        }

        public bool IsRootMenu
        {
            get
            {
                return m_Observers.Count == 0;
            }
        }
        
        public bool IsLeafMenu
        {
            get
            {
                return m_SubMenus.Count == 0;
            }
        }

        public string OptionZero
        {
            get
            {
                return IsRootMenu? "exit" : "back";
            }
        }

        public void AttachSubMenu(Menu i_NewMenu)
        {
            m_SubMenus.Add(i_NewMenu);
        }

        public void AttachObserver(ISelectionObserver i_NewObserver)
        {
            m_Observers.Add(i_NewObserver);
        }
        
        //#Operations
        public bool Initiate()
        {
            bool backMenu = true;   
            
            if (IsLeafMenu)
            {
                NotifyAllObservers();
                backMenu = false;
            }
            else
            {
                // while (backMenu)
                // {
                    backMenu = PresentMenu();                }
                
                //Will result a recursive call to initiate
            // }

            return backMenu;
        }
        
        private void NotifyAllObservers(Menu i_MenuRefernce = null)
        {
            if (i_MenuRefernce == null)
            {
                i_MenuRefernce = this;
            }

            foreach (ISelectionObserver observer in m_Observers)
            {
                observer.HandleSelect(i_MenuRefernce);
            }
        }

        public bool PresentMenu()
        {
            int menuCount = 1;
            int userChoice;
            Menu userChoiceAsMenu;
            bool backOptionWasChosen;
            
            Console.WriteLine("Enter your request (1 to {0} or press '0' to {1})",
                m_SubMenus.Count, OptionZero);
            Console.WriteLine("**{0}**", Text);
            foreach (Menu subMenu in m_SubMenus)
            {
                Console.WriteLine("{0} -> {1}", menuCount++, subMenu.Name);
            }

            Console.WriteLine("0 -> {0}", OptionZero );

            userChoice = ForceOptionInput();
            backOptionWasChosen = userChoice == 0;
            if (!backOptionWasChosen)
            {
                userChoiceAsMenu = m_SubMenus[userChoice - 1];
                userChoiceAsMenu.Initiate();
            }

            return backOptionWasChosen;
        }

        public int ForceOptionInput()
        {
            bool isValidOption = false;
            int userChoice = 0;
            
            while (!isValidOption)
            {
                string userInput = Console.ReadLine();
                isValidOption = int.TryParse(userInput, out  userChoice) 
                                && IsInRange(userChoice, m_SubMenus.Count + 1);
                if (!isValidOption)
                {
                    Console.WriteLine("Your choice was not valid, pleas try again");
                }
            }

            return userChoice;
        }

        private static bool IsInRange(int i_Value, int i_UpperBound = int.MaxValue, int i_LowerBound = 0)
        {
            return i_Value < i_UpperBound && i_Value >= i_LowerBound;
        }
    }
}