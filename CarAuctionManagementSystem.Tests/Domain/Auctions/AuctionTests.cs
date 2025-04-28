using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Auctions;
using CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Tests.TheoryData;

namespace CarAuctionManagementSystem.Tests.Domain.Auctions
{
    public class AuctionTests
    {
        [Fact]
        public void Create_WithValidVehicle_ReturnsAuction()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedTruck();

            // Act
            var auction = Auction.Create(vehicle);

            // Assert
            Assert.NotNull(auction);
            Assert.Equal(vehicle.Id.Value, auction.Vehicle.Id.Value);
            Assert.True(auction.IsActive);
            Assert.Empty(auction.Bids);
        }

        [Fact]
        public void Create_WithNullVehicle_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Auction.Create(null!));
        }

        [Theory]
        [InlineData(2000,2500)]
        [InlineData(2000, 2000)]
        public void PlaceBid_WithBidEqualOrGreaterThenStartingBid_AddsToAuctionBidsList(decimal startingBidAmount, decimal bidAmount)
        {
            // Arrange
            var vehicle = Hatchback.Create(new HatchbackSpecification(TypedId.New(), "Toyota", "Camry", 2022, Bid.Create("USD", startingBidAmount), 5));
            var auction = Auction.Create(vehicle);
            var auctionBid = new AuctionBid(Bid.Create("USD", bidAmount));

            // Act
            auction.PlaceBid(auctionBid);

            // Assert
            Assert.NotNull(auction.CurrentBid);
            Assert.Equal(auctionBid.Amount, auction.CurrentBid.Amount);
            Assert.Contains(auctionBid, auction.Bids);
        }

        [Fact]
        public void PlaceBid_WhenAuctionIsClosed_ThrowsAuctionNotActiveException()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedHatchback();
            var auction = Auction.Create(vehicle);
            auction.Close();

            // Act & Assert
            var exception = Assert.Throws<AuctionNotActiveException>(() => auction.PlaceBid(null!));
            Assert.Equal(vehicle.Id.Value, exception.VehicleId);
        }

        [Fact]
        public void PlaceBid_WithBidLowerThanStartingBid_ThrowsAuctionInvalidBidException()
        {
            // Arrange
            var vehicle = Truck.Create(new TruckSpecification(TypedId.New(), "Truck", "Offroad", 2022, Bid.Create("USD", 20000), 5));
            var auction = Auction.Create(vehicle);
            var auctionBid = new AuctionBid(Bid.Create("USD", 15000));

            // Act & Assert
            var exception = Assert.Throws<AuctionInvalidBidException>(() => auction.PlaceBid(auctionBid));
            Assert.Equal(vehicle.Id.Value, exception.VehicleId);
        }

        [Fact]
        public void Close_WhenAuctionIsActive_SetsIsActiveToFalse()
        {
            // Arrange
            var vehicle = VehiclesSeed.SeedTruck();
            var auction = Auction.Create(vehicle);

            // Act
            auction.Close();

            // Assert
            Assert.False(auction.IsActive);
        }
    }
}
