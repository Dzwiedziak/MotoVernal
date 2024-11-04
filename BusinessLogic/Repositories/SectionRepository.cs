using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;

namespace BusinessLogic.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly MVAppContext _context;

        public SectionRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<Section> GetAll() => _context.Sections.ToList();
        public Section? GetOne(int id) => _context.Sections.FirstOrDefault(s => s.Id == id);
        public void Add(Section section) { _context.Sections.Add(section); _context.SaveChanges(); }
        public void Update(Section section) { _context.Sections.Update(section); _context.SaveChanges(); }
    }
}
