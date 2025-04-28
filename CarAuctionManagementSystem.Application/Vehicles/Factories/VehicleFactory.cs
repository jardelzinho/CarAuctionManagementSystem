using CarAuctionManagementSystem.Application.Vehicles.Interfaces;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Application.Vehicles.Factories
{
    public class VehicleFactory
    {
        private readonly Dictionary<VehicleTypeEnum, IVehicleCreator> _vehicleCreators;

        public VehicleFactory(IEnumerable<IVehicleCreator> vehicleCreators)
        {
            _vehicleCreators = vehicleCreators.ToDictionary(c => c.VehicleType);
        }

        public Vehicle Create(VehicleSpecification request)
        {
            if (!_vehicleCreators.TryGetValue(request.Type, out var creator))
                throw new InvalidInputException($"Unsupported vehicle type: {request.Type}");

            return creator.Create(request);
        }
    }
}
