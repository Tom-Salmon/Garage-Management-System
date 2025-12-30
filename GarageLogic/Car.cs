using System.Text;
public class Car : Vehicle
{
    private const int k_NumberOfWheels = 5;
    private const float k_MaxAirPressure = 33f;
    private eColor m_Color;
    private eNumberOfDoors m_NumberOfDoors;

    public Car(string i_ModelName, string i_LicenseNumber, Engine i_Engine) : base(i_ModelName, i_LicenseNumber)
    {
        m_Engine = i_Engine;
        for (int i = 0; i < k_NumberOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(k_MaxAirPressure));
        }
    }

    public override Dictionary<string, Type> GetVehicleSpecificParameters()//TODO
    {
        Dictionary<string, Type> parameters = new Dictionary<string, Type>();
        parameters.Add("Color", typeof(eColor));
        parameters.Add("Number of Doors", typeof(eNumberOfDoors));
        return parameters;
    }
    public override void SetVehicleSpecificParameters(Dictionary<string, object> i_VehicleParameters)//TODO
    {
        m_Color = (eColor)i_VehicleParameters["Color"];
        m_NumberOfDoors = (eNumberOfDoors)i_VehicleParameters["Number of Doors"];
    }

    public override string ToString()
    {
        StringBuilder carInfo = new StringBuilder();
        carInfo.AppendLine(base.ToString());
        carInfo.AppendLine($"Car Color: {m_Color}");
        carInfo.AppendLine($"Number of Doors: {(int)m_NumberOfDoors}");
        return carInfo.ToString();
    }

}