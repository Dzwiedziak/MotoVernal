using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Repositories
{
    public class PostReactionRepository : IPostReactionRepository
    {

        private readonly MVAppContext _context;
        public PostReactionRepository(MVAppContext context)
        {
            _context = context;
        }
        public void Add(PostReaction postReaction)
        {
            _context.Add(postReaction);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbPostReaction = GetOne(id);
            if (dbPostReaction is not null)
                _context.PostReactions.Remove(dbPostReaction);
            _context.SaveChanges();
        }

        public List<PostReaction> GetAll()
        {
            return _context.PostReactions.ToList();
        }

        public List<PostReaction> GetAllByPostId(int postId)
        {
            return _context.PostReactions
                 .Where(pr => pr.Post.Id == postId)
                 .Include(pr => pr.User)
                 .Include(pr => pr.User.ProfileImage)
                 .ToList();
        }

        public PostReaction? GetOne(int id)
        {
            return _context.PostReactions.FirstOrDefault(pr => pr.Id == id);
        }

        public PostReaction? GetOne(string userId, int postId)
        {
            return _context.PostReactions.FirstOrDefault(pr => pr.User.Id == userId && pr.Post.Id == postId);
        }
    }
}
