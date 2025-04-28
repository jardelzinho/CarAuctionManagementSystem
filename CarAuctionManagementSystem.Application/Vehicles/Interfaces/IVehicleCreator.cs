using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Application.Vehicles.Interfaces
{
    public interface IVehicleCreator
    {
        VehicleTypeEnum VehicleType { get; }

        Vehicle Create(VehicleSpecification specification);
    }
}
