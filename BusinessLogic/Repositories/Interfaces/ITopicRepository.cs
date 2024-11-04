using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface ITopicRepository
    {
        List<Topic> GetAll();
        Topic? GetOne(int id);
        void Add(Topic topic);
        void Update(Topic topic);

    }
}
