public class FuelEngine : Engine
{
    private eFuelType m_FuelType;
    public FuelEngine(eFuelType i_FuelType, float i_MaxFuelCapacity) : base(i_MaxFuelCapacity)
    {
        m_FuelType = i_FuelType;
    }

    public void Refuel(float i_FuelToAdd, eFuelType i_FuelType)
    {
        if(i_FuelType != m_FuelType)
        {
            throw new ArgumentException($"Incorrect fuel type. Expected: {m_FuelType}, Received: {i_FuelType}");
        }

        AddEnergy(i_FuelToAdd);
    }
}