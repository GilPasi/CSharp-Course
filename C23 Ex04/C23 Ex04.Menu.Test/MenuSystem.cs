namespace Ex04.Menus.Interfaces
{
    public class MenuSystem : ISelectionObserver
    {
        Menu m_RootMenu = new Menu();

        public void constructInterfacesMenu()
        {
            /*This method was implemented as a member method
            rather than a constructor because the that
            procedure is relevant for this assignment 
            and will not be relevant when reusing the 
            MenuSystem class again */
            Menu DateOption = new Menu();
            Menu TimeOption = new Menu();
            Menu VersionOption = new Menu();
            Menu CountCapitalsOption = new Menu();
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
            Menu DateTimeSubMenu = new Menu();
            DateTimeSubMenu.Text = "Show Date/Time";
            DateTimeSubMenu.InferName();
            DateTimeSubMenu.AttachSubMenu(DateOption);
            DateTimeSubMenu.AttachSubMenu(TimeOption);
            Menu VersionCapitalsSubMenu = new Menu();
            VersionCapitalsSubMenu.Text = "Version and Capitals";
            VersionCapitalsSubMenu.InferName();
            VersionCapitalsSubMenu.AttachSubMenu(VersionOption);
            VersionCapitalsSubMenu.AttachSubMenu(CountCapitalsOption);
            RootMenu.Text = "Interfaces Main Menu";
            RootMenu.AttachSubMenu(DateTimeSubMenu);
            RootMenu.AttachSubMenu(VersionCapitalsSubMenu);
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
            while (!RootMenu.Initiate()) ;
        }

        public void HandleSelect(Menu i_MenuReference)
        {
            Program.FullfillRequest(i_MenuReference.Task);
        }
    }
}

