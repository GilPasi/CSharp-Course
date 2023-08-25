using System.Drawing;

namespace Ex03
{
    public class ElectricityDrivenCar : Vehicle
    {
        private eColor m_Color;
        private eDoorsCount m_DoorsCount;
        
        
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

        public ElectricityDrivenCar()
        {
            const int k_WheelsQuantitiy = 5;   
            m_Engine = new ElectricalEngine();
            m_Tires = new List<Tire>(k_WheelsQuantitiy);
            m_UniqueDataMembers = new List<DataMember>(2);
            List<eColor> allColors = new List<eColor> ((eColor [])Enum.GetValues(typeof(eColor)));
            int colorQuantity = allColors.Count;
            List<eDoorsCount> allDoorsCounts = new List<eDoorsCount>((eDoorsCount[])Enum.GetValues(typeof(eColor)));
            int allPossibilitiesForDoorsCount = allDoorsCounts.Count;
            BoundedIntParser colorParser = new BoundedIntParser(new int[]{0,colorQuantity});
            int doorsQuantity = Enum.GetNames(typeof(eColor)).Length;
            BoundedIntParser doorsParser = new BoundedIntParser(new int[]{0,doorsQuantity});
            m_UniqueDataMembers.Add(new DataMember<eColor>("color" ,colorParser, allColors));
            m_UniqueDataMembers.Add(new DataMember<eDoorsCount>("doors' count", doorsParser,allDoorsCounts));
        }
        
        public override void SetDataMembers(object[] i_Values)
        {
            m_Color = (eColor)(int)i_Values[0];
            m_DoorsCount = (eDoorsCount)(int)i_Values[1];
        }
        
    }
}
