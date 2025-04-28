using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles
{
    public abstract class RegularVehicle : Vehicle, IRegularVehicleProvider
    {
        protected RegularVehicle(RegularSpecification regularSpecification, VehicleTypeEnum type):base(regularSpecification, type)
        {
            ValidateNumberOfDoors(regularSpecification.NumberOfDoors);
            NumberOfDoors = regularSpecification.NumberOfDoors;
        }

        protected virtual void ValidateNumberOfDoors(ushort numberOfDoors)
        {
            if (numberOfDoors < 2 || numberOfDoors > 5)
                throw new InvalidInputException("Vehicle must have between 2 and 5 doors");
        }

        public ushort NumberOfDoors { get; }
    }
}
