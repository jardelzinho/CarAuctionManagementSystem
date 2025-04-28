using CarAuctionManagementSystem.Application.Auctions.Models;
using CarAuctionManagementSystem.Application.Auctions.Validators;
using CarAuctionManagementSystem.Domain.Entities.Auctions;
using CarAuctionManagementSystem.Domain.Entities.Auctions.Exceptions;
using CarAuctionManagementSystem.Domain.Entities.Vehicles.Exceptions;
using CarAuctionManagementSystem.Domain.Interfaces;

namespace CarAuctionManagementSystem.Application.Auctions.Services
{
    public class AuctionService
    {
        private readonly IQueryAuctionRepository _queryAuctionRepository;
        private readonly ICommandAuctionRepository _commandAuctionRepository;
        private readonly IQueryVehicleRepository _queryVehicleRepository;

        public AuctionService(IQueryAuctionRepository queryAuctionRepository, ICommandAuctionRepository commandAuctionRepository, IQueryVehicleRepository queryVehicleRepository)
        {
            _queryAuctionRepository = queryAuctionRepository;
            _commandAuctionRepository = commandAuctionRepository;
            _queryVehicleRepository = queryVehicleRepository;
        }

        public bool StartAuction(Guid vehicleId)
        {
            if (!_queryVehicleRepository.Exists(vehicleId))
                throw new VehicleNotFoundException(vehicleId);

            if (_queryAuctionRepository.HasActiveAuction(vehicleId))
                throw new AuctionAlreadyActiveException(vehicleId);

            var vehicle = _queryVehicleRepository.GetById(vehicleId);
            var auction = Auction.Create(vehicle);
            return _commandAuctionRepository.Add(auction);
        }

        public void PlaceBid(AuctionBidRequest request)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(AuctionBidRequest));

            if(!AuctionBidValidator.Validate(request))
                throw new AuctionInvalidBidException(request.Id);

            var auction = _queryAuctionRepository.GetByVehicleId(request.Id) ?? throw new AuctionNotFoundException(request.Id);
           
            auction.PlaceBid(request.ToAuctionBid());
            _commandAuctionRepository.Update(auction);
        }

        public void CloseAuction(Guid vehicleId)
        {
            var auction = _queryAuctionRepository.GetByVehicleId(vehicleId);
            if (!auction.IsActive) return;

            auction.Close();
            _commandAuctionRepository.Update(auction);
        }
    }
}
