namespace Ex03.GarageLogic
{
	public class FuelEngine : Engine
	{
	    private eFuelType m_FuelType;
	    public FuelEngine(eFuelType i_FuelType, float i_MaxFuelCapacity) : base(i_MaxFuelCapacity)
	    {
	        m_FuelType = i_FuelType;
	    }

	    public void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
	    {
	        if (i_FuelType != m_FuelType)
	        {
	            throw new ArgumentException($"Incorrect fuel type. Expected: {m_FuelType}, Received: {i_FuelType}");
	        }

	        EnergyAddition(i_FuelToAdd);
	    }

	    public override string ToString()
	    {
	        return $"{base.ToString()}, Fuel Type: {m_FuelType}";
	    }
	}
}