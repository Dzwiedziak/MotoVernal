using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class VehicleOfferObservationRepository : IVehicleOfferObservationRepository
    {
        readonly MVAppContext _context;
        public VehicleOfferObservationRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(VehicleOfferObservation vehicleOfferObservation)
        {
            _context.VehicleOfferObservations.Add(vehicleOfferObservation);
        }

        public void Delete(VehicleOfferObservation vehicleOfferObservation)
        {
            _context.VehicleOfferObservations.Remove(vehicleOfferObservation);    
        }

        public List<VehicleOfferObservation> GetAll()
        {
            return _context.VehicleOfferObservations.ToList();
        }
    }
}
