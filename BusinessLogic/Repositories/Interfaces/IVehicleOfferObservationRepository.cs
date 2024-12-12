using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IVehicleOfferObservationRepository
    {
        void Add(VehicleOfferObservation vehicleOfferObservation);
        void Delete(int id);
        void Delete(VehicleOfferObservation entity);
        List<VehicleOfferObservation> GetAll();
        VehicleOfferObservation? Get(int id);
        VehicleOfferObservation? GetForUserAndOffer(string userId, int offerId);
    }
}
