using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications
{
    public class SuvSpecification : VehicleSpecification, ISuvVehicleProvider
    {
        public SuvSpecification(TypedId id, string manufacturer, string model, ushort year, Bid startingBid, ushort numberOfSeats)
            : base(id, manufacturer, model, year, startingBid, VehicleTypeEnum.Suv)
        {
            NumberOfSeats = numberOfSeats;
        }

        public ushort NumberOfSeats { get; set; }
    }
}
