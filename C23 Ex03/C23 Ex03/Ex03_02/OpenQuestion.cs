namespace Ex03
{
    public class OpenQuestion<T> : Question<T>
    {
        private IParser m_Parser;

        public OpenQuestion(string i_Wording, string i_ErrorMessage, IParser i_Parser)
            : base(i_Wording, i_ErrorMessage)
        {
            m_Parser = i_Parser;
        }
        
        public override T Ask()
        {
            PresentQuestion();
            return IOManipulator.ParseInput<T>(m_ErrorMessage,m_Parser);
        }
        
        public override T AskAndForceInput()
        {
            PresentQuestion();
            return IOManipulator.ForceParseableInput<T>(m_ErrorMessage,m_Parser);
        }
    }
}

