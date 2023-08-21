namespace Ex03
{
    public abstract class Vehicle
    {
        protected string m_licenseNumber = String.Empty;
        protected eVehicleStatus m_status = eVehicleStatus.CurrentlyTreated;
        protected List<Tire> m_tires = null;
        protected Engine m_engine;

        public Vehicle()
        {
            m_engine = new FuelEngine();
        }
        
        public Vehicle(string i_LicenseNumber)
        {
            m_licenseNumber = i_LicenseNumber;
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
            return m_tires.Count;
        }
        
        public void addEnergy(float i_AddedEnergy)
        {
            
        }
        
        public eVehicleStatus Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
            }
        }
        
        public string LicenseNumber
        {
            get
            {
                return m_licenseNumber;
            }
        }
        
        public Engine Engine
        {
            get
            {
                return m_engine;
            }
            set
            {
                if (m_engine == null)
                {
                    m_engine = value;
                }
            }
        }
        
        public List<Tire> Tires
        {
            get
            {
                return m_tires;
            }
            set
            {
                if (m_tires == null)
                {
                    m_tires = value;
                }
            }
        }
    
        public abstract bool AskForSpecificRequirements();
    }
}