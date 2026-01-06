namespace Ex03.GarageLogic
{
	public abstract class Engine
	{
	    protected float m_CurrentEnergy;
	    protected readonly float m_MaxEnergy;
	    public float EnergyPercentage
	    {
	        get
	        {
	            return (m_CurrentEnergy / m_MaxEnergy) * 100;
	        }
	    }  
	    public float CurrentEnergy
	    {
	        get
	        {
	            return m_CurrentEnergy;
	        }
	        set
	        {
	            if (value < 0 || value > m_MaxEnergy)
	            {
	                throw new ValueRangeException(0, m_MaxEnergy);
	            }
	            else
	            {
	                m_CurrentEnergy = value;
	            }
	        }
	    }
	    public float MaxEnergy
	    {
	        get
	        {
	            return m_MaxEnergy;
	        }
	    }

	    public Engine(float i_MaxEnergy)
	    {
	        m_MaxEnergy = i_MaxEnergy;
	    }
	    
	    protected void EnergyAddition(float i_EnergyToAdd)
	    {
	        if (m_CurrentEnergy + i_EnergyToAdd > m_MaxEnergy)
	        {
	            throw new ValueRangeException(0, m_MaxEnergy - m_CurrentEnergy);
	        }

	        m_CurrentEnergy += i_EnergyToAdd;
	    }

	    public override string ToString()
	    {
	        return $"Energy Percentage: {EnergyPercentage:F1}% ({CurrentEnergy} / {MaxEnergy})";
	    }
	}
}