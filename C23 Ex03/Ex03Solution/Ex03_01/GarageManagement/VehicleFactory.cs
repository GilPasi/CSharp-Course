namespace Ex03
{
    public class VehicleFactory
    {
        private static readonly Type[] sr_vehiclesTypes =
            { typeof(ElectricityDrivenCar), typeof(FuelDrivenMotorcycle), typeof(Truck) };

        private static readonly Type[] sr_engineTypes = { typeof(FuelEngine), typeof(ElectricalEngine) };

        public static Type[] AllVehicleTypes
        {
            get { return sr_vehiclesTypes; }
        }

        public static Type[] AllEngineTypes
        {
            get { return sr_engineTypes; }
        }

        public static T ProduceItem<T>(Type i_ItemType)
        {
            T producedItem = default(T);
            if (sr_vehiclesTypes.Contains(i_ItemType))
            {
                producedItem = (T)Activator.CreateInstance(i_ItemType);
            }

            return producedItem;
        }
    }
}

