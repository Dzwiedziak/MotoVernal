using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPostCommentService
    {
        PostComment? Get(int id);
        void Delete(int id);
    }
}
