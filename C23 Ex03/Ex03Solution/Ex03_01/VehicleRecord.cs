
/*This class is a wrapper to the "Vehicle" class and also contain
More details about the car and its owner. Those details are only
relevant to this specific garage. Therefore those details deserve a
specific wrapper.*/
namespace Ex03
{
    public class VehicleRecord
    {
        private Vehicle m_vehicle;
        private string m_ownerName;
        private string m_ownerPhoneNumber;
        private eVehicleStatus m_vehicleStatus;

        public string OwnerName
        {
            get
            {
                return m_ownerName;
            }
            set
            {
                if (!IOManipulator.IsAlphabeiticalString(value))
                {
                    throw new ArgumentException("Owner name containn only letters");
                }

                m_ownerName = value;
            }
        }
        
        public string OwnerPhone
        {
            get
            {
                return m_ownerName;
            }
            set
            {
                if (!IOManipulator.IsNumericalString(value))
                {
                    throw new ArgumentException("Owner name contain only numbers");
                }

                m_ownerPhoneNumber = value;
            }
        }

        public VehicleRecord(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (!IOManipulator.IsAlphabeiticalString(i_OwnerName))
            {
                throw new ArgumentException("A human name could not consist special characters");
            }

            if (!IOManipulator.IsNumericalString(i_OwnerPhoneNumber))
            {
                throw new ArgumentException("A human name could not consist special characters");
            }

            m_vehicle = i_Vehicle;
            m_ownerName = i_OwnerName;
            m_ownerPhoneNumber = i_OwnerPhoneNumber;
            m_vehicleStatus = eVehicleStatus.CurrentlyTreated;
        }
        
        
        public eVehicleStatus Status
        {
             get
             {
                 return m_vehicle.Status;
             }
            set
            {
                m_vehicle.Status = value;
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return m_vehicle.LicenseNumber;
            }
        }
    }
}