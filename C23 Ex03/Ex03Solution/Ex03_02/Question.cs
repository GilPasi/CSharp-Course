namespace Ex03
{
    public abstract class Question<T>
    {
        protected string m_Wording;
        protected string m_ErrorMessage;

        public Question(string i_Wording, string i_ErrorText)
        {
            m_Wording = i_Wording;
            m_ErrorMessage = i_ErrorText;
        }
        
        public Question(Question<T> i_OtherQuestion)
        {
            m_Wording = new string(i_OtherQuestion.m_Wording);
            m_ErrorMessage = new string(i_OtherQuestion.m_ErrorMessage);
        }

        public virtual void PresentQuestion()
        {
            Console.WriteLine(m_Wording);
        }

        public string ErrorMessage
        {
            get
            {
                return m_ErrorMessage;
            }
            set
            {
                m_ErrorMessage = value;
            }
        }
        
        public string Wording
        {
            get
            {
                return m_Wording;
            }
            set
            {
                m_Wording = value;
            }
        }

        public abstract T Ask();

        public abstract T AskAndForceInput();

    }
}

