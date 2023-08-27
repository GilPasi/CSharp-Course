namespace Ex03
{
    public class FuelDrivenMotorcycle : Motorcycle
    {
        public FuelDrivenMotorcycle()
        {
            m_Engine = new FuelEngine(6.2f, eFuelType.Octan98);
        }
    }
}