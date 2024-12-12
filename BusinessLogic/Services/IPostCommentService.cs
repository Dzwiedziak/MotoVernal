using DB.Entities;

namespace BusinessLogic.Services
{
    public interface IPostCommentService
    {
        PostComment? Get(int id);
    }
}
