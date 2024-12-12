using BusinessLogic.DTO.PostCommentReaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IPostCommentReactionService
    {
        public void AddOrUpdate(PostCommentReactionDTO postCommentReaction);
    }
}
