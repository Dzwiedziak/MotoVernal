using BusinessLogic.Errors;
using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IUserObservationRepository
    {
        UserObservation? GetOne(int id);
        void Add(UserObservation userObservation);
        UserObservation? Get(string ObserverId, string ObservedId);
        List<UserObservation> Get(string ObserverId);
        UserObservationErrorCode? Delete(int id);
    }
}
