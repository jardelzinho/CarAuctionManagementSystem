using CarAuctionManagementSystem.Application.Vehicles.Factories;
using CarAuctionManagementSystem.Application.Vehicles.Interfaces;
using CarAuctionManagementSystem.Application.Vehicles.Dtos;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Tests.Dummies;
using CarAuctionManagementSystem.Tests.TheoryData;
using FakeItEasy;

namespace CarAuctionManagementSystem.Tests.Application
{
    public class VehicleServiceTests : BaseServiceTests
    {
        [Fact]
        public void AddVehicle_WhenRequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange  
            var vehicleService = GetVehicleServiceSut(null!);

            // Act & Assert  
            Assert.Throws<ArgumentNullException>(() => vehicleService.AddVehicle(null!));
        }

        [Fact]
        public void AddVehicle_WhenVehicleAlreadyExists_ThrowsDuplicateVehicleIdException()
        {
            // Arrange
            A.CallTo(() => _queryVehicleRepositoryMock.Exists(A<Guid>.Ignored)).Returns(true);
            var dummyVehicleSpecification = new DummyVehicleSpecification();
            var vehicleService = GetVehicleServiceSut(null!);

            // Act & Assert  
            var exception = Assert.Throws<DuplicateVehicleIdException>(() => vehicleService.AddVehicle(dummyVehicleSpecification));
            Assert.Equal(exception.Message, new DuplicateVehicleIdException(dummyVehicleSpecification.Id.Value).Message);
            A.CallTo(() => _commandVehicleRepositoryMock.Add(A<Vehicle>.Ignored)).MustNotHaveHappened();
        }

        [Theory]
        [ClassData(typeof(VehicleAddTheoryData))]
        public void AddVehicle_WhenRequestIsValid_AddsVehicleToInventory(VehicleAddTestCase testCase)
        {
            // Arrange
            IVehicleCreator vehicleCreator = A.Fake<IVehicleCreator>();
           
            A.CallTo(() => _queryVehicleRepositoryMock.Exists(A<Guid>.Ignored)).Returns(false);
            A.CallTo(() => vehicleCreator.Create(A<VehicleSpecification>.Ignored)).Returns(testCase.Vehicle);
            A.CallTo(() => vehicleCreator.VehicleType).Returns(testCase.Specification.Type);
            A.CallTo(() => _commandVehicleRepositoryMock.Add(A<Vehicle>.Ignored)).Returns(testCase.ExpectedResult);

            var _vehicleFactory = new VehicleFactory([vehicleCreator]);
            var vehicleService = GetVehicleServiceSut(_vehicleFactory);

            // Act
            var result = vehicleService.AddVehicle(testCase.Specification);

            // Assert
            Assert.Equal(testCase.ExpectedResult, result);
            A.CallTo(() => _commandVehicleRepositoryMock.Add(A<Vehicle>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Search_WhenRequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var vehicleService = GetVehicleServiceSut(null!);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => vehicleService.Search(null!));
            A.CallTo(() => _queryVehicleRepositoryMock.GetAll()).MustNotHaveHappened();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(22323)]
        public void Search_WhenRequestIsInvalid_ThrowsVehicleSearchException(ushort year)
        {
            // Arrange
            var request = new VehicleSearchRequest() { Year = year };
            var vehicleService = GetVehicleServiceSut(null!);

            // Act & Assert
            Assert.Throws<VehicleSearchException>(() => vehicleService.Search(request));
            A.CallTo(() => _queryVehicleRepositoryMock.GetAll()).MustNotHaveHappened();
        }

        [Theory]
        [ClassData(typeof(VehicleSearchTheoryData))]
        public void Search_WhenRequestIsValid_ReturnsExpectedResults(VehicleSearchTestCase testCase)
        {
            // Arrange
            A.CallTo(() => _queryVehicleRepositoryMock.GetAll()).Returns(testCase.Vehicles);

            var vehicleService = GetVehicleServiceSut(null!);

            // Act
            var result = vehicleService.Search(testCase.Criteria);

            // Assert
            Assert.Equal(testCase.ExpectedResults, result);
            A.CallTo(() => _queryVehicleRepositoryMock.GetAll()).MustHaveHappenedOnceExactly();
        }
    }
}
