using BusinessLogic.Repositories.Interfaces;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class PostCommentService : IPostCommentService
    {
        readonly IPostCommentRepository _postCommentRepository;
        public PostCommentService(IPostCommentRepository postCommentRepository)
        {
            _postCommentRepository = postCommentRepository;
        }

        public PostComment? Get(int id)
        {
            return _postCommentRepository.Get(id);
        }

    }
}
