namespace Ex03
{
    public class FuelEngine : Engine
    {
        private eFuelType? m_FuelType;
    
        public FuelEngine(float i_TankCapacity, eFuelType i_FuelType):base(i_TankCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public eFuelType? FuelType
        {
            get
            {
                return m_FuelType;
            }
            set
            {
                if (m_FuelType == null)
                {
                    m_FuelType = value;
                }
            }
        }

        // public void Refuel(eFuelType i_FuelType, float i_FuelQunatity)
        // {
        //     if (i_FuelType != m_FuelType)
        //     {
        //         string errorMessage = string.Format("Engine can only get {0} as fuel while got {1}" ,m_FuelType, i_FuelType);
        //         throw new ArgumentException(errorMessage);
        //     }
        //     AddEnergy(i_FuelQunatity);
        // }
    }
}


