using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles
{
    public class Truck: Vehicle, ITruckVehicleProvider
    {
        private Truck(TruckSpecification truckSpecification) : base(truckSpecification, VehicleTypeEnum.Truck)
        {
            if(truckSpecification.LoadCapacity <= 0)
                throw new InvalidInputException("Load capacity must be positive");

            LoadCapacity = truckSpecification.LoadCapacity;
        }

        public double LoadCapacity { get; }

        public static Truck Create(TruckSpecification truckSpecification)
        {
            ArgumentNullException.ThrowIfNull(truckSpecification, nameof(TruckSpecification));

            return new Truck(truckSpecification);
        }
    }
}
