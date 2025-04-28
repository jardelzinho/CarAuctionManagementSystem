using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions
{
    public class VehicleNotFoundException : BaseException
    {
        public VehicleNotFoundException(Guid vehicleId): base(vehicleId) 
        { 
        }

        public override string GetExceptionMessage()
        {
            return $"Vehicle with ID {VehicleId} was not found.";
        }
    }
}
