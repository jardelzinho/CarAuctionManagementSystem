using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications
{
    public abstract class RegularSpecification : VehicleSpecification, IRegularVehicleProvider
    {
        public RegularSpecification(TypedId id, string manufacturer, string model, ushort year, Bid startingBid, ushort numberOfDoors, VehicleTypeEnum type)
            : base(id, manufacturer, model, year, startingBid, type)
        {
            NumberOfDoors = numberOfDoors;
        }

        public ushort NumberOfDoors { get; set; }
    }

    public class SedanSpecification : RegularSpecification
    {
        public SedanSpecification(TypedId id, string manufacturer, string model, ushort year, Bid startingBid, ushort numberOfDoors)
            : base(id, manufacturer, model, year, startingBid, numberOfDoors, VehicleTypeEnum.Sedan )
        {
        }
    }

    public class HatchbackSpecification : RegularSpecification
    {
        public HatchbackSpecification(TypedId id, string manufacturer, string model, ushort year, Bid startingBid, ushort numberOfDoors)
            : base(id, manufacturer, model, year, startingBid, numberOfDoors, VehicleTypeEnum.Hatchback)
        {
        }
    }
}
