namespace Ex03
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Black,
            White,
            Red,
            Blue
        }
        
        public enum eDoorsCount
        {
            TwoDoors,
            ThreeDoors,
            FourDoors,
            FiveDoors,
        }

        public Car()
        {
            InitiateTires(5, 32);
            intiateUniqueDataMembers();
        }
        
        protected override void intiateUniqueDataMembers()
        {
            m_UniqueDataMembers = new List<PseudoAttribute>(2);
            InitiateEnumDataMember<eColor>();
            InitiateEnumDataMember<eDoorsCount>();
        }

        public override string UniqueDataMembersToString()
        {
            const string k_Spacer = "\t \t \t ";
            return string.Format("Color:\t{0}{2}{1}Doors Count:{0}{3}{1}",
                k_Spacer, Environment.NewLine, UniqueDataMembers[0].Value, (int)UniqueDataMembers[1].Value);
        }
    }
}