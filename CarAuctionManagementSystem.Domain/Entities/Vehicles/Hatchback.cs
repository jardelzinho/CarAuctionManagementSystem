using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles
{
    public class Hatchback : RegularVehicle
    {
        private Hatchback(HatchbackSpecification hatchbackSpecification) : base(hatchbackSpecification, VehicleTypeEnum.Hatchback)
        {
        }

        public static Hatchback Create(HatchbackSpecification hatchbackSpecification)
        {
            ArgumentNullException.ThrowIfNull(hatchbackSpecification, nameof(HatchbackSpecification));
            return new Hatchback(hatchbackSpecification);
        }
    }
}
