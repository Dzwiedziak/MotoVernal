using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MVAppContext _context;

        public UserRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<User> GetAll() => _context.Users.ToList();
        public User? GetOne(string id) => _context.Users.Include(u => u.ProfileImage).FirstOrDefault(u => u.Id == id);
        public void Add(User user) { _context.Users.Add(user); _context.SaveChanges(); }
        public void Update(User user) { _context.Users.Update(user); _context.SaveChanges(); }
    }
}
