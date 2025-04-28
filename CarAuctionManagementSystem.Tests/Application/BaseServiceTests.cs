using CarAuctionManagementSystem.Application.Auctions.Services;
using CarAuctionManagementSystem.Application.Vehicles.Factories;
using CarAuctionManagementSystem.Application.Vehicles.Services;
using CarAuctionManagementSystem.Domain.Interfaces;
using FakeItEasy;

namespace CarAuctionManagementSystem.Tests.Application
{
    public abstract class BaseServiceTests
    {
        protected ICommandAuctionRepository _commandAuctionRepositoryMock = A.Fake<ICommandAuctionRepository>();
        protected IQueryAuctionRepository _queryAuctionRepositoryMock = A.Fake<IQueryAuctionRepository>();
        protected ICommandVehicleRepository _commandVehicleRepositoryMock = A.Fake<ICommandVehicleRepository>();
        protected IQueryVehicleRepository _queryVehicleRepositoryMock = A.Fake<IQueryVehicleRepository>();

        public AuctionService GetAuctionServiceSut()
        {
            return new AuctionService(_queryAuctionRepositoryMock, _commandAuctionRepositoryMock, _queryVehicleRepositoryMock);
        }

        public VehicleService GetVehicleServiceSut(VehicleFactory vehicleFactory)
        {
            return new VehicleService(_queryVehicleRepositoryMock, _commandVehicleRepositoryMock, vehicleFactory);
        }
    }
}
