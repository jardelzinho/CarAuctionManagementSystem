using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions
{
    public class AuctionInvalidBidException : BaseException
    {
        public AuctionInvalidBidException(Guid vehicleId) : base(vehicleId)
        {
        }

        public override string GetExceptionMessage()
        {
            return $"Bid must be higher than current one for vehicle with ID {VehicleId}";
        }
    }
}
