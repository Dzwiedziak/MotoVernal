using BusinessLogic.Services.Response;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostCommentReactionRepository
    {
        List<PostCommentReaction> GetAll();
        void Update(PostCommentReaction postComment);
        void Add(PostCommentReaction postComment);
    }
}
