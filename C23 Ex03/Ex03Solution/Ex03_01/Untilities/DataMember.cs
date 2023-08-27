namespace Ex03
{
    public abstract class PseudoAttribute
    {
        protected readonly string m_Name;
        protected readonly IParser m_Parser;
        protected bool m_HasValue;
        /*The "HasValue" property implemets a similar mechnism
          To the Nullable assignemnt mechanism.Thus inevitably 
          Any PseudoAttribute instance can be not initiated at all.
          This Allows the ConsoleUI to manage classes even if it does 
          not know them in details.*/
        

        public PseudoAttribute(string i_Name, IParser i_Parser)
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

        public bool HasValue
        {
            get
            {
                return m_HasValue;
            }
        }

        public abstract bool hasFinitePossibleValues();

        public abstract List<object> PossibleValues
        {
            get;
        }

        public abstract Type ValueType
        {
            get;
        }
        
        public abstract object Value
        {
            get;
            set;
        }

    }

    public class PseudoAttribute<T> : PseudoAttribute
    {

        private Type m_ValueType;
        private T? m_Value = default(T);
        protected readonly List<T> m_PossibleValues;

        public PseudoAttribute(string i_Name, IParser i_Parser, List<T> i_PossibleValues = null)
            :base(i_Name, i_Parser)
        {
            if (i_PossibleValues == null)
            {
                m_PossibleValues = new List<T>();
            }
            else
            {
                m_PossibleValues = i_PossibleValues;
            }
        }
        public PseudoAttribute(string i_Name, IParser i_Parser, object i_Value, List<T> i_PossibleValues = null)
            :this(i_Name, i_Parser, i_PossibleValues)
        {
            Value = i_Value;
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
        
        public override object Value
        {
            get
            {
                return m_Value;
            }
            
            set
            {
                m_Value = (T)value;
                m_HasValue = true;
            }
        }
        
        public override Type ValueType
        {
            get
            {
                return typeof(T);
            }
        }

    }   
}

