using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications
{
    public class TruckSpecification : VehicleSpecification, ITruckVehicleProvider
    {
        public TruckSpecification(TypedId id, string manufacturer, string model, ushort year, Bid startingBid, double loadCapacity)
            : base(id, manufacturer, model, year, startingBid, VehicleTypeEnum.Truck)
        {
            LoadCapacity = loadCapacity;
        }

        public double LoadCapacity { get; set; }
    }
}
