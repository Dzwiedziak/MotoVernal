using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface ISectionRepository
    {
        List<Section> GetAll();
        Section? GetOne(int id);
        void Add(Section section);
        void Update(Section section);
    }
}
