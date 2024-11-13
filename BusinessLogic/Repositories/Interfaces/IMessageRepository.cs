using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        List<Message> GetAll();
        Message? GetOne(int id);
        void Add(Message message);
        void Update(Message message);
    }
}
