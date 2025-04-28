namespace CarAuctionManagementSystem.Domain.Entities.Auctions
{
    public record AuctionBid(Bid Bid, string User = "user")
    {
        public decimal Amount => Bid is null ? default : Bid.Amount;
    }
}
