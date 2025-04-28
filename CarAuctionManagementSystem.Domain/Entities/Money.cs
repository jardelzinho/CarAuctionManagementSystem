namespace CarAuctionManagementSystem.Domain.Entities
{
    public sealed record Money(string Currency, decimal Amount) : IEquatable<Money>
    {
    }
}