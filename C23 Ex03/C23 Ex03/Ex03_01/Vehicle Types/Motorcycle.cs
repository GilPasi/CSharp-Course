namespace Ex03
{
    public class Motorcycle : Vehicle
    {
        enum eLicenseType
        {
            A,
            A1,
            A2,
            AB
        }

        public Motorcycle()
        {
            InitiateTires(2, 30);
        }
        
        protected override void intiateUniqueDataMembers()
        {
            m_UniqueDataMembers = new List<PseudoAttribute>(2);
            InitiateEnumDataMember<eLicenseType>();
            InitiateUFloatDataMember("Engine Volume");
        }
    }
}