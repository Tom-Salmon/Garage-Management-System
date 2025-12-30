using System.Text;

public abstract class Vehicle
{
    protected readonly string m_ModelName;
    protected readonly string m_LicenseNumber;
    protected List<Wheel> m_Wheels;
    protected Engine m_Engine;
    public float EnergyPercentage
    {
        get 
        { 
            return Engine.EnergyPercentage; 
        }
    }
    public string LicenceNumber
    {
        get 
        { 
            return m_LicenseNumber; 
        }
    }
    public string ModelName
    {
        get 
        { 
            return m_ModelName; 
        }
    }
    public List<Wheel> Wheels
    {
        get 
        { 
            return m_Wheels; 
        }
    }
    public Engine Engine
    {
        get 
        { 
            return m_Engine; 
        }
    }

    public Vehicle(string i_ModelName, string i_LicenseNumber)
    {
        m_ModelName = i_ModelName;
        m_LicenseNumber = i_LicenseNumber;
        m_Wheels = new List<Wheel>();
    }

    public void InflateWheelsToMax()
    {
        foreach (Wheel wheel in m_Wheels)
        {
            float airToAdd = wheel.MaxAirPressure - wheel.CurrentAirPressure;
            if(airToAdd > 0)
            {
                wheel.Inflate(airToAdd);
            }
        }
    }

    public override string ToString()
    {
        StringBuilder vehicleInfo = new StringBuilder();
        vehicleInfo.AppendLine($"License Number: {m_LicenseNumber}");
        vehicleInfo.AppendLine($"Model Name: {m_ModelName}");
        vehicleInfo.AppendLine("Engine Info:");
        vehicleInfo.AppendLine(m_Engine.ToString());
        vehicleInfo.AppendLine("Wheels Info:");
        int wheelIndex = 1;
        foreach (Wheel wheel in m_Wheels)
        {
            vehicleInfo.AppendLine($"{wheelIndex++}. {wheel}");
        }
        return vehicleInfo.ToString();
    }


    public abstract Dictionary<string, Type> GetVehicleSpecificParameters();

    public abstract void SetVehicleSpecificParameters(Dictionary<string, object> i_VehicleParameters);

}