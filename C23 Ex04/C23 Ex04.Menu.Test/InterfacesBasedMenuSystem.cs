namespace Ex04.Menus.Interfaces
{
    public class InterfacesBasedMenuSystem : ISelectionObserver
    {
        private Menu m_RootOption = new Menu();

        public void constructInterfacesMenu()
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
            DateOption.AttachObserver(this);
            TimeOption.AttachObserver(this);
            VersionOption.AttachObserver(this);
            CountCapitalsOption.AttachObserver(this);
            DateOption.Task = "show date";
            TimeOption.Task = "show time";
            VersionOption.Task = "show version";
            CountCapitalsOption.Task = "count capitals";
            DateOption.Name = "Show Date";
            TimeOption.Name = "Show Time";
            VersionOption.Name = "Show Version";
            CountCapitalsOption.Name = "Count Capitals";
            Menu dateTimeSubOption = new Menu();
            dateTimeSubOption.Text = "Show Date/Time";
            dateTimeSubOption.InferName();
            dateTimeSubOption.AttachSubOption(DateOption);
            dateTimeSubOption.AttachSubOption(TimeOption);
            Menu versionCapitalsSubOption = new Menu();
            versionCapitalsSubOption.Text = "Version and Capitals";
            versionCapitalsSubOption.InferName();
            versionCapitalsSubOption.AttachSubOption(VersionOption);
            versionCapitalsSubOption.AttachSubOption(CountCapitalsOption);
            RootOption.Text = "Interfaces Main Menu";
            RootOption.AttachSubOption(dateTimeSubOption);
            RootOption.AttachSubOption(versionCapitalsSubOption);
        }

        public Menu RootOption
        {
            get
            {
                return m_RootOption;
            }
            set
            {
                m_RootOption = value;
            }
        }

        public void Initiate()
        {
            RootOption.Initiate();
        }

        public void HandleSelect(Option i_OptionReference)
        {
            Program.FulfillRequest(i_OptionReference.Task);
        }
    }
}

