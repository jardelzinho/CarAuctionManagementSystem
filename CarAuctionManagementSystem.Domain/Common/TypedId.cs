using CarAuctionManagementSystem.Domain.Exceptions;

namespace CarAuctionManagementSystem.Domain.Common
{
    public sealed class TypedId
    {
        public Guid Value { get; }

        private TypedId(Guid value)
        {
            if (value == Guid.Empty)
                throw new InvalidInputException("ID cannot be empty.");

            Value = value;
        }

        public static TypedId New() => new(Guid.NewGuid());

        public static TypedId From(Guid value) => new(value);

        public override string ToString() => Value.ToString();
    }
}
