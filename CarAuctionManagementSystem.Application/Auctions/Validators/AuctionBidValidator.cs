using CarAuctionManagementSystem.Application.Auctions.Models;

namespace CarAuctionManagementSystem.Application.Auctions.Validators
{
    internal class AuctionBidValidator
    {
        public static bool Validate(AuctionBidRequest request)
        {
            return request != null && request.Bid.Amount > 0;
        }
    }
}
