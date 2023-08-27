using System.Text;

namespace Ex03
{
    public class GarageLogic
    {
        private Dictionary<string, VehicleRecord> m_vehicles = new Dictionary<string, VehicleRecord>();

        /*____Administration methods____*/
        
        //#Queries:
        public Dictionary<string, VehicleRecord> AllRecords
        {
            get
            {
                return new Dictionary<string, VehicleRecord>(m_vehicles);
            }
        }

        public VehicleRecord GetRecord(string i_LicenseNumber)
        {
            return m_vehicles[i_LicenseNumber];
        }

        public bool TryGetVehicleRecord(string i_LicenseNumber, out VehicleRecord o_RequestedVehicleRecord)
        {
            return m_vehicles.TryGetValue(i_LicenseNumber, out o_RequestedVehicleRecord);
        }
        
        //#Revisions:
        public void SignVehicle(VehicleRecord i_VehicleToSign)
        {
            m_vehicles.Add(i_VehicleToSign.LicenseNumber,i_VehicleToSign);
        }
        
        public void UpdateStatus(ref VehicleRecord io_RecordToUpdate, eVehicleStatus i_NewStatus)
        {
            io_RecordToUpdate.Status = i_NewStatus;
        }
        
        //#Issues: 

        public string GetRecordReport(VehicleRecord i_Record)
        {
            const string k_Spacer = "\t\t\t";
            int k_ApproximateSize = 500;
            StringBuilder recordDetails = new StringBuilder(k_ApproximateSize);
            Vehicle vehicle = i_Record.PhysicalContent;
            
            recordDetails.Append(string.Format("License Number:{0}{1}{2}", k_Spacer,
                vehicle.LicenseNumber, Environment.NewLine));
            recordDetails.Append(string.Format("Owner Name:{0}{1}{2}", k_Spacer,
                i_Record.OwnerName, Environment.NewLine));
            recordDetails.Append(string.Format("Owner Phone:{0}{1}{2}", k_Spacer,
                i_Record.OwnerPhone, Environment.NewLine));
            recordDetails.Append(string.Format("Vehicle Model:{0}{1}{2}", k_Spacer,
                IOManipulator.PascalCaseToHumanReadable(vehicle.GetType().Name), Environment.NewLine));
            recordDetails.Append(string.Format("Current Status:{0}{1}{2}", k_Spacer,
                IOManipulator.PascalCaseToHumanReadable(vehicle.Status.ToString()), Environment.NewLine));
            Engine engine = vehicle.Engine;
            string currentEnergy = ((float)engine.CurrentEnergyLevel).ToString("0.0");
            
            if (engine is ElectricalEngine)
            {
                recordDetails.Append(string.Format("Battery Charge:{0}{1}/{2}{3}", k_Spacer,
                    currentEnergy, engine.EnergyCapacity, Environment.NewLine));
            }
            else
            {
                recordDetails.Append(string.Format("Fuel Level:{0}{1}/{2}{3}", k_Spacer,
                    currentEnergy, engine.EnergyCapacity, Environment.NewLine));
                recordDetails.Append(string.Format("Fuel Type:{0}{1}{2}", k_Spacer,
                    (engine as FuelEngine).FuelType, Environment.NewLine));
            }

            int tirePosition = 1;
            
            foreach (Tire tire in vehicle.Tires)
            {
                recordDetails.Append(string.Format("Tire {0}:    {1}{2}",
                    tirePosition++, tire, Environment.NewLine));
            }
            recordDetails.Append(vehicle.UniqueDataMembersToString());

            return recordDetails.ToString();
        }

        public string GetGarageReport(eVehicleStatus? i_Filter)
        {
            const int k_ApproximateRowSize = 20;
            string delimiter = string.Format("================================={0}", Environment.NewLine);
            StringBuilder garageReport = new StringBuilder(k_ApproximateRowSize * m_vehicles.Count);
            int vehicleSerialNumber = 1;
            
            garageReport.Append("Vehicles in the garage currently:");
            garageReport.Append(Environment.NewLine);
            garageReport.Append("No.\tLicense\t Name \tPhone");
            garageReport.Append(Environment.NewLine);
            garageReport.Append(delimiter);

            foreach (KeyValuePair<string, VehicleRecord> coupledRecord in m_vehicles)
            {
                if (i_Filter == null || coupledRecord.Value.Status == i_Filter)
                {
                    VehicleRecord record = coupledRecord.Value;
                    string rowToAdd = string.Format("{0}.\t{1}\t{2}\t{3}\t{4}",
                            vehicleSerialNumber, record.LicenseNumber,
                            record.OwnerName,record.OwnerPhone,Environment.NewLine);
                    garageReport.Append(rowToAdd);
                    garageReport.Append(delimiter);
                }
                
                vehicleSerialNumber++;
            }

            return garageReport.ToString();
        }
        
