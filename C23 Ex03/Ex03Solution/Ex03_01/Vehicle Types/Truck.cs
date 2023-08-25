namespace Ex03
{
    public class Truck : Vehicle
    {
        public Truck()
        {
            //TODO: implement a unique properties logic
            const int k_WheelsQuantitiy = 12;
            m_Engine = new FuelEngine();
            m_Tires = new List<Tire>(k_WheelsQuantitiy);
        }

        public override void SetDataMembers(object[] i_Values)
        {
            //TODO: implement properly
            throw new NotImplementedException();
        }
    }
}