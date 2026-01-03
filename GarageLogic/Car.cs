using System.Text;
public abstract class Car : Vehicle
{
    private const int k_NumberOfWheels = 5;
    private const float k_MaxAirPressure = 33f;
    private eColor m_Color;
    private eNumberOfDoors m_NumberOfDoors;

    public Car(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
    {
        for (int i = 0; i < k_NumberOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(k_MaxAirPressure));
        }
    }

    public override Dictionary<string, Type> GetVehicleSpecificParameters()
    {
        Dictionary<string, Type> parameters = base.GetVehicleSpecificParameters();
        parameters.Add("Color", typeof(eColor));
        parameters.Add("Number of Doors", typeof(eNumberOfDoors));

        return parameters;
    }
    public override void SetVehicleSpecificParameters(Dictionary<string, object> i_VehicleParameters)
    {
        base.SetVehicleSpecificParameters(i_VehicleParameters);
        try
        {
            m_Color = (eColor)Enum.Parse(typeof(eColor), i_VehicleParameters["Color"].ToString());
            m_NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), i_VehicleParameters["Number of Doors"].ToString());
        }
        catch (ArgumentException)
        {
            throw new FormatException("One or more of the properties is not valid.");
        }
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