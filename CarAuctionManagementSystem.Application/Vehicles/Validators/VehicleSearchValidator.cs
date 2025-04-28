using CarAuctionManagementSystem.Application.Vehicles.Dtos;
using CarAuctionManagementSystem.Domain.Common.Extensions;

namespace CarAuctionManagementSystem.Application.Vehicles.Validators
{
    internal class VehicleSearchValidator
    {
        public static bool Validate(VehicleSearchRequest request)
        {
            var result = request.Year is null || (request.Year.HasValue && request.Year.Value.IsValidGregorianYear());
            return result;
        }
    }
}
