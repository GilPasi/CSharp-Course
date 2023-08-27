using System.Text;

namespace Ex03
{
    public class ConsoleUI
    {
        private GarageLogic m_Garage;
        //Questions:
        private LimitedChoiceQuestion<int> m_GarageMenu;
        private OpenQuestion<string> m_LicenseNumberQuestion;
        private LimitedChoiceQuestion<Type> m_VehicleTypeQuestion;
        private LimitedChoiceQuestion<eFuelType> m_FuelTypeQuestion;
        private OpenQuestion<float> m_FuelLeveleQuestion;
        private OpenQuestion<float> m_ElectricityLevelQuestion;
        private LimitedChoiceQuestion<bool> m_MultiplePumpQuestion;
        private OpenQuestion<float> m_PumpQuntityQuestion;
        private LimitedChoiceQuestion<eVehicleStatus> m_VehicleStatusVehicle;
        private LimitedChoiceQuestion<eVehicleStatus?> m_FilterLicenseNumbersQuestion;

        public ConsoleUI()
        {
            m_Garage = new GarageLogic();
            eVehicleStatus[] allStatuses = (eVehicleStatus[])Enum.GetValues(typeof(eVehicleStatus));
            eVehicleStatus?[] allStatusFilters = ConvertToNullableArray(allStatuses);
            eFuelType[] allFuelTypes = (eFuelType[])Enum.GetValues(typeof(eFuelType));
            /*The following section is just initialization of all the questions.
             Therefore you can refer here to any question's format albeit the actual
             ask logic is implemented in the Question class and its descendants */
            m_GarageMenu = new LimitedChoiceQuestion<int>(
                new List<int>(new int[7] {1, 2, 3, 4, 5, 6, 7}),
                new List<string>(new string[7]
                {
                    "Add a vehicle to fix", "Show the vehicles", "Change a vehicle status", "Pump wheels",
                    "Refuel/recharge a vehicle", "Get Information about a vehicle", "Exit garage"
                })
                , "Welcome to the garage! How can I help you? (enter a number corresponding to the request)",
                "This is not a valid option, please enter a number in range 1-8");
            m_LicenseNumberQuestion = new OpenQuestion<string>("Please enter the wanted vehicle's license number: ",
                "The license number could not contain any special characters", new LettersAndDigitsParser());
            m_FuelTypeQuestion = new LimitedChoiceQuestion<eFuelType>(new List<eFuelType>(allFuelTypes), 
                "Please enter the requested fuel type", "The chosen fuel is not one of the fuel types");
            m_VehicleTypeQuestion = new LimitedChoiceQuestion<Type>(new List<Type>(VehicleFactory.AllVehicleTypes),
                "What kind of a vehicle do you use?",
                "The chosen vehicle was not one of the options");
            m_FuelLeveleQuestion = new OpenQuestion<float>("Enter the requested fuel level(liters)",
                "Your answer is not a number", new FloatParser());
            m_ElectricityLevelQuestion = new OpenQuestion<float>(
                "Enter the requested time of charge (minutes)",
                "Your answer is not a number", new FloatParser());
            m_MultiplePumpQuestion = new LimitedChoiceQuestion<bool>(new List<bool>(new bool []{ true, false }),
                "Do you wish to set all the tires at once?",
                "Your answer is not one of the options");
            m_PumpQuntityQuestion = new OpenQuestion<float>("Enter the air pressure",
                "The given value is not a number", new FloatParser());
            m_VehicleStatusVehicle = new LimitedChoiceQuestion<eVehicleStatus>(new List<eVehicleStatus>(allStatuses), 
                "Select a status:","The requested status is not one of the options");
            m_FilterLicenseNumbersQuestion = new LimitedChoiceQuestion<eVehicleStatus?>(new List<eVehicleStatus?>(allStatusFilters), 
                "Select a status:","The requested status is not one of the options");
            
            m_VehicleTypeQuestion.InferOptions();
            m_MultiplePumpQuestion.InferOptions();
            m_FilterLicenseNumbersQuestion.InferOptions();
            m_VehicleStatusVehicle.InferOptions();
            m_FuelTypeQuestion.InferOptions();
            m_FilterLicenseNumbersQuestion.AddOption(null, "No filter");
        }
        
