using CarAuctionManagementSystem.Application.Vehicles.Interfaces;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Application.Vehicles.Factories
{
    public class SedanCreator : IVehicleCreator
    {
        public VehicleTypeEnum VehicleType => VehicleTypeEnum.Sedan;

        public Vehicle Create(VehicleSpecification specification)
        {
            return Sedan.Create((SedanSpecification)specification);
        }
    }
}
