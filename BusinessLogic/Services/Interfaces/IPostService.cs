using BusinessLogic.DTO.Post;
using BusinessLogic.DTO.PostComment;
using BusinessLogic.DTO.PostReaction;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPostService
    {
        Result<int?, PostErrorCode> Add(AddPostDTO post);
        Result<GetPostDTO, PostErrorCode> Get(int id);
        Post? GetPost(int id);
        List<GetPostDTO> GetAll();
        PostErrorCode? Update(UpdatePostDTO user);
        PostReactiontErrorCode? LikePost(PostReactionDTO postReaction);
        PostReactiontErrorCode? StopLikePost(PostReactionDTO postReaction);
        PostCommentErrorCode? AddPostComment(int id, AddPostCommentDTO postComment);
        PostCommentErrorCode? UpdatePostComment(int postCommentId, UpdatePostCommentDTO postComment);
        List<PostReaction> GetAllReactions();

    }
}
