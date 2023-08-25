using System.Data.SqlTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;

namespace Ex03
{
    public class ConsoleUI
    {
        public GarageLogic m_Garage;
        //Questions:
        private LimitedChoiceQuestion<int> m_GarageMenu;
        private OpenQuestion<string> m_LicenseNumberQuestion;
        private LimitedChoiceQuestion<Type> m_VehicleTypeQuestion;
        private OpenQuestion<float> m_FuelLeveleQuestion;
        private OpenQuestion<float> m_ElectricityLevelQuestion;
        private LimitedChoiceQuestion<bool> m_MultiplePumpQuestion;
        private OpenQuestion<float> m_PumpQuntityQuestion;
        private LimitedChoiceQuestion<eVehicleStatus> m_VehicleStatusVehicle;
        private LimitedChoiceQuestion<eVehicleStatus> m_FilterLicenseNumbersQuestion;

        public ConsoleUI()
        {
            m_Garage = new GarageLogic();
            /*The following section is just initialization of all the questions.
             Therefore you can refer here to any question's format albeit the actual
             ask logic is implemented in the Question class and its descendants */
            m_GarageMenu = new LimitedChoiceQuestion<int>(
                new List<int>(new int[8] {1, 2, 3, 4, 5, 6, 7, 8}),
                new List<string>(new string[8]
                {
                    "Add a vehicle to fix", "Show the vehicles", "Change a vehicle status", "Pump wheels",
                    "Refuel a vehicle", "Recharge a vehicle", " Get Information about a vehicle", "Exit garage"
                })
                , "Welcome to the garage! How can I help you? (enter a number corresponding to the request)",
                "This is not a valid option, please enter a number in range 1-9");
            m_LicenseNumberQuestion = new OpenQuestion<string>("Please enter the wanted vehicle's license number: ",
                "The license number could not contain any special characters", new LettersAndDigitsParser());
            
            m_VehicleTypeQuestion = new LimitedChoiceQuestion<Type>(new List<Type>(VehicleFactory.AllVehicleTypes),
                "What kind of a vehicle do you use?",
                "The chosen vehicle was not one of the options");
            m_VehicleTypeQuestion.InferOptions();
            m_FuelLeveleQuestion = new OpenQuestion<float>("Enter the current fuel level(liters)",
                "Your answer is not a number", new FloatParser());
            m_ElectricityLevelQuestion = new OpenQuestion<float>(
                "Enter the current time left before the battery dies(hours)",
                "Your answer is not a number", new FloatParser());
            m_MultiplePumpQuestion = new LimitedChoiceQuestion<bool>(new List<bool>(new bool []{ true, false }),
                "Do you wish to pump all the tires at once?",
                "Your answer is not one of the options");
            m_MultiplePumpQuestion.InferOptions();
            m_PumpQuntityQuestion = new OpenQuestion<float>("Enter the air pressure",
                "The given value is not a number", new FloatParser());
            
            eVehicleStatus [] allStatuses = (eVehicleStatus[])Enum.GetValues(typeof(eVehicleStatus));
            List<eVehicleStatus> filtersList = new List<eVehicleStatus>(allStatuses);
            m_FilterLicenseNumbersQuestion = new LimitedChoiceQuestion<eVehicleStatus>(filtersList,
                    "Select a status:",
                    "The requested status is not one of the options");
            m_FilterLicenseNumbersQuestion.InferOptions();
            m_FilterLicenseNumbersQuestion.EditOption(0,"No filter");

            m_VehicleStatusVehicle = new LimitedChoiceQuestion<eVehicleStatus>(m_FilterLicenseNumbersQuestion);
            m_VehicleStatusVehicle.RemoveOption(0);
        }

        public void PresentOptions()
        {
            int userAnswer;
            bool userIsDone = false;

            do
            {
                userAnswer = m_GarageMenu.AskAndForceInput();

                switch (userAnswer)
                {
                    case 1:
                        SignNewVehicle();
                        break;
                    case 2:
                        ShowVehicles();
                        break;
                    case 3:
                        ChangeVehicleStatus();
                        break;
                    case 4:
                        pumpWheels();
                        break;
                    case 5:
                        refuelVehicle();
                        break;
                    case 6:
                        rechargeVehicle();
                        break;
                    case 7:
                        getInformationAboutAVehicle();
                        break;
                    case 8:
                        Console.WriteLine("Ok bye!");
                        userIsDone = userAnswer == 8;
                        break;
                }
            } while (!userIsDone);
        }

        public void SignNewVehicle()
        {
            string licenseNumber = string.Empty;
            VehicleRecord vehicleRecord;

            try
            {
                licenseNumber = m_LicenseNumberQuestion.AskAndForceInput();
                vehicleRecord = m_Garage.AllRecords[licenseNumber];
                Console.WriteLine("This vehicle is already signed");
                vehicleRecord.Status = eVehicleStatus.CurrentlyTreated;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("What is the owner's first name?");
                string ownerName = IOManipulator.ForceParseableInput<string>(
                    "Owner's name could not contain numbers, spaces or special characters",
                    new AlphaBeitParser()
                );
                Console.WriteLine("What is the owner's phone number?");
                string ownerPhoneNumber = IOManipulator.ForceParseableInput<string>(
                    "Owner's phone could not contain letters, hyphens or special characters",
                    new DigitsParser());
                Vehicle vehicle = addVehicle();
                vehicle.LicenseNumber = licenseNumber;
                VehicleRecord newRecord = new VehicleRecord(vehicle, ownerName, ownerPhoneNumber);
                m_Garage.SignVehicle(newRecord);
            }
        }

