using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class VehicleOfferRepository : IVehicleOfferRepository
    {
        private readonly MVAppContext _context;

        public VehicleOfferRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<VehicleOffer> GetAll() => _context.VehicleOffers.Include(vo => vo.Images).Include(vo => vo.User).ThenInclude(u => u.ProfileImage).Where(o => o.State != DB.Enums.VehicleOfferState.Removed).ToList();
        public VehicleOffer? GetOne(int id) => _context.VehicleOffers.Include(vo => vo.Images).Include(vo => vo.User).ThenInclude(u => u.ProfileImage).Where(o => o.State != DB.Enums.VehicleOfferState.Removed).FirstOrDefault(vo => vo.Id == id);
        public void Add(VehicleOffer offer) { _context.VehicleOffers.Add(offer); _context.SaveChanges(); }
        public void Update(VehicleOffer offer) { _context.VehicleOffers.Update(offer); _context.SaveChanges(); }

        public List<VehicleOffer> GetAllOwners(string userId)
        {
            return GetAll().Where(o => o.User.Id == userId).ToList();
        }

        public void Delete(VehicleOffer vehicleOffer)
        {
            vehicleOffer.State = DB.Enums.VehicleOfferState.Removed;
            Update(vehicleOffer);
            _context.SaveChanges();
        }
    }
}
