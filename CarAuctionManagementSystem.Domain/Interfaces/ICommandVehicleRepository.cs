using CarAuctionManagementSystem.Domain.Entities.Vehicles;

namespace CarAuctionManagementSystem.Domain.Interfaces
{
    public interface ICommandVehicleRepository
    {
        bool Add(Vehicle car);
    }
}
