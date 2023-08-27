namespace Ex03
{
    public class BoundedIntParser : IParser
    {
        private int[] m_Range;

        public BoundedIntParser(int[] i_Range )
        {
            m_Range = i_Range;
        }

        public bool TryParse(string i_Input, out object o_InputAsBoundedInt)
        {
            bool successfulParse = IsParseable(i_Input);

            if (successfulParse)
            {
                o_InputAsBoundedInt = int.Parse(i_Input);
            }
            else
            {
                o_InputAsBoundedInt = 0;
            }

            return successfulParse;
        }

        public bool IsParseable(string i_Input)
        {
            return int.TryParse(i_Input, out int inputAsInt) && IsItemInRange(inputAsInt, m_Range) ;
        }
        
        public bool IsItemInRange<T>(T i_ExaminedValue, T[] i_Range) where T : IComparable<T>
        {
            return i_ExaminedValue.CompareTo(i_Range[0]) >= 0 && i_ExaminedValue.CompareTo(i_Range[1]) <= 0  ;
        }
        
        public bool IsItemInRange<T>(T i_ExaminedValue, T i_BottomBound, T i_TopBound) where T : IComparable<T>
        {
            return i_ExaminedValue.CompareTo(i_BottomBound) >= 0 && i_ExaminedValue.CompareTo(i_TopBound) <= 0  ;
        }
        
        public void IncreaseUpperBound()
        {
            m_Range[1]++;
        }
    }
}

