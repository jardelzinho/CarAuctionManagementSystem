using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles
{
    public class Suv : Vehicle, ISuvVehicleProvider
    {
        private Suv(SuvSpecification suvSpecification) : base(suvSpecification, VehicleTypeEnum.Suv)
        {
            if (suvSpecification.NumberOfSeats < 2 || suvSpecification.NumberOfSeats > 8)
                throw new InvalidInputException("SUV must have at least 2 seats and maximum 8");

            NumberOfSeats = suvSpecification.NumberOfSeats;
        }

        public ushort NumberOfSeats { get; }

        public static Suv Create(SuvSpecification suvSpecification)
        {
            ArgumentNullException.ThrowIfNull(suvSpecification, nameof(SuvSpecification));

            return new Suv(suvSpecification);
        }
    }
}
