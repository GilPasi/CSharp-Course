namespace Ex03;

public class ValueOutOfRangeException : Exception
{
    private float m_maxValue;
    private float m_minValue;
    private const int k_RangeDimentions = 2;

    public float[] Range
    {
        get
        {
            return new float[k_RangeDimentions] { m_minValue, m_maxValue };
        }
    }
    public ValueOutOfRangeException(Exception i_BaseException, float i_MinValue, float i_MaxValue)
    :base(string.Format(
        "{0} Value must be in range {1}-{2}", i_BaseException.Message ,i_MinValue, i_MaxValue )
        ,i_BaseException)
    {
        
        m_maxValue = i_MaxValue;
        m_minValue = i_MinValue;
    }
}