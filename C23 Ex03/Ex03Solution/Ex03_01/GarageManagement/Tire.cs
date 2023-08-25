namespace Ex03
{
    public class Tire
    {
        private string m_manufacturer;
        private float m_currentAirPressure;
        private float m_maxAirPressure;

        public string Manufacturer
        {
            get
            {
                return m_manufacturer;
            }
        }
        public float CurrentAirPressure
        {
            get
            {
                return m_currentAirPressure;
            }
            set
            {
                //TODO: implement
            }
        }
        public float MaxAirPressure
        {
            get
            {
                return m_maxAirPressure;
            }
        }

        public void Pump(float i_AdditionalPressure)
        {
            if (i_AdditionalPressure > 0 && m_currentAirPressure + i_AdditionalPressure < m_maxAirPressure)
            {
                m_currentAirPressure += i_AdditionalPressure;
            }
        }
    }
}

