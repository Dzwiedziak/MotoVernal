using BusinessLogic.DTO.VehicleOfferObservation;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class VehicleOfferObservationService : IVehicleOfferObservationService
    {
        readonly IVehicleOfferObservationRepository _repository;
        readonly IUserRepository _userRepository;
        readonly IVehicleOfferRepository _vehicleOfferRepository;

        public VehicleOfferObservationService(IVehicleOfferObservationRepository repository, IUserRepository userRepository, IVehicleOfferRepository vehicleOfferRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _vehicleOfferRepository = vehicleOfferRepository;
        }

        public Result<int, VehicleOfferObservationErrorCode> Add(AddVehicleOfferObservationDTO entityAddDTO)
        {
            var dbEntity = _repository.GetForUserAndOffer(entityAddDTO.UserId, entityAddDTO.OfferId);
            if(dbEntity != null)
                return VehicleOfferObservationErrorCode.RelationAlreadyExists;

            var dbUser = _userRepository.GetOne(entityAddDTO.UserId);
            if(dbUser == null)
                return VehicleOfferObservationErrorCode.UserNotExists;

            var dbVehicleOffer = _vehicleOfferRepository.GetOne(entityAddDTO.OfferId);
            if(dbVehicleOffer == null)
                return VehicleOfferObservationErrorCode.OfferNotExists;

            var newEntity = new VehicleOfferObservation(0, dbUser, dbVehicleOffer);
            _repository.Add(newEntity);
            return newEntity.Id;
        }

        public VehicleOfferObservationErrorCode? Delete(int id)
        {
            var dbEntitiy = _repository.Get(id);
            if(dbEntitiy == null)
                return VehicleOfferObservationErrorCode.RelationNotExists;
            _repository.Delete(dbEntitiy);
            return null;
        }

        public VehicleOfferObservation? FindByUserAndOffer(string userId, int offerId)
        {
            return _repository.GetForUserAndOffer(userId, offerId);
        }
    }
}
