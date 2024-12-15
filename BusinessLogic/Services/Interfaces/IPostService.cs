using BusinessLogic.DTO.Post;
using BusinessLogic.DTO.PostComment;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPostService
    {
        Result<int?, PostErrorCode> Add(AddPostDTO post);
        Result<GetPostDTO, PostErrorCode> Get(int id);
        List<GetPostDTO> GetAll();
        PostErrorCode? Update(UpdatePostDTO user);
        PostCommentErrorCode? AddPostComment(int id, AddPostCommentDTO postComment);
        PostCommentErrorCode? UpdatePostComment(int postCommentId, UpdatePostCommentDTO postComment);

    }
}
