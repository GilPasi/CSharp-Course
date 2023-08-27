using Microsoft.VisualBasic;

namespace Ex03
{
    public class Tire
    {
        private string m_Manufacturer = "Unknown";
        private float? m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Tire(float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }
        }
        
        public float? CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if (value > MaxAirPressure || value < 0)
                {
                    string errorMessage = Strings.Format(
                        "Pressure value must be a non-negative value that is lesser than the max value-{0}", 
                        m_MaxAirPressure.ToString());
                    throw new ValueOutOfRangeException(new Exception(errorMessage), 0, MaxAirPressure);
                }
                
                m_CurrentAirPressure = value;
            }
        }
        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public void Pump(float i_PressureToAdd)
        {
            float pressureToSet = i_PressureToAdd + (float)m_CurrentAirPressure;

            if (pressureToSet > MaxAirPressure)
            {
                float maxPressureToAdd = MaxAirPressure - (float)CurrentAirPressure;
                string errorMessage = string.Format("A tire cannot be pumped more than its maximum value. Try a value in lesser than{0}"
                    ,maxPressureToAdd);
                throw new ValueOutOfRangeException(new Exception(errorMessage), 0, maxPressureToAdd);
            }
        }

        public void FullyPump()
        {
            m_CurrentAirPressure = m_MaxAirPressure;
        }
        
        public override string ToString()
        {
             string returnedString  = string.Format("Manufacturer:  {0}  Air Pressure: {1}/{2}",
                 m_Manufacturer, CurrentAirPressure, MaxAirPressure);

             return returnedString;
        }
    }
}

