using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions
{
    public class AuctionNotActiveException : BaseException
    {
        public AuctionNotActiveException(Guid vehicleId) : base(vehicleId)
        {
        }
        
        public override string GetExceptionMessage()
        {
            return $"Auction is not active for vehicle with ID {VehicleId}";
        }
    }
}
