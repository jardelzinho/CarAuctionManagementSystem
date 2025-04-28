using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions
{
    public class AuctionAlreadyActiveException : BaseException
    {
        public AuctionAlreadyActiveException(Guid vehicleId):base(vehicleId)
        {
        }

        public override string GetExceptionMessage()
        {
            return $"An auction is already active for vehicle with ID {VehicleId}";
        }
    }
}
