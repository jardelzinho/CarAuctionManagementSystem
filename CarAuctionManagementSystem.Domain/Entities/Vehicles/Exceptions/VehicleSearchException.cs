namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions
{
    public class VehicleSearchException: Exception
    {
        public override string Message => "Vehicle search failed due to invalid parameters or other issues";
    }
}
