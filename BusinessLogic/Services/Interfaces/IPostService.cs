using BusinessLogic.DTO.Post;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPostService
    {
        Result<int?, PostErrorCode> Add(AddPostDTO post);
        Result<GetPostDTO, PostErrorCode> Get(int id);
        PostErrorCode? Update(int id, UpdatePostDTO user);
    }
}
