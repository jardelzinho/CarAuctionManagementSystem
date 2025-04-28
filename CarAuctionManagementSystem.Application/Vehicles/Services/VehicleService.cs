using CarAuctionManagementSystem.Application.Vehicles.Factories;
using CarAuctionManagementSystem.Application.Vehicles.Dtos;
using CarAuctionManagementSystem.Application.Vehicles.Validators;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Interfaces;

namespace CarAuctionManagementSystem.Application.Vehicles.Services
{
    public class VehicleService
    {
        private readonly ICommandVehicleRepository _commandVehicleRepository;
        private readonly IQueryVehicleRepository _queryVehicleRepository;
        private readonly VehicleFactory _vehicleFactory;

        public VehicleService(IQueryVehicleRepository queryVehicleRepository, ICommandVehicleRepository commandVehicleRepository, VehicleFactory vehicleFactory)
        {
            _queryVehicleRepository = queryVehicleRepository;
            _commandVehicleRepository = commandVehicleRepository;
            _vehicleFactory = vehicleFactory;
        }

        public bool AddVehicle(VehicleSpecification vehicleSpecification)
        {
            ArgumentNullException.ThrowIfNull(vehicleSpecification, nameof(VehicleSpecification));

            if (_queryVehicleRepository.Exists(vehicleSpecification.Id.Value))
                throw new DuplicateVehicleIdException(vehicleSpecification.Id.Value);

            var vehicle = _vehicleFactory.Create(vehicleSpecification);
            return _commandVehicleRepository.Add(vehicle);
        }

        public IEnumerable<Vehicle> Search(VehicleSearchRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(VehicleSearchRequest));

            if (!VehicleSearchValidator.Validate(request))
                throw new VehicleSearchException();

            var vehicles = _queryVehicleRepository.GetAll();

            vehicles = vehicles
                .Where(v => !request.Type.HasValue || v.Type == request.Type)
                .Where(v => string.IsNullOrEmpty(request.Manufacturer) || v.Manufacturer.Equals(request.Manufacturer, StringComparison.OrdinalIgnoreCase))
                .Where(v => string.IsNullOrEmpty(request.Model) || v.Model.Equals(request.Model, StringComparison.OrdinalIgnoreCase))
                .Where(v => !request.Year.HasValue || v.Year == request.Year.Value);

            return vehicles;
        }
    }
}
