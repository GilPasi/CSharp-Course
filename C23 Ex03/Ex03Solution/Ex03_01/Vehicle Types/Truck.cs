namespace Ex03
{
    public class Truck : Vehicle
    {
        public Truck()
        {
            const int k_WheelsQuantitiy = 12;
            m_Engine = new FuelEngine(130, eFuelType.Soler);
            InitiateTires(12, 27);
            
        }

        protected override void intiateUniqueDataMembers()
        {
            m_UniqueDataMembers = new List<PseudoAttribute>(2);
            PseudoAttribute<bool> isCargoRefrigerated =
                new PseudoAttribute<bool>("Cargo is refrigerated",
                    new BoundedIntParser(new int[2]{0, 1}),
                    new List<bool>(new bool[]{false, true})); 
            
            InitiateUFloatDataMember("Cargo Volume");
            UniqueDataMembers.Add(isCargoRefrigerated);
        }
    }
}