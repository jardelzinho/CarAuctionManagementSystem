using CarAuctionManagementSystem.Application.Auctions.Models;
using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Auctions;
using CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Tests.TheoryData;
using FakeItEasy;

namespace CarAuctionManagementSystem.Tests.Application
{
    public class AuctionServiceTests : BaseServiceTests
    {
        [Fact]
        public void StartAuction_WhenVehicleDoesNotExist_ThrowsVehicleNotFoundException()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            A.CallTo(() => _queryVehicleRepositoryMock.Exists(A<Guid>.Ignored)).Returns(false);
            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<VehicleNotFoundException>(() => auctionService.StartAuction(vehicleId));
            Assert.Equal(exception.Message, new VehicleNotFoundException(vehicleId).Message);
            A.CallTo(() => _commandAuctionRepositoryMock.Add(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void StartAuction_WhenAuctionAlreadyActive_ThrowsAuctionAlreadyActiveException()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            A.CallTo(() => _queryVehicleRepositoryMock.Exists(A<Guid>.Ignored)).Returns(true);
            A.CallTo(() => _queryAuctionRepositoryMock.HasActiveAuction(A<Guid>.Ignored)).Returns(true);
            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<AuctionAlreadyActiveException>(() => auctionService.StartAuction(vehicleId));
            Assert.Equal(exception.Message, new AuctionAlreadyActiveException(vehicleId).Message);
            A.CallTo(() => _commandAuctionRepositoryMock.Add(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void StartAuction_WhenVehicleExistsAndNoActiveAuction_ReturnsSuccess()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedTruck();

            A.CallTo(() => _queryVehicleRepositoryMock.Exists(A<Guid>.Ignored)).Returns(true);
            A.CallTo(() => _queryVehicleRepositoryMock.GetById(A<Guid>.Ignored)).Returns(vehicle);

            A.CallTo(() => _queryAuctionRepositoryMock.HasActiveAuction(A<Guid>.Ignored)).Returns(false);
            A.CallTo(() => _commandAuctionRepositoryMock.Add(A<Auction>.Ignored)).Returns(true);

            var auctionService = GetAuctionServiceSut();

            // Act
            var result = auctionService.StartAuction(vehicle.Id.Value);

            // Assert
            Assert.True(result);
            A.CallTo(() => _commandAuctionRepositoryMock.Add(A<Auction>.That.Matches(a => a.Vehicle.Id.Value == vehicle.Id.Value))).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData(35000, 35000)]
        [InlineData(35000, 35001)]
        public void PlaceBid_WhenBidIsEqualOrGreaterThanCurrentBid_UpdatesAuctionWithNewBid(decimal startingBidAmount, decimal bidAmount)
        {
            // Arrange
            var vehicle = Hatchback.Create(new HatchbackSpecification(TypedId.New(), manufacturer: "Mercedes", model: "Benz", year: 2000, startingBid: Bid.Create("EUR", startingBidAmount), numberOfDoors: 4));
            var auction = Auction.Create(vehicle);
            var bidRequest = new AuctionBidRequest(auction.Vehicle.Id, Bid.Create("EUR", bidAmount));

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(auction);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).Returns(true);

            var auctionService = GetAuctionServiceSut();

            // Act
            auctionService.PlaceBid(bidRequest);

            // Assert
            Assert.NotNull(auction.CurrentBid);
            Assert.Equal(bidRequest.Bid.Amount, auction.CurrentBid.Amount);
            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void PlaceBid_WhenBidIsLowerThanCurrentBid_ThrowsAuctionInvalidBidException()
        {
            // Arrange
            var vehicle = Hatchback.Create(new HatchbackSpecification(TypedId.New(), manufacturer: "Mercedes", model: "Benz", year: 2000, startingBid: Bid.Create("EUR", 35000), numberOfDoors: 4));
            var auction = Auction.Create(vehicle);
            var bidRequest = new AuctionBidRequest(auction.Vehicle.Id, Bid.Create("EUR", 34999));

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(auction);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).Returns(true);

            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<AuctionInvalidBidException>(() => auctionService.PlaceBid(bidRequest));
            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void PlaceBid_WhenBidAmountIsZero_ThrowsAuctionInvalidBidException()
        {
            // Arrange
            var vehicle = Hatchback.Create(new HatchbackSpecification(TypedId.New(), manufacturer: "Mercedes", model: "Benz", year: 2000, startingBid: Bid.Create("EUR", 35000), numberOfDoors: 4));
            var auction = Auction.Create(vehicle);
            var bidRequest = new AuctionBidRequest(auction.Vehicle.Id, Bid.Create("EUR", default));

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(auction);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).Returns(true);

            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<AuctionInvalidBidException>(() => auctionService.PlaceBid(bidRequest));
            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void PlaceBid_WhenAuctionIsNotFound_ThrowsAuctionNotFoundException()
        {
            // Arrange
            var vehicleId = TypedId.New();
            var bidRequest = new AuctionBidRequest(vehicleId, Bid.Create("EUR", 34999));

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(null!);

            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<AuctionNotFoundException>(() => auctionService.PlaceBid(bidRequest));
            Assert.Equal(exception.Message, new AuctionNotFoundException(vehicleId.Value).Message);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void PlaceBid_WhenAuctionIsClosed_ThrowsAuctionNotActiveException()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedHatchback();
            var auction = Auction.Create(vehicle);
            auction.Close();
            var bidRequest = new AuctionBidRequest(vehicle.Id, Bid.Create("EUR", 34999));

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(auction);

            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<AuctionNotActiveException>(() => auctionService.PlaceBid(bidRequest));
            Assert.Equal(exception.Message, new AuctionNotActiveException(vehicle.Id.Value).Message);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void PlaceBid_WhenBidRequestIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var auctionService = GetAuctionServiceSut();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => auctionService.PlaceBid(null!));
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void CloseAuction_WhenAuctionIsActive_ClosesAuction()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedHatchback();
            var auction = Auction.Create(vehicle);

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(auction);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).Returns(true);

            var auctionService = GetAuctionServiceSut();

            // Act
            auctionService.CloseAuction(auction.Vehicle.Id.Value);

            // Assert
            Assert.False(auction.IsActive);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CloseAuction_WhenAuctionIsNotActive_DoesNothing()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedHatchback();
            var auction = Auction.Create(vehicle);
            auction.Close();

            A.CallTo(() => _queryAuctionRepositoryMock.GetByVehicleId(A<Guid>.Ignored)).Returns(auction);

            var auctionService = GetAuctionServiceSut();

            // Act
            auctionService.CloseAuction(auction.Vehicle.Id.Value);

            // Assert
            Assert.False(auction.IsActive);
            A.CallTo(() => _commandAuctionRepositoryMock.Update(A<Auction>.Ignored)).MustNotHaveHappened();
        }
    }
}
