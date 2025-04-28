using CarAuctionManagementSystem.Domain.Entities.Auctions;

namespace CarAuctionManagementSystem.Domain.Interfaces
{
    public interface IQueryAuctionRepository
    {
        IEnumerable<Auction> GetAll();

        Auction GetByVehicleId(Guid vehicleId);

        bool HasActiveAuction(Guid vehicleId);
    }
}
