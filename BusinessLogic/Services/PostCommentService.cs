using BusinessLogic.Repositories.Interfaces;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
