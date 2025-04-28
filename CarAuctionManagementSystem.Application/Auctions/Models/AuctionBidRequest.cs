using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Auctions;

namespace CarAuctionManagementSystem.Application.Auctions.Models
{
    public record AuctionBidRequest(TypedId VehicleId, Bid Bid, string Customer = "Customer")
    {
        public Guid Id => VehicleId.Value;

        public AuctionBid ToAuctionBid()
        {
            return new AuctionBid(Bid, Customer);
        }
    }
}
