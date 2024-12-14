using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        List<Topic> GetAll();
        List<Topic> GetAllInSection(int sectionId);
        Topic? GetOne(int id);
        void Add(Topic topic);
        void Update(Topic topic);
    }
}
