using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class ConsoleUI
    {
        GarageManager m_GarageManager;

        public ConsoleUI()
        {
            m_GarageManager = new GarageManager();
        }

        public static void MainMenu()
        {
            ConsoleUI consoleUI = new ConsoleUI();
            bool exitRequested = false;
            while (!exitRequested)
            {
                Console.WriteLine("\nGarage Management System - Main Menu");
                Console.WriteLine("1. Load vehicles from database");
                Console.WriteLine("2. Add New Vehicle");
                Console.WriteLine("3. Display Vehicle License Plates");
                Console.WriteLine("4. Change Vehicle Status");
                Console.WriteLine("5. Inflate Vehicle Wheels to Max");
                Console.WriteLine("6. Refuel Vehicle");
                Console.WriteLine("7. Recharge Vehicle");
                Console.WriteLine("8. Display Full Vehicle Details");
                Console.WriteLine("9. Exit");
                Console.Write("Please select an option (1-9): ");
                string userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        consoleUI.loadVehiclesFromDB();
                        break;
                    case "2":
                        consoleUI.addNewVehicle();
                        break;
                    case "3":
                        consoleUI.printGarageVehiclesLicensePlate();
                        break;
                    case "4":
                        consoleUI.changeVehicleStatus();
                        break;
                    case "5":
                        consoleUI.inflateAllWheelsToMax();
                        break;
                    case "6":
                        consoleUI.RefuelVehicle();
                        break;
                    case "7":
                        consoleUI.RechargeVehicle();
                        break;
                    case "8":
                        consoleUI.printFullVehicleDetails();
                        break;
                    case "9":
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please choose a number from 1 to 9.");
                        break;
                }
            }
        }

        private void loadVehiclesFromDB()
        {
            try
            {
                string filePath = @"C:\Users\tomsa\Desktop\C#.NET\Ex03\A26 Ex03 TomSalmon 206493694 GiladRaskind 318467743\VehiclesDB.txt";
                string[] allFileLines = File.ReadAllLines(filePath);

                foreach (string line in allFileLines)
                {
                    try
                    {
                        string[] vehicleData = line.Split(',');
                        string vehicleType = vehicleData[0];
                        string licensePlate = vehicleData[1];
                        string modelName = vehicleData[2];
                        string ownerName = vehicleData[6];
                        string ownerPhone = vehicleData[7];
                        bool isExists = m_GarageManager.AddVehicle(licensePlate, modelName, vehicleType, ownerName, ownerPhone, out Vehicle newVehicle);

                        if (!isExists && newVehicle != null)
                        {
                            Dictionary<string, Type> specificRequiredParameters = newVehicle.GetVehicleSpecificParameters();
                            Dictionary<string, object> vehicleParametersAnswer = new Dictionary<string, object>();
                            List<string> specificParametersKeys = specificRequiredParameters.Keys.ToList();

                            vehicleParametersAnswer.Add("Energy Percentage", vehicleData[3].Trim());
                            vehicleParametersAnswer.Add("Tire Model", vehicleData[4].Trim());
                            vehicleParametersAnswer.Add("Current Air Pressure", vehicleData[5].Trim());

                            int fileIndex = 8;
                            for (int i = 3; i < specificParametersKeys.Count; i++)
                            {
                                if (fileIndex < vehicleData.Length)
                                {
                                    vehicleParametersAnswer.Add(specificParametersKeys[i], vehicleData[fileIndex]);
                                    fileIndex++;
                                }
                            }

                            newVehicle.SetVehicleSpecificParameters(vehicleParametersAnswer);
                        }
                        else//Vehicle already exists
                        {
                            m_GarageManager.ChangeVehicleStatus(licensePlate, eVehicleStatus.InRepair);
                            Console.WriteLine($"Vehicle '{licensePlate}' is already in garage. Status updated to In Repair.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading vehicle: {ex.Message}");
                    }
                }
                Console.WriteLine("Data loading completed.");
            }
            catch (IOException)
            {
                Console.WriteLine("Error: Could not access the file.");
            }
        }

        private void handleSpecificParameters(Vehicle i_NewVehicle)
        {
            Dictionary<string, Type> requirements = i_NewVehicle.GetVehicleSpecificParameters();
            Dictionary<string, object> answers = new Dictionary<string, object>();

            Console.WriteLine("\nPlease provide the following specific details:");
            foreach (KeyValuePair<string, Type> requirement in requirements)
            {
                Console.Write($"{requirement.Key}: ");
                if (requirement.Value.IsEnum)
                {
                    string options = string.Join(", ", Enum.GetNames(requirement.Value));
                    Console.Write($"({options}) ");
                }

                string userInput = Console.ReadLine();
                answers.Add(requirement.Key, userInput);
            }

            i_NewVehicle.SetVehicleSpecificParameters(answers);
        }

        private void addNewVehicle()
        {
            Console.WriteLine("Please insert the vehicle license number:");
            string licenseNumber = Console.ReadLine();
            bool isExists = m_GarageManager.IsVehicleInGarage(licenseNumber);

            if (isExists)
            {
                Console.WriteLine("Vehicle already exists in the garage. Updating status to In Repair.");
                m_GarageManager.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
            }
            else//Create a new vehicle
            {
                try
                {
                    Console.WriteLine("Please insert the Model name of the vehicle:");
                    string vehicleModel = Console.ReadLine();
                    Console.WriteLine("Please insert your name:");
                    string userName = Console.ReadLine();
                    Console.WriteLine("Please insert your phone number:");
                    string userPhoneNumber = Console.ReadLine();
                    Console.WriteLine("Please enter the vehicle type you want to insert:");
                    string vehicleType = Console.ReadLine();
                    m_GarageManager.AddVehicle(licenseNumber, vehicleModel, vehicleType, userName, userPhoneNumber, out Vehicle newVehicle);
                    if (newVehicle != null)
                    {
                        handleSpecificParameters(newVehicle);
                        Console.WriteLine("Vehicle added successfully.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void printGarageVehiclesLicensePlate()
        {
            Console.WriteLine("Do you want to filter by vehicle status? (yes/no)");
            string filterResponse = Console.ReadLine().ToLower();
            eVehicleStatus? filterStatus = null;
            if (filterResponse == "yes")
            {
                Console.WriteLine("Select the status to filter by:");
                string[] statusOptions = Enum.GetNames(typeof(eVehicleStatus));
                for (int i = 0; i < statusOptions.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {statusOptions[i]}");
                }

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= statusOptions.Length)
                {
                    filterStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), statusOptions[choice - 1]);
                }
                else
                {
                    Console.WriteLine("Invalid selection. No filter will be applied.");
                }
            }

            List<string> licenseNumbers = m_GarageManager.GetLicenseNumbers(filterStatus);
            if (licenseNumbers.Count > 0)
            {
                Console.WriteLine("Vehicles in the garage:");
                foreach (string license in licenseNumbers)
                {
                    Console.WriteLine(license);
                }
            }
            else
            {
                Console.WriteLine("No vehicles found.");
            }
        }

        private void changeVehicleStatus()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();

            Console.WriteLine("Select the new status:");
            string[] statusOptions = Enum.GetNames(typeof(eVehicleStatus));
            for (int i = 0; i < statusOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {statusOptions[i]}");
            }

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= statusOptions.Length)
            {
                try
                {
                    eVehicleStatus newStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), statusOptions[choice - 1]);
                    m_GarageManager.ChangeVehicleStatus(license, newStatus);
                    Console.WriteLine("Vehicle status updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose a number from the list.");
            }
        }

        private void inflateAllWheelsToMax()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();
            try
            {
                m_GarageManager.InflateVehicleWheelsToMax(license);
                Console.WriteLine("All wheels inflated to maximum successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void RefuelVehicle()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();
            Console.WriteLine("Select the fuel type:");
            string[] statusOptions = Enum.GetNames(typeof(eFuelType));
            for (int i = 0; i < statusOptions.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {statusOptions[i]}");
            }

            Console.WriteLine("Enter the amount of fuel to add:");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1)
            {
                try
                {
                    eFuelType fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), statusOptions[choice - 1]);
                    float amountToAdd = float.Parse(Console.ReadLine());
                    m_GarageManager.RefuelVehicle(license, fuelType, amountToAdd);
                    Console.WriteLine("Vehicle refueled successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose a number from the list.");
            }
        }

        private void RechargeVehicle()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();
            Console.WriteLine("Enter the amount of energy to add:");
            string energyAmount = Console.ReadLine();
            if (float.TryParse(energyAmount, out float choice) && choice >= 1)
            {
                try
                {
                    float amountToAdd = choice;
                    m_GarageManager.RechargeVehicle(license, amountToAdd);
                    Console.WriteLine("Vehicle recharged successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose a number from the list.");
            }
        }

        private void printFullVehicleDetails()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();
            try
            {
                Console.WriteLine(m_GarageManager.GetVehicleInfo(license));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
