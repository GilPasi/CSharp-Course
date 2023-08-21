namespace Ex03
{
    public class FuelDrivenMotorcycle : Vehicle
    {
        public FuelDrivenMotorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }

        public FuelDrivenMotorcycle()
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