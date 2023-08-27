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
                m_FuelType ??= value;
            }
        }
    }
}


