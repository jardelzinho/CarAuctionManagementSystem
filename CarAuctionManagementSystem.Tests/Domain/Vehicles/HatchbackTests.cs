using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Tests.Domain.Vehicles
{
    public class HatchbackTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        public void Create_WithInvalidNumberOfDoors_ThrowsInvalidInputException(ushort numberOfDoors)
        {
            // Arrange
            var vehicleId = TypedId.New();
            var manufacturer = "Toyota";
            var model = "Camry";
            ushort year = 2022;
            var startingBid = Bid.Create("EUR", 5000);
            var expectedMessage = "Vehicle must have between 2 and 5 doors";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() =>
                Hatchback.Create(new HatchbackSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfDoors)));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
