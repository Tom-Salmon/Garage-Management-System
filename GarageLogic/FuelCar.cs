using System.Text;
public class FuelCar : Car
{
    public FuelCar(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
    {
        m_Engine = new FuelEngine(eFuelType.Octan95, 47f);
    }
}