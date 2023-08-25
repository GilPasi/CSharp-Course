namespace Ex03
{
    public abstract class DataMember
    {
        protected readonly string m_Name;
        protected readonly IParser m_Parser;

        public DataMember(string i_Name, IParser i_Parser)
        {
            m_Name = i_Name;
            m_Parser = i_Parser;
        }
        
        public string Name
        {
            get
            {
                return m_Name;
            }
        }
        
        public IParser Parser
        {
            get
            {
                return m_Parser;
            }
        }

        public abstract bool hasFinitePossibleValues();

        public abstract List<object> PossibleValues
        {
            get;
        }

    }

    public class DataMember<T> : DataMember
    {

        protected readonly List<T> m_PossibleValues;

        public DataMember(string i_Name, IParser i_Parser, List<T> i_PossibleValues = null)
            :base(i_Name, i_Parser)
        {
            if (i_PossibleValues == null)
            {
                i_PossibleValues = new List<T>();
            }
            else
            {
                m_PossibleValues = i_PossibleValues;
            }
        }

        public override bool hasFinitePossibleValues()
        {
            return m_PossibleValues.Count != 0;
        }

        public override List<object> PossibleValues
        {
            get
            {
                List<object> possibleValuesAsObjects = new List<object>();
                foreach (T possibleValue in m_PossibleValues)
                {
                    possibleValuesAsObjects.Add(possibleValue);
                }

                return possibleValuesAsObjects;
            }
        }

    }   
}

