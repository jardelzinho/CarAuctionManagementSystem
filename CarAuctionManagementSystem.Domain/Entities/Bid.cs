namespace CarAuctionManagementSystem.Domain.Entities
{
    public sealed class Bid: IEquatable<Bid>
    {
        public decimal Amount => Money is null ? default : Money.Amount;

        public required Money Money { get; init; }

        public DateTime SubmittedOn { get; private set; }

        public static Bid Create(string currency, decimal amount)
        {
            return new Bid 
            { 
                Money = new Money(currency, amount), 
                SubmittedOn = DateTime.UtcNow
            };
        }

        public bool Equals(Bid? other)
        {
            return other is not null &&
                   Money.Equals(other.Money) &&
                   SubmittedOn == other.SubmittedOn;
        }
    }
}
