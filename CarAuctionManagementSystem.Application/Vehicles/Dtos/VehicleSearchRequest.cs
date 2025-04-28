using CarAuctionManagementSystem.Domain.Entities.Vehicles;

namespace CarAuctionManagementSystem.Application.Vehicles.Dtos
{
    public record VehicleSearchRequest
    {
        public VehicleTypeEnum? Type { get; init; }  

        public string? Manufacturer { get; init; }

        public string? Model { get; init; }

        public ushort? Year { get; init; }
    }
}
