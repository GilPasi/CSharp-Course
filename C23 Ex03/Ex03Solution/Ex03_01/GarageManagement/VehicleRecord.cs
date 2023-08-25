
/*This class is a wrapper to the "Vehicle" class and also contain
More details about the car and its owner. Those details are only
relevant to this specific garage. Therefore those details deserve a
specific wrapper.*/
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
                return m_OwnerName;
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
    }
}