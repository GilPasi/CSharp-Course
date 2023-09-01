namespace Ex04.Menus.Delegates
{
    public class DelegatesBasedMenuSystem 
    {
        private Menu m_RootMenu = new Menu();

        public void constructDelegatesMenu()
        {
            /*This method was implemented as a member method
            rather than a constructor because the that
            procedure is relevant for this assignment 
            and will not be relevant when reusing the 
            MenuSystem class again */
            Option DateOption = new Option();
            Option TimeOption = new Option();
            Option VersionOption = new Option();
            Option CountCapitalsOption = new Option();
            DateOption.Task = "show date";
            TimeOption.Task = "show time";
            VersionOption.Task = "show version";
            CountCapitalsOption.Task = "count capitals";
            DateOption.Name = "Show Date";
            TimeOption.Name = "Show Time";
            VersionOption.Name = "Show Version";
            CountCapitalsOption.Name = "Count Capitals";
            Menu DateTimeSubMenu = new Menu();
            DateTimeSubMenu.Text = "Show Date/Time";
            DateTimeSubMenu.InferName();
            DateTimeSubMenu.AttachOption(DateOption);
            DateTimeSubMenu.AttachOption(TimeOption);
            Menu VersionCapitalsSubMenu = new Menu();
            VersionCapitalsSubMenu.Text = "Version and Capitals";
            VersionCapitalsSubMenu.InferName();
            VersionCapitalsSubMenu.AttachOption(VersionOption);
            VersionCapitalsSubMenu.AttachOption(CountCapitalsOption);
            RootMenu.Text = "Delegates Main Option";
            RootMenu.AttachOption(DateTimeSubMenu);
            RootMenu.AttachOption(VersionCapitalsSubMenu);
            RootMenu.m_ChoiceSelectedDelegate += LeafOption_Selection;

        }

        public Menu RootMenu
        {
            get
            {
                return m_RootMenu;
            }
            set
            {
                m_RootMenu = value;
            }
        }

        public void Initiate()
        {
            eSystemStatus status = eSystemStatus.Ongoing;
            RootMenu.Initiate();
        }

        public void LeafOption_Selection(Option i_TriggerOption)
        {
            Program.FulfillRequest(i_TriggerOption.Task);
        }
    }
}

