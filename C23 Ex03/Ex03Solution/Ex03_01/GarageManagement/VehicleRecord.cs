
/*This class is a wrapper to the "Vehicle" class and also contain
More details about the car and its owner. Those details are only
relevant to this specific garage. Therefore those details deserve a
specific wrapper.*/

using System.Text;

namespace Ex03
{
    public class VehicleRecord
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;
        private AlphaBeitParser m_NameParser = new AlphaBeitParser();

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
            set
            {
                object parsedName;
                if (!m_NameParser.TryParse(value, out parsedName))
                {
                    throw new ArgumentException("Owner name contain only letters");
                }
                else
                {
                    m_OwnerName = parsedName as string;
                }
            }
        }
        
        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhoneNumber;
            }
            set
            {
                if (!int.TryParse(value, out int placeholder))
                {
                    throw new ArgumentException("Owner name contain only numbers");
                }

                m_OwnerPhoneNumber = value;
            }
        }

        public VehicleRecord(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (!m_NameParser.IsParseable(i_OwnerName))
            {
                throw new ArgumentException("A human name could not consist special characters");
            }

            if (!int.TryParse(i_OwnerPhoneNumber, out int placeholder))
            {
                throw new ArgumentException("A phone number could not consist non digits characters");
            }

            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.CurrentlyTreated;
        }
        
        
        public eVehicleStatus Status
        {
             get
             {
                 return m_Vehicle.Status;
             }
            set
            {
                m_Vehicle.Status = value;
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return m_Vehicle.LicenseNumber;
            }
            set
            {
                m_Vehicle.LicenseNumber = value;
            }
        }

        public Vehicle PhysicalContent
        {
            get
            {
                return m_Vehicle;
            }
        }
        
        public override string ToString()
        {
            const string k_Spacer = "\t\t\t";
            int k_ApproximateSize = 500;
            StringBuilder recordDetails = new StringBuilder(k_ApproximateSize);
            recordDetails.Append(string.Format("License Number:{0}{1}{2}", k_Spacer,
                m_Vehicle.LicenseNumber, Environment.NewLine));
            recordDetails.Append(string.Format("Owner Name:{0}{1}{2}", k_Spacer,
                m_OwnerName, Environment.NewLine));
            recordDetails.Append(string.Format("Owner Phone:{0}{1}{2}", k_Spacer,
                m_OwnerPhoneNumber, Environment.NewLine));
            recordDetails.Append(string.Format("Vehicle Model:{0}{1}{2}", k_Spacer,
                PascalCaseToHumanReadable(m_Vehicle.GetType().Name), Environment.NewLine));
            recordDetails.Append(string.Format("Current Status:{0}{1}{2}", k_Spacer,
                PascalCaseToHumanReadable(m_VehicleStatus.ToString()), Environment.NewLine));
            Engine engine = m_Vehicle.Engine;
            
            if (engine is ElectricalEngine)
            {
                recordDetails.Append(string.Format("Battery Charge:{0}{1}/{2}{3}", k_Spacer,
                    engine.CurrentEnergyLevel, engine.EnergyCapacity, Environment.NewLine));
            }
            else
            {
                recordDetails.Append(string.Format("Fuel Level:{0}{1}/{2}{3}", k_Spacer,
                    engine.CurrentEnergyLevel, engine.EnergyCapacity, Environment.NewLine));
                recordDetails.Append(string.Format("Fuel Type:{0}{1}{2}", k_Spacer,
                    (engine as FuelEngine).FuelType, Environment.NewLine));
            }

            int tirePosition = 1;
            
            foreach (Tire tire in m_Vehicle.Tires)
            {
                recordDetails.Append(string.Format("Tire {0}:    {1}{2}",
                    tirePosition++, tire, Environment.NewLine));
            }

            recordDetails.Append(m_Vehicle.UniqueDataMembersToString());

            return recordDetails.ToString();
        }

        protected string PascalCaseToHumanReadable(string i_TextInPascalCase)
        {
            StringBuilder humanReadableText = new StringBuilder(i_TextInPascalCase.Length * 2);

            foreach (char character in i_TextInPascalCase)
            {
                if (!char.IsUpper(character))
                {
                    humanReadableText.Append(" ");
                }

                humanReadableText.Append(character);
            }

            return humanReadableText.ToString().Substring(1).ToLower();
        }
    }
}