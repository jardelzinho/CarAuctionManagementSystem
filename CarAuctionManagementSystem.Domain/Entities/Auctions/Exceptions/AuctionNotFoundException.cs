using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions
{
    public class AuctionNotFoundException: BaseException
    {
        public AuctionNotFoundException(Guid vehicleId):base(vehicleId)
        {
        }
        
        public override string GetExceptionMessage()
        {
            return $"Auction not found for vehicle ID {VehicleId}";
        }
    }
}
