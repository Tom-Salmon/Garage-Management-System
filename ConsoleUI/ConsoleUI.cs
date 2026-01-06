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
                        consoleUI.refuelVehicle();
                        break;
                    case "7":
                        consoleUI.rechargeVehicle();
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

        private string getEnumSelection(Type i_EnumType, string i_Message)
        {
            if (!i_EnumType.IsEnum)
            {
                throw new ArgumentException("Type must be an enum");
            }
            
            Console.WriteLine(i_Message);
            string[] options = Enum.GetNames(i_EnumType);

            while (true)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine(string.Format("{0}. {1}", i + 1, options[i]));
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= options.Length)
                {
                    return options[choice - 1];
                }

                Console.WriteLine("Invalid selection. Please try again.");
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
                        string licenseNumber = vehicleData[1];
                        string modelName = vehicleData[2];
                        string ownerName = vehicleData[6];
                        string ownerPhone = vehicleData[7];
                        bool isExists = m_GarageManager.AddVehicle(licenseNumber, modelName, vehicleType, ownerName, ownerPhone, out Vehicle newVehicle);

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
                            m_GarageManager.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                            Console.WriteLine($"Vehicle '{licenseNumber}' is already in garage. Status updated to In Repair.");
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
                if (requirement.Value.IsEnum)
                {
                    string requestKeyMessage = $"Please select {requirement.Key}:";
                    string chosenValue = getEnumSelection(requirement.Value, requestKeyMessage);
                    answers.Add(requirement.Key, chosenValue);
                }
                else
                {
                    Console.Write($"{requirement.Key}: ");
                    string userInput = Console.ReadLine();
                    answers.Add(requirement.Key, userInput);
                }
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
                m_GarageManager.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                Console.WriteLine($"Vehicle '{licenseNumber}' is already in garage. Status updated to In Repair.");
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
                string selectedStatus = getEnumSelection(typeof(eVehicleStatus), "Select the vehicle status to filter by:");
                filterStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), selectedStatus);
            }
            else//user chose not to filter
            {
                Console.WriteLine("Displaying all vehicles without filtering.");
            }

            List<string> licenseNumbers = m_GarageManager.GetLicenseNumbers(filterStatus);
            printFoundVehicles(licenseNumbers);
        } 

        private void printFoundVehicles(List<string> i_Licenses)
        {
            if (i_Licenses.Count > 0)
            {
                Console.WriteLine("Vehicles in the garage:");
                foreach (string license in i_Licenses)
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
            eVehicleStatus newStatus = eVehicleStatus.InRepair;

            string selectedStatus = getEnumSelection(typeof(eVehicleStatus), "Select the new vehicle status:");
            newStatus = (eVehicleStatus)Enum.Parse(typeof(eVehicleStatus), selectedStatus);
            try
            {
                m_GarageManager.ChangeVehicleStatus(license, newStatus);
                Console.WriteLine("Vehicle status updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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

        private void refuelVehicle()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();
            string statusInput = getEnumSelection(typeof(eFuelType), "Select the fuel type:");
            eFuelType fuelType = (eFuelType)Enum.Parse(typeof(eFuelType), statusInput);

            Console.WriteLine("Enter the amount of fuel to add:");
            if (float.TryParse(Console.ReadLine(), out float choice) && choice > 0)
            {
                try
                {
                    m_GarageManager.RefuelVehicle(license, fuelType, choice);
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

        private void rechargeVehicle()
        {
            Console.WriteLine("Please enter the license plate:");
            string license = Console.ReadLine();
            Console.WriteLine("Enter the amount of energy to add:");
            string energyAmount = Console.ReadLine();

            if (float.TryParse(energyAmount, out float choice) && choice > 0)
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
                Console.Clear();
                Console.WriteLine(m_GarageManager.GetVehicleInfo(license));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
