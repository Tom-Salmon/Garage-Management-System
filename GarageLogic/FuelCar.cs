using System.Text;

namespace Ex03.GarageLogic
{
	public class FuelCar : Car
	{
        public FuelCar(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
	        m_Engine = new FuelEngine(eFuelType.Octan95, 47f);
	    }
	}
}