        private T?[] ConvertToNullableArray<T>(T[] i_NonNullableArray) where T : struct
        {
            T?[] nullableArray = new T?[i_NonNullableArray.Length];
            for (int i = 0; i < i_NonNullableArray.Length; i++)
            {
                nullableArray[i] = i_NonNullableArray[i];
            }
            return nullableArray;
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
                        ViewGarageReport();
                        break;
                    case 3:
                        ChangeVehicleStatus();
                        break;
                    case 4:
                        PumpWheels();
                        break;
                    case 5:
                        RefillEnergy();
                        break;
                    case 6:
                        GetInformationAboutAVehicle();
                        break;
                    case 7:
                        Console.WriteLine("Ok bye!");
                        userIsDone = true;
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
                vehicleRecord = m_Garage.GetRecord(licenseNumber);
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
                Console.WriteLine("Alright! The vehicle was signed successfully!");
                Console.ReadLine();
            }
        }

        public void ViewGarageReport()
        {
            Console.WriteLine("How would you like to filter the shown vehicle?");
            eVehicleStatus? statusFilter = m_FilterLicenseNumbersQuestion.AskAndForceInput();

            string report = m_Garage.GetGarageReport(statusFilter);
            Console.WriteLine(report);
            Console.ReadLine();
        }

        public void ChangeVehicleStatus()
        {
            try
            {
                if (makeQueryVehicle(out VehicleRecord record))
                {
                    Console.WriteLine("Status to change the vehicle to:");
                    eVehicleStatus updatedStatus = m_VehicleStatusVehicle.AskAndForceInput();
                    m_Garage.UpdateStatus(ref record, updatedStatus);
                }

            }
            catch (FormatException)
            {
                Console.WriteLine(m_LicenseNumberQuestion.ErrorMessage);
            }
        }

        public void PumpWheels()
        {
            if (makeQueryVehicle(out VehicleRecord vehicleRecord))
            {
                m_Garage.FullyPumpAll(ref vehicleRecord);
                Console.WriteLine("Alright! all of your tires are fully pumped!");
            }
            else
            {
                Console.WriteLine("Sorry Your vehicle can not be found on our system");
            }
        }

