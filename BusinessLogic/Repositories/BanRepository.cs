using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class BanRepository : IBanRepository
    {
        public readonly MVAppContext _context;
        public BanRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(Ban ban) { _context.Add(ban); _context.SaveChanges(); }

        public List<Ban> GetAll() => _context.Bans.ToList();

        public Ban? GetOne(int id) => _context.Bans.FirstOrDefault(b => b.Id == id);

        public void Update(Ban ban) { _context.Update(ban); _context.SaveChanges(); }
    }
}
