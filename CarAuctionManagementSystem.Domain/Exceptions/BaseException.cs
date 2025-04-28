namespace CarAuctionManagementSystem.Domain.Exceptions
{
     public abstract class BaseException: Exception
     {
        protected BaseException(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        public Guid VehicleId { get; }

        public abstract string GetExceptionMessage();
    }
}
