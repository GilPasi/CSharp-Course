using System;
namespace Ex04.Menus.Interfaces
{
    public class Option
    {
        /// <summary>
        /// Important note: every none-root node in the menus tree is
        /// possible option to choose.Specifically leaf-menus are nodes with no
        /// sub menus == final option.
        /// </summary>
        
        protected bool m_IsRoot = true;
        protected eSystemStatus m_CurrentStatus = eSystemStatus.Ongoing;
        protected string m_Task = "no task";
        protected string m_OptionName;
        protected string m_OptionText;
        protected List<ISelectionObserver> m_Observers = new List<ISelectionObserver>();
        
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
        
        public bool IsLeafOption
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

        public void AttachObserver(ISelectionObserver i_NewObserver)
        {
            m_Observers.Add(i_NewObserver);
        }
        
        //#Operations:
        public void SignalSelection()
        {
            m_CurrentStatus = eSystemStatus.ForceExit;
            //Update current status and keep propagating
            PropagateMessage(this);
        }

        protected void PropagateMessage(Option i_TriggerOption)
        {
            foreach (ISelectionObserver observer in m_Observers)
            {
                observer.HandleSelect(i_TriggerOption);
            }
        }
    }
}