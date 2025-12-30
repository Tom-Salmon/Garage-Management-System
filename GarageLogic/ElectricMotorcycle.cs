using System.Text;
public class ElectricMotorcycle : Motorcycle
{
    public ElectricMotorcycle(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
    {
        m_Engine = new ElectricEngine(2.6f);
    }
}