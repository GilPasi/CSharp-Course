
namespace Ex03
{
    public class ElectricityDrivenCar : Car
    {
        public ElectricityDrivenCar()
        {
            m_Engine = new ElectricalEngine(5.2f);
        }
    }
}
