using System;
namespace Ex04.Menus.Interfaces
{
    public class Menu : ISelectionObserver
    {
        private bool m_IsRoot = true;
        private eSystemStatus m_CurrentStatus = eSystemStatus.Ongoing;
        public enum eSystemStatus
        {
            Ongoing,
            BackOneOrExit,
            ForceExit,
        }

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
        
        //#Accessors & Mutators
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

        public bool IsRootMenu
        {
            get
            {
                return m_IsRoot;
            }
            set
            {
                m_IsRoot = value;
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
        
        public void InferName()
        {
            if (Name == null && Text != null)
            {
                Name = Text;
            }
        }

        public void AttachSubMenu(Menu i_NewMenu)
        {
            i_NewMenu.m_IsRoot = false;
            m_SubMenus.Add(i_NewMenu);
            i_NewMenu.AttachObserver(this);
        }

        public void AttachObserver(ISelectionObserver i_NewObserver)
        {
            m_Observers.Add(i_NewObserver);
        }
        
        //#Operations
        
        /// <summary>
        /// This Menu node is implemented as a part of
        /// a while menus tree.Therefore it has a
        /// mechanism for propagating the user's input
        /// down and up the tree so all the nodes are
        /// updated with the current status in any given  moment.
        /// </summary>
        public void Initiate()
        {
            if (IsLeafMenu)
            {
                m_CurrentStatus = eSystemStatus.ForceExit;
                NotifyAllObservers();
            }
            else
            {
                manageMenu();
            }
        }

        private void manageMenu()
        {
            while (m_CurrentStatus != eSystemStatus.ForceExit)
            {
                if (m_CurrentStatus == eSystemStatus.BackOneOrExit)
                {
                    //One step back is already taken
                    m_CurrentStatus = eSystemStatus.Ongoing;
                }

                int userChoice = PresentMenu();
                if (doesUserWantToGoBack(userChoice))
                {
                    m_CurrentStatus = eSystemStatus.BackOneOrExit;
                    break;// == go back one menu
                }

                if (m_CurrentStatus == eSystemStatus.Ongoing)
                {
                    Menu userChoiceAsMenu = m_SubMenus[userChoice - 1];
                    userChoiceAsMenu.Initiate();
                }
            }
        }

        private void NotifyAllObservers(Menu i_TriggerMenu = null)
        {
            if (i_TriggerMenu == null)
            {
                i_TriggerMenu = this;
            }

            foreach (ISelectionObserver observer in m_Observers)
            {
                observer.HandleSelect(i_TriggerMenu);
            }
        }

        public int PresentMenu()
        {
            int menuCount = 1;
            
            Console.WriteLine("Enter your request (1 to {0} or press '0' to {1})",
                m_SubMenus.Count, OptionZero);
            Console.WriteLine("**{0}**", Text);
            foreach (Menu subMenu in m_SubMenus)
            {
                Console.WriteLine("{0} -> {1}", menuCount++, subMenu.Name);
            }

            Console.WriteLine("0 -> {0}", OptionZero );
            return ForceOptionInput();
        }

        //#Utility methods:
        private int ForceOptionInput()
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
        
        private static bool doesUserWantToGoBack(int i_UserChoice)
        {
            return i_UserChoice == 0;
        }
        
        private static bool IsInRange(int i_Value, int i_UpperBound = int.MaxValue, int i_LowerBound = 0)
        {
            return i_Value < i_UpperBound && i_Value >= i_LowerBound;
        }
        
        //#Implementations:
        public void HandleSelect(Menu i_TriggerMenu)
        {
            m_CurrentStatus = i_TriggerMenu.m_CurrentStatus;
            foreach (ISelectionObserver observer in m_Observers)
            {
                if (observer is Menu)
                {
                    observer.HandleSelect(i_TriggerMenu);
                }
            }
        }
    }
}