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
            return int.TryParse(i_Input, out int inputAsInt) && 
                   IOManipulator.IsItemInRange(inputAsInt, m_Range) ;
        }
    }
}

