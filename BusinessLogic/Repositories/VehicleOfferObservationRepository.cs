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

        public void Add(VehicleOfferObservation entity)
        {
            _context.VehicleOfferObservations.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entityToDelete = new VehicleOfferObservation() { Id = id };
            Delete(entityToDelete);
        }

        public void Delete(VehicleOfferObservation entity)
        {
            _context.VehicleOfferObservations.Remove(entity);
            _context.SaveChanges();
        }

        public VehicleOfferObservation? Get(int id)
        {
            return _context.VehicleOfferObservations.Where(o => o.Id == id).FirstOrDefault();
        }

        public List<VehicleOfferObservation> GetAll()
        {
            return _context.VehicleOfferObservations.ToList();
        }

        public VehicleOfferObservation? GetForUserAndOffer(string userId, int offerId)
        {
            return _context.VehicleOfferObservations
                .Where(o =>
                    o.Observer.Id == userId &&
                    o.VehicleOffer.Id == offerId)
                .FirstOrDefault();
        }
    }
}
