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

        public abstract T Ask();

        public abstract T AskAndForceInput();

    }
}

