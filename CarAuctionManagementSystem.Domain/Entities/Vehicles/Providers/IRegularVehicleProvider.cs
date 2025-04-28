namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers
{
    public interface IRegularVehicleProvider: IVehicleProvider
    {
        public ushort NumberOfDoors { get; }
    }
}
