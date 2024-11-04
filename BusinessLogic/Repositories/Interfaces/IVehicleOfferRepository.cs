using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IVehicleOfferRepository
    {
        List<VehicleOffer> GetAll();
        VehicleOffer? GetOne(int id);
        void Add(VehicleOffer vehicleOffer);
        void Update(VehicleOffer vehicleOffer);
    }
}
