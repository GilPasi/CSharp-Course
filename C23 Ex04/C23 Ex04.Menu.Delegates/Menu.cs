namespace Ex04.Menus.Delegates
{
    public class Menu : Option
    {
        private List<Option> m_SubOptions = new List<Option>();
        
        //#Accessors & Mutators:
        public void AttachOption(Option i_NewOption)
        {
            i_NewOption.IsRootOption = false;
            i_NewOption.m_ChoiceSelectedDelegate += this.LeafOption_Selected;
            this.m_SubOptions.Add(i_NewOption);
        }
        
        //#Operations
        
        /// <summary>
        /// This Option node is implemented as a part of
        /// a while menus tree.Therefore it has a
        /// mechanism for propagating the user's input
        /// down and up the tree so all the nodes are
        /// updated with the current status in any given  moment.
        /// </summary>

        public void Initiate()
        {
            while (m_CurrentStatus != eSystemStatus.ForceExit)
            {
                if (m_CurrentStatus == eSystemStatus.BackOneOrExit)
                {
                    //One step back is already taken
                    m_CurrentStatus = eSystemStatus.Ongoing;
                }

                int userChoice = PresentOption();
                if (doesUserWantToGoBack(userChoice))
                {
                    m_CurrentStatus = eSystemStatus.BackOneOrExit;
                    break;// == go back one menu
                }

                if (m_CurrentStatus == eSystemStatus.Ongoing)
                {
                    Option userChoiceAsOption = m_SubOptions[userChoice - 1];
                    if (userChoiceAsOption.IsLeafOption)
                    {
                        userChoiceAsOption.OnOptionSelection(userChoiceAsOption);
                    }
                    else
                    {
                        (userChoiceAsOption as Menu).Initiate();
                    }
                }
            }
        }

        public int PresentOption()
        {
            int menuCount = 1;
            
            Console.WriteLine("Enter your request (1 to {0} or press '0' to {1})",
                m_SubOptions.Count, OptionZero);
            Console.WriteLine("**{0}**", Text);
            foreach (Option subOption in m_SubOptions)
            {
                Console.WriteLine("{0} -> {1}", menuCount++, subOption.Name);
            }

            Console.WriteLine("0 -> {0}", OptionZero );
            return ForceOptionInput();
        }
        
        //#Event Handlers:
        private void LeafOption_Selected(Option i_TriggerOption)
        {
            CurrentStatus = i_TriggerOption.CurrentStatus;
            //Update current status and keep propagating
            PropagateMessage(i_TriggerOption);
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
                                && IsInRange(userChoice, m_SubOptions.Count + 1);
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
        
        private static bool doesUserWantToGoBack(int i_UserChoice)
        {
            return i_UserChoice == 0;
        }
    }
}