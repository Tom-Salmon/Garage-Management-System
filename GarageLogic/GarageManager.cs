
namespace Ex03.GarageLogic
{
	public class GarageManager
	{
		private readonly Dictionary<string, GarageVehicle> m_GarageVehicles;

		public GarageManager()
		{
			m_GarageVehicles = new Dictionary<string, GarageVehicle>();//key = LicenseNumber
		}

		private GarageVehicle getVehicleFromGarage(string i_LicenseNumber)
		{
			bool isExists = m_GarageVehicles.TryGetValue(i_LicenseNumber, out GarageVehicle requestedVehicle);

			if (isExists)
			{
				return requestedVehicle;
			}
			else
			{
				throw new ArgumentException($"License number '{i_LicenseNumber}' does not exist in the garage.");
			}
		}

		public bool IsVehicleInGarage(string i_LicenseNumber)
		{
			return m_GarageVehicles.ContainsKey(i_LicenseNumber);
		}

		public bool AddVehicle(string i_LicenseNumber, string i_ModelName, string i_VehicleType, string i_OwnerName, string i_OwnerPhone, out Vehicle o_NewVehicle)///void?
		{
			bool isExists = m_GarageVehicles.TryGetValue(i_LicenseNumber, out GarageVehicle vehicleToInsert);

			if (isExists)
			{
				vehicleToInsert.Status = eVehicleStatus.InRepair;
				o_NewVehicle = null;
			}
			else
			{
				o_NewVehicle = VehicleCreator.CreateVehicle(i_VehicleType, i_LicenseNumber, i_ModelName);
				vehicleToInsert = new GarageVehicle(i_OwnerName, i_OwnerPhone, o_NewVehicle);
				m_GarageVehicles.Add(i_LicenseNumber, vehicleToInsert);
			}

			return isExists;
		}

		public List<string> GetLicenseNumbers(eVehicleStatus? i_Filter = null)
		{
			List<string> licenseNumbers = new List<string>();

			foreach (KeyValuePair<string, GarageVehicle> entry in m_GarageVehicles)
			{
				if (i_Filter == null || i_Filter == entry.Value.Status)
				{
					licenseNumbers.Add(entry.Key);
				}
			}

			return licenseNumbers;
		}

		public void ChangeVehicleStatus(string i_LicenseNumber, eVehicleStatus i_NewStatus)
		{
			GarageVehicle requestedVehicle = getVehicleFromGarage(i_LicenseNumber);

			requestedVehicle.Status = i_NewStatus;
		}

		public void InflateVehicleWheelsToMax(string i_LicenseNumber)
		{
			GarageVehicle requestedVehicle = getVehicleFromGarage(i_LicenseNumber);

			requestedVehicle.Vehicle.InflateWheelsToMax();
		}

		public string GetVehicleInfo(string i_LicenseNumber)
		{
			GarageVehicle requestedVehicle = getVehicleFromGarage(i_LicenseNumber);

			return requestedVehicle.ToString();
		}

		public void RefuelVehicle(string i_LicenseNumber, eFuelType i_FuelType, float i_AmountToAdd)
		{
			GarageVehicle requestedVehicle = getVehicleFromGarage(i_LicenseNumber);

			if (requestedVehicle.Vehicle.Engine is FuelEngine fuelEngine)
			{
				fuelEngine.Refuel(i_AmountToAdd, i_FuelType);
			}
			else
			{
				throw new ArgumentException("The vehicle's engine is not a fuel engine.");
			}
		}

		public void RechargeVehicle(string i_LicenseNumber, float i_HoursToAdd)
		{
			GarageVehicle requestedVehicle = getVehicleFromGarage(i_LicenseNumber);

			if (requestedVehicle.Vehicle.Engine is ElectricEngine electricEngine)
			{
				electricEngine.Recharge(i_HoursToAdd);
			}
			else
			{
				throw new ArgumentException("The vehicle's engine is not an electric engine.");
			}
		}
	}
}