using CarAuctionManagementSystem.Domain.Entities.Vehicles;

namespace CarAuctionManagementSystem.Domain.Interfaces
{
    public interface IQueryVehicleRepository
    {
        Vehicle GetById(Guid id);

        IEnumerable<Vehicle> GetAll();

        bool Exists(Guid id);
    }
}
