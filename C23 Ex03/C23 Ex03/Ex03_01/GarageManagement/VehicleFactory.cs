namespace Ex03
{
    public class VehicleFactory
    {
        private static readonly Type[] sr_VehiclesTypes =
            { typeof(FuelDrivenMotorcycle), typeof(ElectricityDrivenMotorcycle),
                typeof(FuelDrivenCar), typeof(ElectricityDrivenCar), typeof(Truck)
            };

        private static readonly Type[] sr_EngineTypes = { typeof(FuelEngine), typeof(ElectricalEngine) };

        public static Type[] AllVehicleTypes
        {
            get
            {
                return sr_VehiclesTypes;
            }
        }

        public static Type[] AllEngineTypes
        {
            get
            {
                return sr_EngineTypes;
            }
        }

        public static Vehicle ProduceVehicle(Type i_VehicleType)
        {
            Vehicle producedVehicle = default(Vehicle);

            if (i_VehicleType == typeof(FuelDrivenMotorcycle))
            {
                producedVehicle = new FuelDrivenMotorcycle();
            }
            else if (i_VehicleType == typeof(ElectricityDrivenMotorcycle))
            {
                producedVehicle = new ElectricityDrivenMotorcycle();
            }
            else if (i_VehicleType == typeof(FuelDrivenCar))
            {
                producedVehicle = new FuelDrivenCar();
            }
            else if (i_VehicleType == typeof(ElectricityDrivenCar))
            {
                producedVehicle = new ElectricityDrivenCar();
            }
            else if (i_VehicleType == typeof(Truck))
            {
                producedVehicle = new Truck();
            }
            else if (i_VehicleType.IsAssignableFrom(typeof(Vehicle)))
            {
                //In case that the new vehicle type was not updated here
                producedVehicle = (Vehicle)Activator.CreateInstance(i_VehicleType);
            }
            else
            {
                throw new ArgumentException("The provided type is not a sort of vehicle");
            }
            
            return producedVehicle;
        }
    }
}

