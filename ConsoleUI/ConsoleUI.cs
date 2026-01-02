using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    class ConsoleUI
    {
        GarageManager m_GarageManager = new GarageManager();

        /// <summary>
        /// THE FORMAT IS:
        ///VehicleType, LicensePlate, ModelName, EnergyPercentage, TierModel, CurrAirPressure, OwnerName, OwnerPhone, [SPECIFIC VEHICLE PROPERTIES]
        /// </summary>
        public void loadVehiclesFromDB()
        {
            string[] allFileLines = System.IO.File.ReadAllLines("\"C:\\Users\\tomsa\\Desktop\\C#.NET\\Ex03\\A26 Ex03 TomSalmon 206493694 GiladRaskind 318467743\\VehiclesDB.txt\"");

            foreach (string line in allFileLines)
            {
                string[] vehicleData = line.Split(',');
                string vehicleType = vehicleData[0];
                string licensePlate = vehicleData[1];
                string modelName = vehicleData[2];
                float energyPercentage = float.Parse(vehicleData[3]);
                string tireModel = vehicleData[4];
                float currAirPressure = float.Parse(vehicleData[5]);
                string ownerName = vehicleData[6];
                string ownerPhone = vehicleData[7];
                bool isExists = m_GarageManager.AddVehicle(licensePlate, modelName, vehicleType, ownerName, ownerPhone, out Vehicle newVehicle);

                if (!isExists && newVehicle != null)
                {
                    Dictionary<string, Type> specificRequiredParameters = newVehicle.GetVehicleSpecificParameters();
                    Dictionary<string, object> vehicleParametersAnswer = new Dictionary<string, object>();
                    List<string> specificParametersKeys = specificRequiredParameters.Keys.ToList();

                    vehicleParametersAnswer.Add(specificParametersKeys[0], energyPercentage);
                    vehicleParametersAnswer.Add(specificParametersKeys[1], currAirPressure);
                    vehicleParametersAnswer.Add(specificParametersKeys[2], tireModel);
                    int fileIndex = 8;
                    for(int i = 3; i < specificParametersKeys.Count; i++)
                    {
                        vehicleParametersAnswer.Add(specificParametersKeys[i], vehicleData[fileIndex]);
                        fileIndex++;
                    }
                    newVehicle.SetVehicleSpecificParameters(vehicleParametersAnswer);
                }
                else
                {
                    //continue here.
                }

            }
        }
    }
}
