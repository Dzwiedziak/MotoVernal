using BusinessLogic.DTO.PostCommentReaction;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPostCommentReactionService
    {
        public void AddOrUpdate(PostCommentReactionDTO postCommentReaction);
    }
}
