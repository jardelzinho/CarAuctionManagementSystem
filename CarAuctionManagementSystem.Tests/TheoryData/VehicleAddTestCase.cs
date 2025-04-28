using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Tests.TheoryData
{
    public record VehicleAddTestCase(
        VehicleSpecification Specification,
        Vehicle Vehicle,
        bool ExpectedResult
    );
}
