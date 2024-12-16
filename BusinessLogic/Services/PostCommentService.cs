using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
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
        public void Delete(int id) 
        {
            var comment = _postCommentRepository.Get(id);
            _postCommentRepository.Delete(comment);
        }
        public PostComment? Get(int id)
        {
            return _postCommentRepository.Get(id);
        }

    }
}
