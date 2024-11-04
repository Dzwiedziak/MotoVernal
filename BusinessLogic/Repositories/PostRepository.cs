using BusinessLogic.Repositories.Interfaces;
using DB.Entities;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MVAppContext _context;

        public PostRepository(MVAppContext context)
        {
            _context = context;
        }

        public List<Post> GetAll() => _context.Posts.ToList();

        public Post? GetOne(int id) => _context.Posts.FirstOrDefault(p => p.Id == id);

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
