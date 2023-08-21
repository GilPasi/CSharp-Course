using System.Text;
using System.Text.RegularExpressions;

namespace Ex03
{
    public class ConsoleUI
    {
        public GarageLogic m_garage;
        const int k_BufferSize = 70;
        private StringBuilder m_errorMessages = new StringBuilder(k_BufferSize);
        

        public ConsoleUI()
        {
            m_garage = new GarageLogic();
        }

        public void PresentOptions()
        {
            string userAnswer;
            string reformattedAnswer;
            bool userIsDone = false;
            
            Console.WriteLine(
                "Welcome to the garage! How can I help you? (enter a number corresponding to the request)");
            do
            {
                Console.WriteLine(
                    @"1.Add a vehicle to fix
                    2. Show all vehicle numbers
                    3. Change a vehicle status
                    4. Pump wheels
                    5. Refuel a vehicle
                    6. Recharge a vehicle
                    7. Get Information about a vehicle"
                );
                
                userAnswer = Console.ReadLine();
                reformattedAnswer = IOManipulator.ConvertToUniformFormat(userAnswer);
                switch (reformattedAnswer)
                {
                    case "1":
                        SignNewVehicle();
                        break;
                    case "2":
                        showVehicles();
                        break;
                    case "3":
                        changeVehicleStatus();
                        break;
                    case "4":
                        pumpWheels();
                        break;
                    case "5":
                        refuelVehicle();
                        break;
                    case "6":
                        rechargeVehicle();
                        break;
                    case "7":
                        getInformationAboutAVehicle();
                        break;
                    case "Q":
                        Console.WriteLine("Ok bye!");
                        userIsDone = reformattedAnswer == "Q";
                        break;
                    default:
                        Console.WriteLine("This is not a valid option, please enter" +
                                          " a number in range 1-7 or 'Q' for quitting");
                        break;
                }
            } while (!userIsDone);
        }

        public void SignNewVehicle()
        {
            string licenseNumber;
            VehicleRecord vehicleRecord;
            
            Console.WriteLine("Please enter the wanted vehicle's license number: ");
            licenseNumber = Console.ReadLine();

            if (IOManipulator.ContainsSpecialCharacters(licenseNumber))
            {
                Console.WriteLine("The license number could not contain any special characters");
            }
            else
            {
                if (m_garage.TryGetVehicleRecord(licenseNumber, out vehicleRecord))
                {
                    Console.WriteLine("This vehicle is already signed");
                    vehicleRecord.Status = eVehicleStatus.CurrentlyTreated;
                }
                else
                {
                    Console.WriteLine("What is the owner's first name?");
                    string ownerName = IOManipulator.ForceAlphabeiticalInput(
                        "Owner's name could not contain numbers, spaces or special characters");
                    string ownerPhoneNumber = IOManipulator.ForceNumericalInput(
                        "Owner's phone could not contain letters, hyphens or special characters");
                    
                    VehicleRecord newRecord = new VehicleRecord(addVehicle(), ownerName, ownerPhoneNumber);
                    m_garage.SignVehicle(newRecord);
                }
            }
        }
        
        private void showVehicles()
        {
            m_errorMessages.Clear();
            eVehicleStatus statusFilter;
            eVehicleStatus[] filters = (eVehicleStatus[])Enum.GetValues(typeof(eVehicleStatus));
            
            referMutipleChoiceItem(ref statusFilter,filters,
                "Which status would you like to view?","The requested filter is not one of the options" );
            
            Console.WriteLine("Vehicles in the garage currently:");
            int vehicleSerialNumber = 1;
            foreach (KeyValuePair<string,VehicleRecord> coupledRecord in m_garage.AllRecords)
            {
                Console.WriteLine("{0}.\t{1}\t{2}\t{3}\t{4}",
                    vehicleSerialNumber++, coupledRecord.Value.OwnerName,
                    coupledRecord.Key, coupledRecord.Value.LicenseNumber );
            }
        }
       
        private void changeVehicleStatus()
        {
            
        }

        private void pumpWheels()
        {
            
        }

        private void refuelVehicle()
        {
        }
        
        private void rechargeVehicle()
        {
        }

        private void getInformationAboutAVehicle()
        {
        }

        private Vehicle addVehicle()
        {
            Vehicle userVehicle = null;
            Type[] vehicleOptions = VehicleFactory.AllVehicleTypes;

            do
            {
                m_errorMessages.Clear();
                referMutipleChoiceItem<Vehicle>(ref userVehicle, vehicleOptions, "What kind of a vehicle do you use?",
                    "The chosen vehicle was not one of the options");
                referCurrentEnergy(userVehicle);
                referTiresPressure(userVehicle);
                Console.WriteLine(m_errorMessages.ToString());
            } while(m_errorMessages.ToString() != string.Empty);

            return userVehicle;
        }
        
