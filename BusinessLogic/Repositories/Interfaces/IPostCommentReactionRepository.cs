using DB.Entities;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostCommentReactionRepository
    {
        List<PostCommentReaction> GetAll();
        void Update(PostCommentReaction postComment);
        void Add(PostCommentReaction postComment);
    }
}
