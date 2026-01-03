namespace Ex03.GarageLogic
{
	public class ElectricEngine : Engine
	{
	    public ElectricEngine(float i_MaxBatteryCapacity) : base(i_MaxBatteryCapacity)
	    {
	    }

	    public void Recharge(float i_HoursToAdd)
	    {
	        EnergyAddition(i_HoursToAdd);
	    }
	}
}