using System.Text;
public class Motorcycle : Vehicle
{
    private const int k_NumberOfWheels = 2;
    private const float k_MaxAirPressure = 31f;
    private eLicenseType m_LicenseType;
    private int m_EngineVolume;

    public Motorcycle(string i_ModelName, string i_LicenseNumber, Engine i_Engine) : base(i_ModelName, i_LicenseNumber)
    {
        m_Engine = i_Engine;
        for (int i = 0; i < k_NumberOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(k_MaxAirPressure));
        }
    }

    public override Dictionary<string, Type> GetVehicleSpecificParameters()
    {
        Dictionary<string, Type> parameters = new Dictionary<string, Type>();

        parameters.Add("License Type", typeof(eLicenseType));
        parameters.Add("Engine Volume", typeof(int));
        
        return parameters;
    }

    public override void SetVehicleSpecificParameters(Dictionary<string, object> i_VehicleParameters)
    {
        m_LicenseType = (eLicenseType)i_VehicleParameters["License Type"];
        m_EngineVolume = (int)i_VehicleParameters["Engine Volume"];
    }

    public override string ToString()
    {
        StringBuilder motorcycleInfo = new StringBuilder();

        motorcycleInfo.AppendLine(base.ToString());
        motorcycleInfo.AppendLine($"License Type: {m_LicenseType}");
        motorcycleInfo.AppendLine($"Engine Volume: {m_EngineVolume} cc");

        return motorcycleInfo.ToString();
    }
}