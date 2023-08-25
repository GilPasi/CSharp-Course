namespace Ex03
{
    public class FuelDrivenMotorcycle : Vehicle
    {
        public FuelDrivenMotorcycle()
        {
            //TODO: implement a unique properties logic
            const int k_WheelsQuantity = 2;
            m_Engine = new FuelEngine();
            m_Tires = new List<Tire>(k_WheelsQuantity);
            
        }
        
        public override void SetDataMembers(object[] i_Values)
        {
            //TODO: implement properly
            throw new NotImplementedException();
        }
    }
}