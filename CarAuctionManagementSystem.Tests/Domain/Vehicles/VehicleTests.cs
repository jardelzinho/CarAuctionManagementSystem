using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Tests.Domain.Vehicles
{
    public class VehicleTests
    {
        [Fact]
        public void Create_WithValidInputs_ReturnsSedan()
        {
            // Arrange
            var vehicleId = TypedId.New();
            var manufacturer = "Toyota";
            var model = "Camry";
            ushort year = 2022;
            ushort numberOfDoors = 4;
            var startingBid = Bid.Create("USD", 20000);

            // Act
            var sedan = Sedan.Create(new SedanSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfDoors));

            // Assert
            Assert.NotNull(sedan);
            Assert.Equal(vehicleId, sedan.Id);
            Assert.Equal(manufacturer, sedan.Manufacturer);
            Assert.Equal(model, sedan.Model);
            Assert.Equal(year, sedan.Year);
            Assert.Equal(numberOfDoors, sedan.NumberOfDoors);
            Assert.Equal(startingBid, sedan.StartingBid);
            Assert.Equal(VehicleTypeEnum.Sedan, sedan.Type);
        }

        [Fact]
        public void Create_WithNullVehicleId_ThrowsInvalidInputException()
        {
            // Arrange
            TypedId vehicleId = null!;
            var manufacturer = "Toyota";
            var model = "Camry";
            ushort year = 2022;
            ushort numberOfDoors = 4;
            var startingBid = Bid.Create("EUR", 5000);
            var expectedMessage = "Vehicle Id is required";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() =>
                Hatchback.Create(new HatchbackSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfDoors)));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Create_WithNullManufacturer_ThrowsInvalidInputException()
        {
            // Arrange
            var vehicleId = TypedId.New();
            string manufacturer = null!;
            var model = "Camry";
            ushort year = 2022;
            ushort numberOfDoors = 4;
            var startingBid = Bid.Create("EUR", 5000);
            var expectedMessage = "Manufacturer is required";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() =>
                Sedan.Create(new SedanSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfDoors)));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Create_WithNullModel_ThrowsInvalidInputException()
        {
            // Arrange
            var vehicleId = TypedId.New();
            var manufacturer = "Toyota";
            string model = null!;
            ushort year = 2022;
            ushort numberOfDoors = 4;
            var startingBid = Bid.Create("EUR", 5000);
            var expectedMessage = "Model is required";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() =>
                Sedan.Create(new SedanSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfDoors)));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Create_WithNullStartingBid_ThrowsInvalidInputException()
        {
            // Arrange
            var vehicleId = TypedId.New();
            var manufacturer = "Toyota";
            var model = "Camry";
            ushort year = 2022;
            ushort numberOfDoors = 4;
            Bid startingBid = null!;
            var expectedMessage = "Starting Bid is required";

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() =>
               Sedan.Create(new SedanSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfDoors)));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}
