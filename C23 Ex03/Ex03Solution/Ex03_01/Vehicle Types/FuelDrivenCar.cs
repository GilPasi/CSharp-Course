namespace Ex03
{
    public class FuelDrivenCar : Car
    {
        public FuelDrivenCar()
        {
            m_Engine = new FuelEngine(44, eFuelType.Octan95);
        }
    }
}