        /*____Service methods____*/
        //#Energizing:
        public void Refuel(ref FuelEngine io_EngineToFuel, float i_FuelQunatity, eFuelType i_FuelType)
        {
            if (i_FuelType != io_EngineToFuel.FuelType)
            {
                string errorMessage = string.Format("Engine can only get {0} as fuel while got {1}"
                    ,io_EngineToFuel.FuelType, i_FuelType);
                throw new ArgumentException(errorMessage);
            }
            
            Engine abstractEngine = io_EngineToFuel;
            addEnergy(ref abstractEngine, i_FuelQunatity);
        }
        
        public void Recharge(ref ElectricalEngine io_EngineTocharge, float i_BatteryTimeToAdd)
        {
            Engine abstractEngine = io_EngineTocharge;
            addEnergy(ref abstractEngine, i_BatteryTimeToAdd);
        }

        private void addEnergy(ref Engine io_EngineToEnergize, float i_EnergyToAdd)
        {
            
            if (io_EngineToEnergize.CurrentEnergyLevel == null)
            {
                /*It is likely that in this case, the user
                Meant to actually set the energy level rather than
                to add more energy*/
                io_EngineToEnergize.CurrentEnergyLevel = i_EnergyToAdd;
            }
            else
            {
                float maxEnergyToAdd = io_EngineToEnergize.EnergyCapacity - (float)io_EngineToEnergize.CurrentEnergyLevel;
                if (i_EnergyToAdd > maxEnergyToAdd || i_EnergyToAdd < 0 )
                {
                    string erroMessage = string.Format("Additional energy cannot exceed {0} and cannot be negative",
                        maxEnergyToAdd);
                
                    throw new ValueOutOfRangeException(new Exception(erroMessage),0, maxEnergyToAdd);
                }

                io_EngineToEnergize.CurrentEnergyLevel += i_EnergyToAdd;
            }
        }
        
        //#Air Pumping: 
        public void SetTirePressure(ref VehicleRecord io_RecordToPumpItsTires, float i_NewPressure, int i_TirePosition)
        {
            Vehicle vehicleToPumpItsTires = io_RecordToPumpItsTires.PhysicalContent;
            SetTirePressure(ref vehicleToPumpItsTires, i_NewPressure, i_TirePosition);
        }
        
        public void SetTirePressure(ref Vehicle io_VehicleToPumpItsTires, float i_NewPressure, int i_TirePosition)
        {
            io_VehicleToPumpItsTires.Tires[i_TirePosition].CurrentAirPressure = i_NewPressure;
        }
        
        public void SetTiresSamePressure(ref VehicleRecord io_RecordToPumpItsTires, float i_NewPressure)
        {
            Vehicle vehicleToPumpItsTires = io_RecordToPumpItsTires.PhysicalContent;
            SetTiresSamePressure(ref vehicleToPumpItsTires, i_NewPressure);
        }
        
        public void SetTiresSamePressure(ref Vehicle io_VehicleToPumpItsTires, float i_NewPressure)
        {
            for (int i = 0; i < io_VehicleToPumpItsTires.Tires.Count; i++)
            {
                io_VehicleToPumpItsTires.Tires[i].CurrentAirPressure = i_NewPressure;
            }
        }

        public void FullyPumpAll(ref VehicleRecord io_RecordToPumpItsTires)
        {
            Vehicle vehicleToPumpItsTires = io_RecordToPumpItsTires.PhysicalContent;
            FullyPumpAll(ref vehicleToPumpItsTires);
        }
        
        public void FullyPumpAll(ref Vehicle io_VehicleToPumpItsTires)
        {
            foreach (Tire tire in io_VehicleToPumpItsTires.Tires)
            {
                tire.FullyPump();
            }
        }
    }
}