        public void ShowVehicles()
        {
            Console.WriteLine("How would you like to filter the shown vehicle?");
            eVehicleStatus statusFilter = m_FilterLicenseNumbersQuestion.AskAndForceInput();

            Console.WriteLine("Vehicles in the garage currently:");
            int vehicleSerialNumber = 1;
            foreach (KeyValuePair<string, VehicleRecord> coupledRecord in m_Garage.AllRecords)
            {
                if (statusFilter == null || coupledRecord.Value.Status == statusFilter)
                {
                Console.WriteLine("{0}.\t{1}",
                    vehicleSerialNumber, coupledRecord.Value.LicenseNumber);
                }
                
                vehicleSerialNumber++;
            }
        }

        private void ChangeVehicleStatus()
        {

            Console.WriteLine("Please enter the wanted vehicle's license number: ");
            string licenseNumber = Console.ReadLine();
            VehicleRecord record;
            bool validRequest = m_Garage.AllRecords.TryGetValue(licenseNumber, out record);
            
            if (!validRequest)
            {
                Console.WriteLine("Sorry, the given license number does not appear in our system");
            }
            else
            {
                Console.WriteLine("Status to change the vehicle to:");
                record.Status = m_VehicleStatusVehicle.AskAndForceInput();
            }
            
        }

        private void pumpWheels()
        {
            string userInput;
            VehicleRecord selectedRecord;
            
            Console.WriteLine("Please enter the vehicle license");
            userInput = Console.ReadLine();
            m_Garage.TryGetVehicleRecord(userInput, out selectedRecord);
            referTiresPressure(selectedRecord.PhysicalContent);
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
            bool validVehicleSigning;

            do
            {
                validVehicleSigning = true;
                try
                {
                    userVehicle = referVehicleType();
                    referCurrentEnergy(userVehicle);
                    referTiresPressure(userVehicle);
                    referUniqueDemands(userVehicle);
                }
                catch (ArgumentException ex)
                {
                    validVehicleSigning = false;
                    Console.WriteLine(ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    validVehicleSigning = false;
                    Console.WriteLine("The given value is not in the range [{0},{1}]", ex.Range[0], ex.Range[1]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong, please rerun the program");
                }
            } while (!validVehicleSigning);

            return userVehicle;
        }

        private Vehicle referVehicleType()
        {
            Type userVehicleType = m_VehicleTypeQuestion.AskAndForceInput();
            return VehicleFactory.ProduceItem<Vehicle>(userVehicleType);
        }

        private void referCurrentEnergy(Vehicle i_Vehicle)
        {
            if (i_Vehicle != null && i_Vehicle.Engine.CurrentEnergyLevel == null)
            {
                float leftTimeForBattery;
                if (i_Vehicle.Engine is ElectricalEngine)
                {
                    leftTimeForBattery = m_ElectricityLevelQuestion.AskAndForceInput();
                }
                else
                {
                    leftTimeForBattery = m_FuelLeveleQuestion.AskAndForceInput();
                }

                i_Vehicle.addEnergy(leftTimeForBattery);
            }
        }

        private void referTiresPressure(Vehicle i_selectedVehicle)
        {
            if (i_selectedVehicle != null && i_selectedVehicle.GetTiresQuantity() == 0)
            {
                bool pumpAllAtOnce = m_MultiplePumpQuestion.AskAndForceInput();

                if (pumpAllAtOnce)
                {
                    Console.WriteLine("The pressure for the tires");
                    float requestedPressure = m_PumpQuntityQuestion.AskAndForceInput();
                    i_selectedVehicle.PumpTiresSamePressure(requestedPressure);
                }
                else
                {
                    for (int i = 0; i < i_selectedVehicle.GetTiresCapacity(); i++)
                    {
                        Console.WriteLine("The pressure for the {0} tire", i);
                        float requestedPressure = m_PumpQuntityQuestion.AskAndForceInput();
                        i_selectedVehicle.PumpTire(requestedPressure, i + 1);
                    }
                }
            }
        }

        private void referUniqueDemands(Vehicle i_Vehicle)
        {
            object[] valuesForUniqueDataMembers = new object[i_Vehicle.UniqueDataMembers.Count];
            int dataMemberPosition = 0;
            
            foreach (DataMember uniquePropery in i_Vehicle.UniqueDataMembers)
            {
                Question<object> dataMemberQuestion;
                string questionWording = string.Format("Enter the value for {0}", uniquePropery.Name);
                string errorMessage = "This value is not valid";
                if(uniquePropery.hasFinitePossibleValues())
                {
                    List<object> possibleValues = new List<object>(uniquePropery.PossibleValues);
                    dataMemberQuestion = new LimitedChoiceQuestion<object>(
                        possibleValues, questionWording, errorMessage);
                    (dataMemberQuestion as LimitedChoiceQuestion<object>).InferOptions();
                }
                else
                {
                    dataMemberQuestion = new OpenQuestion<object>(questionWording, 
                        errorMessage, uniquePropery.Parser);
                }
                
                valuesForUniqueDataMembers[dataMemberPosition++] = dataMemberQuestion.AskAndForceInput();
            }
            
            i_Vehicle.SetDataMembers(valuesForUniqueDataMembers);
        }
    }
}