using System.Text;

public class FuelTruck : Vehicle
{
    private const int k_NumberOfWheels = 14;
    private const float k_MaxAirPressure = 26f;
    private bool m_IsCarryingHazardousMaterials;
    private float m_CargoVolume;

    public FuelTruck(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
    {
        m_Engine = new FuelEngine(eFuelType.Soler, 140f);
        
        for (int i = 0; i < k_NumberOfWheels; i++)
        {
            m_Wheels.Add(new Wheel(k_MaxAirPressure));
        }
    }

    public override Dictionary<string, Type> GetVehicleSpecificParameters()
    {
        Dictionary<string, Type> parameters = new Dictionary<string, Type>();

        parameters.Add("Is Carrying Hazardous Materials", typeof(bool));
        parameters.Add("Cargo Volume", typeof(float));

        return parameters;
    }

    public override void SetVehicleSpecificParameters(Dictionary<string, object> i_VehicleParameters)
    {
        m_IsCarryingHazardousMaterials = (bool)i_VehicleParameters["Is Carrying Hazardous Materials"];
        m_CargoVolume = (float)i_VehicleParameters["Cargo Volume"];
    }

    public override string ToString()
    {
        StringBuilder truckInfo = new StringBuilder();

        truckInfo.AppendLine(base.ToString());
        truckInfo.AppendLine($"Carrying Hazardous Materials: {m_IsCarryingHazardousMaterials}");
        truckInfo.AppendLine($"Cargo Volume: {m_CargoVolume} cubic meters");

        return truckInfo.ToString();
    }
}