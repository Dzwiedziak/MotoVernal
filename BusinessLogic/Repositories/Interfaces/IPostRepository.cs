using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetAll();
        Post? GetOne(int id);
        void Add(Post post);
        void Update(Post post);
    }
}
