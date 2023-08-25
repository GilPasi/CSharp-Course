namespace Ex03
{
    public abstract class Vehicle
    {
        protected LettersAndDigitsParser m_LicenseNumberParser = new LettersAndDigitsParser();
        protected string m_LicenseNumber = String.Empty;
        protected eVehicleStatus m_Status = eVehicleStatus.CurrentlyTreated;
        protected List<Tire> m_Tires;
        protected Engine m_Engine;
        protected List<DataMember> m_UniqueDataMembers;

        public Vehicle()
        {
            m_Engine = new FuelEngine();
        }
        
        public Vehicle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
        }
        
        
        public float GetMaxEnergy()
        {
            //TODO: Implement properly
            return 0;
        }
        
        public float GetTiresMaxPressure()
        {
            //TODO: Implement properly
            return 0;
        }
        
        public int GetTiresQuantity()
        {
            return m_Tires.Count;
        }
        
        public int GetTiresCapacity()
        {
            return m_Tires.Capacity;
        }
        
        public void addEnergy(float i_AddedEnergy)
        {
            
        }
        
        public eVehicleStatus Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                m_Status = value;
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
            set
            {
                if (! m_LicenseNumberParser.TryParse(value, out object parsedLicenseNumber))
                {
                    throw new FormatException("License number must contain only letters or numbers");
                }
                else
                {
                    m_LicenseNumber = value;
                }
            }
        }
        
        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
            set
            {
                if (m_Engine == null)
                {
                    m_Engine = value;
                }
            }
        }
        
        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
            set
            {
                if (m_Tires == null)
                {
                    m_Tires = value;
                }
            }
        }
        
        public List<DataMember> UniqueDataMembers
        {
            get
            {
                return m_UniqueDataMembers;
            }
        }

        public void PumpTire(float i_AddedPressure, int i_TirePosition)
        {
            //Implement logic
        }
        
        public void PumpTiresSamePressure(float i_AddedPressure)
        {
            //Implement logic
        }

        public abstract void SetDataMembers(object[] i_Values);


    }
}