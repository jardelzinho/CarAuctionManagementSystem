using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Tests.Domain.Vehicles
{
    public class SuvTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(9)]
        public void Create_WithInvalidNumberOfSeats_ThrowsInvalidInputException(ushort numberOfSeats)
        {
            // Arrange
            var vehicleId = TypedId.New();
            var manufacturer = "John Deer";
            var model = "4x4";
            ushort year = 2022;
            var startingBid = Bid.Create("EUR", 5000);
            var expectedMessage = "SUV must have at least 2 seats and maximum 8";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() => 
                Suv.Create(new SuvSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfSeats)));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
