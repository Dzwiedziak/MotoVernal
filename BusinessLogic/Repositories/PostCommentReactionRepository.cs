using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class PostCommentReactionRepository : IPostCommentReactionRepository
    {
        readonly MVAppContext _context;
        public PostCommentReactionRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(PostCommentReaction postComment)
        {
            _context.Add(postComment);
            _context.SaveChanges();
        }

        public List<PostCommentReaction> GetAll()
        {
            return _context.PostCommentReactions
                .Include(pcr => pcr.PostComment)
                .Include(pcr => pcr.User)
                .ToList();
        }

        public void Update(PostCommentReaction postComment)
        {
            _context.Update(postComment);
            _context.SaveChanges();
        }
    }
}
