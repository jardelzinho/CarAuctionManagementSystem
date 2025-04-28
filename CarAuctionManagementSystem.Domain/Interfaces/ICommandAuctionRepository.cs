using CarAuctionManagementSystem.Domain.Entities.Auctions;

namespace CarAuctionManagementSystem.Domain.Interfaces
{
    public interface ICommandAuctionRepository
    {
        bool Add(Auction auction);

        bool Update(Auction auction);
    }
}
