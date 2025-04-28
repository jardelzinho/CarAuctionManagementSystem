using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Tests.Dummies
{
    internal class DummyVehicleSpecification: VehicleSpecification
    {
        public DummyVehicleSpecification()
            : base(TypedId.New(), "manufacturer", "model", 2025, Bid.Create("USD", 1), VehicleTypeEnum.Suv)
        {
        }
    }
}
