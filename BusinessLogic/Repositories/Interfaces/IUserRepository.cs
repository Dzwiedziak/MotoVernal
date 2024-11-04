using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetOne(string id);
        void Add(User user);
        void Update(User user);
    }
}
