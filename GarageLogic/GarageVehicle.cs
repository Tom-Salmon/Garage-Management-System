using System.Text;

namespace Ex03.GarageLogic
{
	public class GarageVehicle
	{
		private string m_OwnerName;
		private string m_OwnerPhone;
		private eVehicleStatus m_VehicleStatus;
		private Vehicle m_Vehicle;
		
		public eVehicleStatus Status
		{
			get
			{
				return m_VehicleStatus;
			}
			set
			{
				m_VehicleStatus = value;
			}
				
		}
		public Vehicle Vehicle
		{
			get
			{
				return m_Vehicle;
			}
		}
		
		public GarageVehicle(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
		{
			m_OwnerName = i_OwnerName;
			m_OwnerPhone = i_OwnerPhone;
			m_Vehicle = i_Vehicle;
			m_VehicleStatus = eVehicleStatus.InRepair;
		}
		
		public override string ToString()
		{
			StringBuilder garageVehicleInfo = new StringBuilder();
			
			garageVehicleInfo.AppendLine("     Garage Record     ");
			garageVehicleInfo.AppendLine($"Owner Name: {m_OwnerName}");
			garageVehicleInfo.AppendLine($"Phone Number: {m_OwnerPhone}");
			garageVehicleInfo.AppendLine($"Vehicle Status: {m_VehicleStatus}");
			garageVehicleInfo.AppendLine(m_Vehicle.ToString());

			return garageVehicleInfo.ToString();
		}
	}
}
