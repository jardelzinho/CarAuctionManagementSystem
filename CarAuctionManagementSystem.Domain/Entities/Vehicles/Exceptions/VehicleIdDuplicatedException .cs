using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions
{
    public class DuplicateVehicleIdException : BaseException
    {
        public DuplicateVehicleIdException(Guid vehicleId):base(vehicleId)
        {
        }

        public override string GetExceptionMessage()
        {
            return $"Vehicle with ID {VehicleId} already exists.";
        }
    }
}
