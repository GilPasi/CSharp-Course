namespace Ex03
{
    public class Truck : Vehicle
    {
        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }

        public Truck()
        {
            m_engine = new FuelEngine();
        }

        public override bool AskForSpecificRequirements()
        {
            //TODO:implement properly
            return true;
        }
    }
}