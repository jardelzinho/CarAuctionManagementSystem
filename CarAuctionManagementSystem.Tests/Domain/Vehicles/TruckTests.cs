using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Tests.Domain.Vehicles
{
    public class TruckTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1200)]
        public void Create_WithInvalidLoadCapacity_ThrowsInvalidInputException(double loadCapacity)
        {
            // Arrange
            var vehicleId = TypedId.New();
            var manufacturer = "John Deer";
            var model = "4x4";
            ushort year = 2022;
            var startingBid = Bid.Create("EUR", 5000);
            var expectedMessage = "Load capacity must be positive";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() =>
                Truck.Create(new TruckSpecification(TypedId.New(), manufacturer, model, year, startingBid, loadCapacity)));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
