using System.Text;

namespace Ex03.GarageLogic
{
	public abstract class Motorcycle : Vehicle
	{
	    protected const int k_NumberOfWheels = 2;
	    protected const float k_MaxAirPressure = 31f;
	    protected eLicenseType m_LicenseType;
	    protected int m_EngineVolume;

	    public Motorcycle(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
	    {        
	        for (int i = 0; i < k_NumberOfWheels; i++)
	        {
	            m_Wheels.Add(new Wheel(k_MaxAirPressure));
	        }
	    }

	    public override Dictionary<string, Type> GetVehicleSpecificParameters()
	    {
	        Dictionary<string, Type> parameters = base.GetVehicleSpecificParameters();

	        parameters.Add("License Type", typeof(eLicenseType));
	        parameters.Add("Engine Volume", typeof(int));
	        
	        return parameters;
	    }

	    public override void SetVehicleSpecificParameters(Dictionary<string, object> i_VehicleParameters)
	    {
	        base.SetVehicleSpecificParameters(i_VehicleParameters);
	        try
	        {
	            m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_VehicleParameters["License Type"].ToString());
	            m_EngineVolume = int.Parse(i_VehicleParameters["Engine Volume"].ToString());
	        }
	        catch (ArgumentException)
	        {
	            throw new FormatException("The provided License Type is not valid.");
	        }
	        catch (FormatException)
	        {
	            throw new FormatException("Engine Volume must be a valid whole number.");
	        }
	    }

	    public override string ToString()
	    {
	        StringBuilder motorcycleInfo = new StringBuilder();

	        motorcycleInfo.Append(base.ToString());
	        motorcycleInfo.AppendLine($"License Type: {m_LicenseType}");
	        motorcycleInfo.AppendLine($"Engine Volume: {m_EngineVolume} cc");

	        return motorcycleInfo.ToString();
	    }
	}
}