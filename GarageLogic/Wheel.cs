public class Wheel
{
    private string m_ManufacturerName;
    private float m_CurrentAirPressure;
    private float m_MaxAirPressure;

    public string ManufacturerName
    {
        get
        {
            return m_ManufacturerName;
        }
    }

    public float CurrentAirPressure
    {
        get
        {
            return m_CurrentAirPressure;
        }
    }

    public float MaxAirPressure
    {
        get
        {
            return m_MaxAirPressure;
        }
    }

    public Wheel(float i_MaxAirPressure)
    {
        m_MaxAirPressure = i_MaxAirPressure;
    }

    public void Inflate(float i_AirToAdd)
    {
        if (m_CurrentAirPressure + i_AirToAdd > m_MaxAirPressure)
        {
            throw new ValueRangeException(0, m_MaxAirPressure - m_CurrentAirPressure);
        }

        m_CurrentAirPressure += i_AirToAdd;
    }

    public override string ToString()
    {
        return $"Manufacturer: {m_ManufacturerName}, Current Air Pressure: {m_CurrentAirPressure} (Max: {m_MaxAirPressure})";
    }
}