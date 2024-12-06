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

        public List<VehicleOffer> GetAll() => _context.VehicleOffers.Include(vo => vo.Images).Include(vo => vo.User).ThenInclude(u => u.ProfileImage).ToList();
        public VehicleOffer? GetOne(int id) => _context.VehicleOffers.Include(vo => vo.Images).Include(vo => vo.User).ThenInclude(u => u.ProfileImage).FirstOrDefault(vo => vo.Id == id);
        public void Add(VehicleOffer offer) { _context.VehicleOffers.Add(offer); _context.SaveChanges(); }
        public void Update(VehicleOffer offer) { _context.VehicleOffers.Update(offer); _context.SaveChanges(); }
    }
}
