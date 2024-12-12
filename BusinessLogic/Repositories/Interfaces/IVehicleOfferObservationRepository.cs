using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IVehicleOfferObservationRepository
    {
        void Add(VehicleOfferObservation vehicleOfferObservation);
        void Delete(VehicleOfferObservation vehicleOfferObservation);
        List<VehicleOfferObservation> GetAll();
        VehicleOfferObservation? GetForUserAndOffer(string userId, int offerId);
    }
}