        public void RefillEnergy()
        {
            Engine engine = null;
            try
            {
                string selectedLicenseNumber = m_LicenseNumberQuestion.AskAndForceInput();
                VehicleRecord vehicleRecord = m_Garage.GetRecord(selectedLicenseNumber);
                Vehicle vehicleToRecharge = vehicleRecord.PhysicalContent;
                engine = vehicleRecord.PhysicalContent.Engine;

                if (engine is ElectricalEngine)
                {
                    rechargeVehicle(ref vehicleToRecharge);
                }
                else
                {
                    refuelVehicle(ref vehicleToRecharge);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                if (ex.Range[1] == 0)
                {
                    Console.WriteLine("The engine is full, try another action on the menu");
                }
                else if (engine is ElectricalEngine)
                {
                    Console.WriteLine("The charge value must be a positive number lesser than {0}", ex.Range[1]);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The fuel quantity must be a positive number lesser than {0}", ex.Range[1]);
                    Console.ReadLine();
                }
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine("Unfortunately the mention vehicle is not signed, pleas sign it using option 1 on the menu");
            }
        }

        private void refuelVehicle(ref Vehicle io_Vehicle)
        {
            FuelEngine engineToRefuel = (FuelEngine)io_Vehicle.Engine;
            eFuelType fuelType = m_FuelTypeQuestion.AskAndForceInput();
            float leftEnergy = m_FuelLeveleQuestion.AskAndForceInput();
            
            m_Garage.Refuel(ref engineToRefuel, leftEnergy, fuelType);
        }

        private void rechargeVehicle(ref Vehicle io_VehicleToRecharge)
        {
            ElectricalEngine engineToRecharge;
            float leftEnergy;
            const float k_HourFactor = 60;

            leftEnergy = m_ElectricityLevelQuestion.AskAndForceInput();
            engineToRecharge = (ElectricalEngine)io_VehicleToRecharge.Engine;
            try
            {
                m_Garage.Recharge(ref engineToRecharge, leftEnergy / k_HourFactor);
            }
            catch (ValueOutOfRangeException ex)
            {
                //Catch and rethrow in order to convert hours' range to minutes' range
                float[] valuesRange = ex.Range;
                throw new ValueOutOfRangeException(new Exception(),
                    valuesRange[0] * k_HourFactor, valuesRange[1] * k_HourFactor);
            }
        }

        public void GetInformationAboutAVehicle()
        {
            VehicleRecord record;
            if (makeQueryVehicle(out record))
            {
                string report = m_Garage.GetRecordReport(record);
                Console.WriteLine(report);
                Console.ReadLine();
            }
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
                    referVehicleType(ref userVehicle);
                    referCurrentEnergy(ref userVehicle);
                    referTiresPressure(ref userVehicle);
                    referUniqueDemands(ref userVehicle);
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

        private void referVehicleType(ref Vehicle io_Vehicle)
        {
            if (io_Vehicle == null)
            {
                Type userVehicleType = m_VehicleTypeQuestion.AskAndForceInput();
                io_Vehicle = VehicleFactory.ProduceVehicle(userVehicleType);
            }
        }

        private void referCurrentEnergy(ref Vehicle io_Vehicle )
        {
            if (io_Vehicle.Engine.CurrentEnergyLevel == null)
            {
                if (io_Vehicle.Engine is ElectricalEngine)
                {
                    rechargeVehicle(ref io_Vehicle);
                }
                else
                {
                    refuelVehicle(ref io_Vehicle);
                }
            }
        }

        private void referTiresPressure(ref Vehicle io_SelectedVehicle)
        {
            if (!io_SelectedVehicle.CheckIfAllTiresAreSet())
            {
                bool setAllAtOnce = m_MultiplePumpQuestion.AskAndForceInput();

                if (setAllAtOnce)
                {
                    Console.WriteLine("The pressure for the tires");
                    float requestedPressure = m_PumpQuntityQuestion.AskAndForceInput();
                    m_Garage.SetTiresSamePressure(ref io_SelectedVehicle, requestedPressure);
                }
                else
                {
                    for (int i = 0; i < io_SelectedVehicle.GetTiresQuantity(); i++)
                    {
                        Console.WriteLine("The pressure for the {0} tire", i + 1);
                        float requestedPressure = m_PumpQuntityQuestion.AskAndForceInput();
                        m_Garage.SetTirePressure(ref io_SelectedVehicle, requestedPressure, i);
                    }
                }
            }
        }

        private void referUniqueDemands(ref Vehicle io_Vehicle)
        {
            object[] valuesForUniqueDataMembers = new object[io_Vehicle.UniqueDataMembers.Count];
            bool allPropertiesFilled = true;
            int dataMemberPosition = 0;

            foreach (PseudoAttribute uniquePropery in io_Vehicle.UniqueDataMembers)
            {
                Question<object> dataMemberQuestion = createQuestionFromUniqueDataMember(uniquePropery);
                valuesForUniqueDataMembers[dataMemberPosition++] = dataMemberQuestion.AskAndForceInput();
            }
            
            io_Vehicle.SetUniqueDataMembersValues(valuesForUniqueDataMembers);
        }

        private Question<object> createQuestionFromUniqueDataMember(PseudoAttribute i_UniqueDataMember)
        {
            Question<object> dataMemberQuestion;
            string questionWording = string.Format("Enter the value for {0}", i_UniqueDataMember.Name);
            string errorMessage = "This value is not valid";
            
            if(i_UniqueDataMember.hasFinitePossibleValues())
            {
                List<object> possibleValues = new List<object>(i_UniqueDataMember.PossibleValues);
                dataMemberQuestion = new LimitedChoiceQuestion<object>(
                    possibleValues, questionWording, errorMessage);
                (dataMemberQuestion as LimitedChoiceQuestion<object>).InferOptions();
            }
            else
            {
                dataMemberQuestion = new OpenQuestion<object>(questionWording, 
                    errorMessage, i_UniqueDataMember.Parser);
            }

            return dataMemberQuestion;
        }

        private bool makeQueryVehicle(out VehicleRecord o_Vehicle)
        {
            o_Vehicle = null;
            bool recordFound;
            string selectedLicenseNumber = m_LicenseNumberQuestion.AskAndForceInput();
            recordFound = m_Garage.TryGetVehicleRecord(selectedLicenseNumber, out o_Vehicle);

            if (!recordFound)
            {
                Console.WriteLine("Sorry the requested vehicle does not appear on the system");
                Console.ReadLine();
            }

            return recordFound;
        }
    }
}