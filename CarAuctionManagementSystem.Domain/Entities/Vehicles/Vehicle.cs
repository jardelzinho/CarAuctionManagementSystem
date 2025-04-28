using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Common.Extensions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Providers;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using CarAuctionManagementSystem.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace CarAuctionManagementSystem.Domain.Entities.Vehicles
{
    public abstract class Vehicle : IVehicleProvider
    {
        protected Vehicle(VehicleSpecification vehicleSpecification, VehicleTypeEnum type)
        {
            if (vehicleSpecification.Id is null)
                throw new InvalidInputException("Vehicle Id is required");
            if (string.IsNullOrWhiteSpace(vehicleSpecification.Manufacturer))
                throw new InvalidInputException("Manufacturer is required");
            if (string.IsNullOrWhiteSpace(vehicleSpecification.Model))
                throw new InvalidInputException("Model is required");
            if (!vehicleSpecification.Year.IsValidGregorianYear())
                throw new InvalidInputException("Year is invalid");
            if (vehicleSpecification.StartingBid is null)
                throw new InvalidInputException("Starting Bid is required");

            Id = vehicleSpecification.Id;
            Manufacturer = vehicleSpecification.Manufacturer;
            Model = vehicleSpecification.Model;
            Year = vehicleSpecification.Year;
            StartingBid = vehicleSpecification.StartingBid;
            Type = type;
        }

        [DisallowNull]
        public TypedId Id { get; }

        [DisallowNull]
        public string Manufacturer { get; }

        [DisallowNull]
        public string Model { get;  }

        public ushort Year { get; }

        [DisallowNull]
        public Bid StartingBid { get; }

        public VehicleTypeEnum Type { get; }
    }
}
