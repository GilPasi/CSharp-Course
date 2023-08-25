namespace Ex03
{
    public class GarageLogic
    {
        private Dictionary<string, VehicleRecord> m_vehicles = new Dictionary<string, VehicleRecord>();

        public Dictionary<string, VehicleRecord> AllRecords
        {
            get
            {
                return new Dictionary<string, VehicleRecord>(m_vehicles);
            }
        }

        public bool TryGetVehicleRecord(string i_LicenseNumber, out VehicleRecord o_RequestedVehicleRecord)
        {
            return m_vehicles.TryGetValue(i_LicenseNumber, out o_RequestedVehicleRecord);
        }

        public void FillAllWheelsInAVehicle(float i_CommonPressure)
        {
        }
        
        public void FillAllWheelsInAVehicle(float[] i_WheelsPressure)
        {
        }

        public void SignVehicle(VehicleRecord i_VehicleToSign)
        {
            //TODO: implement validation
            m_vehicles.Add(i_VehicleToSign.LicenseNumber,i_VehicleToSign);
        }
    }
}

