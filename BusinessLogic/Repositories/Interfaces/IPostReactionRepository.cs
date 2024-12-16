using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IPostReactionRepository
    {
        List<PostReaction> GetAll();
        List<PostReaction> GetAllByPostId(int postId);
        PostReaction? GetOne(int id);
        PostReaction? GetOne(string userId, int postId);
        void Add(PostReaction postReaction);
        void Delete(int id);
    }
}
