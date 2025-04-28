using CarAuctionManagementSystem.Application.Vehicles.Dtos;
using CarAuctionManagementSystem.Domain.Common;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Entities.Vehicles;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.VehicleSpecifications;
using System.Collections;

namespace CarAuctionManagementSystem.Tests.TheoryData
{
    public class VehicleAddTheoryData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var vehicleId = TypedId.New();
            var manufacturer = "Toyota";
            var model = "RAV4";
            ushort year = 2021;
            ushort numberOfSeats = 5;
            var startingBid = Bid.Create("EUR", 5000);

            var specification = new SuvSpecification(vehicleId, manufacturer, model, year, startingBid, numberOfSeats);
            var vehicle = Suv.Create(specification);

            yield return new object[]
            {
                    new VehicleAddTestCase(
                        specification,
                        vehicle,
                        true
                    )
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class VehicleSearchTheoryData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var vehicle1 = Suv.Create( new SuvSpecification(TypedId.New(), manufacturer: "Toyota", model:"RAV4", year: 2021, startingBid: Bid.Create("EUR", 5000), numberOfSeats: 5));
            var vehicle2 = Hatchback.Create( new HatchbackSpecification(TypedId.New(), manufacturer: "Honda", model:"Civic", year: 2020, Bid.Create("EUR", 4000), numberOfDoors: 4));
            var vehicle3 = Suv.Create(new SuvSpecification(TypedId.New(), manufacturer: "Ford", model: "Explorer", year:2021, Bid.Create("EUR", 2500.10M), numberOfSeats: 7));

            var allVehicles = new List<Vehicle> { vehicle1, vehicle2, vehicle3 };

            yield return new object[]
            {
                    new VehicleSearchTestCase(
                        new VehicleSearchRequest { Type = VehicleTypeEnum.Suv, Manufacturer = "Toyota", Model = "RAV4", Year = 2021 },
                        allVehicles,
                        [vehicle1]
                    )
            };

            yield return new object[]
            {
                    new VehicleSearchTestCase(
                        new VehicleSearchRequest { Type = VehicleTypeEnum.Suv, Manufacturer = "Ford" },
                        allVehicles,
                        [vehicle3]
                    )
            };

            yield return new object[]
            {
                    new VehicleSearchTestCase(
                        new VehicleSearchRequest { Type = VehicleTypeEnum.Truck, Manufacturer = "Chevrolet" },
                        allVehicles,
                        []
                    )
            };

            yield return new object[]
           {
                    new VehicleSearchTestCase(
                        new VehicleSearchRequest { Type = VehicleTypeEnum.Suv, Model = "RAV4" },
                        allVehicles,
                        [vehicle1]
                    )
           };

            yield return new object[]
            {
                    new VehicleSearchTestCase(
                        new VehicleSearchRequest(),
                        allVehicles,
                        allVehicles
                    )
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
