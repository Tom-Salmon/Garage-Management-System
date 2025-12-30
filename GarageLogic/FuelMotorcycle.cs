using System.Text;
public class FuelMotorcycle : Motorcycle
{
    public FuelMotorcycle(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
    {
        m_Engine = new FuelEngine(eFuelType.Octan98, 6.8f);
    }
}