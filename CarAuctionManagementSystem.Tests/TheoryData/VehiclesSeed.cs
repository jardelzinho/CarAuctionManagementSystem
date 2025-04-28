using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;

namespace CarAuctionManagementSystem.Tests.TheoryData
{
    public class VehiclesSeed
    {
        public static Sedan SeedSedan()
        {
            return Sedan.Create(new SedanSpecification(
                TypedId.New(),
                "Toyota",
                "Camry",
                2022,
                Bid.Create("USD", 20000),
                5));
        }

        public static Hatchback SeedHatchback()
        {
            return Hatchback.Create(new HatchbackSpecification(
                TypedId.New(),
                "Toyota",
                "Camry",
                2022,
                Bid.Create("USD", 20000),
                5));
        }

        public static Truck SeedTruck()
        {
            return Truck.Create(new TruckSpecification(
                TypedId.New(),
                "Truck",
                "Offroad",
                2022,
                Bid.Create("USD", 4343.98M),
                5));
        }

        public static Suv SeedSuv()
        {
            return Suv.Create(new SuvSpecification(
                TypedId.New(),
                "Toyota",
                "RAV4",
                2022,
                Bid.Create("USD", 20000),
                5));
        }
    }
}
