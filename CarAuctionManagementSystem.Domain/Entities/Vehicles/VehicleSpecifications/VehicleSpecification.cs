using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications
{
    public abstract class VehicleSpecification: IVehicleProvider
    {
        protected VehicleSpecification(TypedId id, string manufacturer, string model, ushort year, Bid startingBid, VehicleTypeEnum type)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
            Type = type;
        }

        public TypedId Id { get; }

        public string Manufacturer { get; }

        public string Model { get; }

        public ushort Year { get; }

        public Bid StartingBid { get; }

        public VehicleTypeEnum Type { get; }
    }
}
