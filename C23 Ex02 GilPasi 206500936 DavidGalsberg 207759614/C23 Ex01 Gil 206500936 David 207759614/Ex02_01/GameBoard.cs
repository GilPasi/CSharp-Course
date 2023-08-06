using System.Text;
namespace sapir_c23_dn_course_gil_and_david.Ex02_01
{
    public class GameBoard
    {
        private const string k_verticalDelimiter = "|";
        private const string k_horizonalDelimiter = "|=========|=========|";
        private const int k_QunatityOfNeededCharactersForPrintingOneTurn = 50;
        private  readonly uint r_CurrentStateAsStringSize;
        private StringBuilder m_currentStateAsString;
        private List <Turn> historyTurns = new List<Turn>();

        public GameBoard(uint i_MaxTries)
        {
            r_CurrentStateAsStringSize = (1 + i_MaxTries) * k_QunatityOfNeededCharactersForPrintingOneTurn;
            m_currentStateAsString = new StringBuilder((int)r_CurrentStateAsStringSize);
        }

        public void AddTurn(Turn i_NewTurn)
        {
            
        }

        public void PrintCurrentState()
        {
            m_currentStateAsString.Append(string.Format("Current board status:{0}", Environment.NewLine));
            m_currentStateAsString.Append(string.Format("{0}Pins:    {0}Result:  {0}{1}",k_verticalDelimiter, Environment.NewLine));
            m_currentStateAsString.Append(string.Format("{0}{1}", k_horizonalDelimiter, Environment.NewLine));
            m_currentStateAsString.Append(string.Format("|{0}|" ,Turn.GetHiddenSequenceString()));

            foreach (Turn passedTurn in historyTurns)
            {
                m_currentStateAsString.Append(string.Format("|{0}|" ,passedTurn.ToString()));
            }
            
            Console.WriteLine(m_currentStateAsString.ToString());
        }
    }
}



