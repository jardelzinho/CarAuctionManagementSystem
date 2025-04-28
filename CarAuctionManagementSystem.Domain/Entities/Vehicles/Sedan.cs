using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles
{
    public class Sedan: RegularVehicle
    {
        private Sedan(SedanSpecification sedanSpecification) : base(sedanSpecification, VehicleTypeEnum.Sedan)
        {
        }

        public static Sedan Create(SedanSpecification sedanSpecification)
        {
            ArgumentNullException.ThrowIfNull(sedanSpecification, nameof(SedanSpecification));
            return new Sedan(sedanSpecification);
        }
    }
}
