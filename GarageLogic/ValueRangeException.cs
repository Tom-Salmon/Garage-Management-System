
public class ValueRangeException : Exception
{
    private float m_MaxValue;
    private float MinValue
    {
        get 
        { 
            return m_MinValue;
        }
    }
    private float m_MinValue;
    private float MaxValue
    {
        get 
        { 
            return m_MaxValue; 
        }
    }

    public ValueRangeException(float i_MinValue, float i_MaxValue) : base($"Value is out of range. Should be between {i_MinValue} and {i_MaxValue}.")
    {
        m_MinValue = i_MinValue;
        m_MaxValue = i_MaxValue;
    }

}