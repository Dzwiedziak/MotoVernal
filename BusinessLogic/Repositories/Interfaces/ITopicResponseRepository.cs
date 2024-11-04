using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface ITopicResponseRepository
    {
        List<TopicResponse> GetAll();
        TopicResponse? GetOne(int id);
        void Add(TopicResponse user);
        void Update(TopicResponse user);
    }
}
