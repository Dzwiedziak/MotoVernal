using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class UserObservationRepository : IUserObservationRepository
    {
        private readonly MVAppContext _context;
        public UserObservationRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(UserObservation userObservation) { _context.Add(userObservation); _context.SaveChanges(); }

        public UserObservation? GetOne(int id)
        {
            return _context.UserObservations.FirstOrDefault(uo => uo.Id == id);
        }

        public List<UserObservation> Get(string ObserverId)
        {
            return _context.UserObservations
                .Include(uo => uo.Observer) 
                .Include(uo => uo.Observed) 
                .Where(uo => uo.Observer.Id == ObserverId)
                .ToList();
        }

        public UserObservation? Get(string ObserverId, string ObservedId)
        {
            return Get(ObserverId).FirstOrDefault(uo => uo.Observed.Id == ObservedId);
        }

        public UserObservationErrorCode? Delete(int id)
        {
            UserObservation? userObservation = _context.UserObservations.FirstOrDefault(uo => uo.Id == id);
            if (userObservation is null)
                return UserObservationErrorCode.UserObservationNotFound;

            _context.UserObservations.Remove(userObservation);
            return null;
        }
    }
}
