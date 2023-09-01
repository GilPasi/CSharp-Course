namespace Ex04.Menus.Delegates
{
    public class Option 
    {
        protected bool m_IsRoot = true;
        protected eSystemStatus m_CurrentStatus = eSystemStatus.Ongoing;
        protected string m_Task = "no task";
        protected string m_OptionName;
        protected string m_OptionText;
        public event Action<Option> m_ChoiceSelectedDelegate;
        
        //#Accessors & Mutators
        public string Text
        {
            get
            {
                return m_OptionText;
            }
            set
            {
                m_OptionText = value;
            }
        }
        
        public string Name
        {
            get
            {
                return m_OptionName;
            }
            set
            {
                m_OptionName = value;
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

        public bool IsRootOption
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

        public eSystemStatus CurrentStatus
        {
            get
            {
                return m_CurrentStatus;
            }
            set
            {
                m_CurrentStatus = value;
            }
        }
        
        protected bool IsLeafOption
        {
            get
            {
                return !(this is Menu);
            }
        }

        public string OptionZero
        {
            get
            {
                return IsRootOption? "exit" : "back";
            }
        }
        
        public void InferName()
        {
            if (Name == null && Text != null)
            {
                Name = Text;
            }
        }
        
        //#Event Inducers:
        
        /// <summary>
        /// Note that due to the recursive structure of the menu,
        /// all menus and sub-menus are both the event raiser and
        /// event handlers.
        /// </summary>
        
        public void OnOptionSelection(Option i_TriggerOption)
        {
            m_CurrentStatus = i_TriggerOption.m_CurrentStatus;
            //Update current status and keep propagating
            PropagateMessage(i_TriggerOption);
        }

        protected void PropagateMessage(Option i_TriggerOption)
        {
            if (m_ChoiceSelectedDelegate != null)
            {
                m_ChoiceSelectedDelegate.Invoke(i_TriggerOption);
            }
        }
    }
}