        private bool managedTryParse( out float o_ParsedValue, string i_RequestForNumber, string i_ErrorText)
        {
            bool successfulParse;
            
            Console.WriteLine(i_RequestForNumber);
            successfulParse = float.TryParse(Console.ReadLine(), out o_ParsedValue);
            if (!successfulParse)
            {
                m_errorMessages.Append(i_ErrorText);
            }

            return successfulParse;
        }
        private void referCurrentEnergy( Vehicle i_Vehicle)
        {
            if (i_Vehicle != null && i_Vehicle.Engine.CurrentEnergyLevel == null)
            {
                float leftEnergy;
                string requestMessage = 
                    i_Vehicle.Engine is ElectricalEngine?
                    "Enter the current time left before the battery dies(hours)":
                    "Enter the current fuel level(liters)";
                string errorText = "The energy value is not a number";
                
                managedTryParse(out leftEnergy, requestMessage, errorText);
                try
                {
                    i_Vehicle.addEnergy(leftEnergy);
                }
                catch (ValueOutOfRangeException ex)
                {
                    m_errorMessages.Append(ex.Message);
                }
            }
        }

        private bool askAboutTiresCommonAirPressure()
        {
            Console.WriteLine("Tires air pressure:{0} do you want to enter the same air pressure for all " +
                              "tires?(Y for Yes or any other key for no)", Environment.NewLine);         
            string userInput = Console.ReadLine();
            return userInput.ToUpper() == "Y";
        }

        private void  referTiresPressure( Vehicle i_selectedVehicle)
        {
            if(i_selectedVehicle != null && i_selectedVehicle.Tires == null)
            {
                if (askAboutTiresCommonAirPressure())
                {
                    float leftPressure;
               
                    if (askTirePressure(out leftPressure))
                    {
                        m_garage.FillAllWheelsInAVehicle(leftPressure);
                    }
                }
                else
                {
                    float[] tiresPressure = new float[i_selectedVehicle.GetTiresQuantity()];
                    for (int i = 0; i < tiresPressure.Length; i++)
                    {
                        Console.Write(" {0}.", i);
                        askTirePressure(out tiresPressure[i]);
                    }
                    m_garage.FillAllWheelsInAVehicle(tiresPressure);
                }
            }
        }
        
        private bool askTirePressure(out float o_TirePressure)
        {
            string requestMessage =  "Enter the requested pressure" ;
            string errorText = "One of the wheels' pressure is not a number";
            return managedTryParse(out o_TirePressure, requestMessage, errorText);

        }

        private void referMutipleChoiceItem<T>(ref T io_Item, Type[] i_Options, string i_Question, string i_errorMessage)
        {
            if (io_Item == null)
            {
                int currentOptionPosition = 0 ;
                string[] optionsAsText = new string[i_Options.Length];
                
                foreach (Type itemType in i_Options)
                {
                    optionsAsText[currentOptionPosition++] =  IOManipulator.PascalCaseToHumanReadable(itemType.Name);
                }
                
                string optionsText = createOptionsText(optionsAsText,i_Question);
                if (!tryGetChoiceFromOptions(out Type chosenType, optionsText, i_Options))
                {
                    m_errorMessages.Append(i_errorMessage);
                }
                else
                {
                    io_Item = VehicleFactory.ProduceItem<T>(chosenType);
                }
            }
        }
        
        private string createOptionsText(string[] i_OptionsAsText,string i_Question)
        {
            const int k_AverageOptionSize = 10;
            string questionString = string.Format("{0}{1}", i_Question, Environment.NewLine);
            StringBuilder optionsText = new StringBuilder(questionString
                ,k_AverageOptionSize * i_OptionsAsText.Length +questionString.Length);
            int optionCounter = 1;
            
            foreach (Type vehicleType in VehicleFactory.AllVehicleTypes)
            {
                optionsText.Append(string.Format("{0}. {1}{2}",
                    optionCounter++, vehicleType.Name,Environment.NewLine));
            }

            return optionsText.ToString();
        }
        
        private static bool tryGetChoiceFromOptions<T>(out T o_ResultedItem,string i_InstructiveMessage, T[] i_Options)
        {
            string userInput;
            bool success = false;
            o_ResultedItem = default(T);
            int optionNumber = 1;
            
            Console.WriteLine(i_InstructiveMessage);
            userInput = IOManipulator.ConvertToUniformFormat(Console.ReadLine());
            foreach (T option in i_Options)
            {
                success = userInput == optionNumber++.ToString();
                if (success)
                {
                    o_ResultedItem = option;
                    break;
                }
            }
            
            return success;
        }
    }
}
