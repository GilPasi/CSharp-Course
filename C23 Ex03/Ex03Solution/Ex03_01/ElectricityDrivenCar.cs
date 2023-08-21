namespace Ex03
{
    class ElectricityDrivenCar : Vehicle
    {
        public enum eColor
        {
            Black,
            White,
            Red,
            Blue
        }

        public ElectricityDrivenCar()
        {
            m_engine = new ElectricalEngine();
        }
    
        public ElectricityDrivenCar(string i_LicenseNumber) : base(i_LicenseNumber)
        {
        }
        
        public override bool AskForSpecificRequirements()
        {
            //TODO:implement properly
            return true;
        }
    
    }
}
