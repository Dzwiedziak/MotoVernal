using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostCommentRepository
    {
        void Add(DB.Entities.PostComment postComment);
        void Update(DB.Entities.PostComment postComment);
        void Delete(PostComment postComment);
        DB.Entities.PostComment? Get(int id);
    }
}
