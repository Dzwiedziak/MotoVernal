using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MVAppContext _context;

        public PostRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<Post> GetAll() => _context.Posts.Include(p => p.Image).Include(p => p.PostComments).ThenInclude(pc => pc.Publisher).ToList();

        public Post? GetOne(int id) => _context.Posts.Include(p => p.Image).Include(p => p.PostComments).ThenInclude(pc => pc.Publisher).FirstOrDefault(p => p.Id == id);

        public void Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = GetOne(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
        }
    }

}
