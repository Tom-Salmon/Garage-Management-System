using System.Text;

namespace Ex03.GarageLogic
{
	public class ElectricCar : Car
	{
	    public ElectricCar(string i_ModelName, string i_LicenseNumber) : base(i_ModelName, i_LicenseNumber)
	    {
	        m_Engine = new ElectricEngine(4.2f);
	    }
	}
}