using CarAuctionManagementSystem.Domain.Common;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers
{
    public interface IVehicleProvider
    {
        public TypedId Id { get; }

        public string Manufacturer { get; }

        public string Model { get; }

        public ushort Year { get; }

        public Bid StartingBid { get; }

        public VehicleTypeEnum Type { get; }
    }
}
