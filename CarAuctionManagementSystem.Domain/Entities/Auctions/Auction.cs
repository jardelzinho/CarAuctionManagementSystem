using CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;

namespace CarAuctionManagementSystem.Domain.Entities.Auctions
{
    public class Auction
    {
        private readonly List<AuctionBid> _bids;

        private Auction(Vehicle vehicle)
        {
            Vehicle = vehicle;
            IsActive = true;
            _bids = [];
        }

        public Vehicle Vehicle { get; }

        public AuctionBid? CurrentBid { get; private set; }

        public bool IsActive { get; private set; } = false;

        public IReadOnlyCollection<AuctionBid> Bids => _bids.AsReadOnly();

        public void PlaceBid(AuctionBid auctionBid)
        {
            if (!IsActive) throw new AuctionNotActiveException(Vehicle.Id.Value);

            var highest = _bids.OrderByDescending(b => b.Bid.Amount).FirstOrDefault();
            if (auctionBid.Bid.Amount < Vehicle.StartingBid.Amount || (highest is not null && (auctionBid.Bid.Amount <= highest.Bid.Amount)))
                throw new AuctionInvalidBidException(Vehicle.Id.Value);

            CurrentBid = auctionBid;
            _bids.Add(auctionBid);
        }

        public void Close() => IsActive = false;

        public static Auction Create(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle, nameof(vehicle));

            var auction = new Auction(vehicle);
            return auction;
        }
    }
}
