namespace Ex03
{
    public class ElectricityDrivenMotorcycle : Motorcycle
    {
        public ElectricityDrivenMotorcycle()
        {
            m_Engine = new ElectricalEngine(2.4f);
        }
    }
}

