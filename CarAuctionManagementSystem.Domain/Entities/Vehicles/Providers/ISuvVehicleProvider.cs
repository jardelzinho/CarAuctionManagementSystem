namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers
{
    public interface ISuvVehicleProvider: IVehicleProvider
    {
        public ushort NumberOfSeats { get; }
    }
}
