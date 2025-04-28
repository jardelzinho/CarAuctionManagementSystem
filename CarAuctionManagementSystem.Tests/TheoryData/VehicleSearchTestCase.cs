using CarAuctionManagementSystem.Application.Vehicles.Dtos;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;

namespace CarAuctionManagementSystem.Tests.TheoryData
{
    public record VehicleSearchTestCase(
      VehicleSearchRequest Criteria,
      List<Vehicle> Vehicles,
      List<Vehicle> ExpectedResults
   );
}
