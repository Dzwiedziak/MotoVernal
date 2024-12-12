using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class PostCommentRepository : IPostCommentRepository
    {
        MVAppContext _context;
        public PostCommentRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(PostComment postComment)
        {
            _context.Add(postComment);
            _context.SaveChanges();
        }

        public PostComment? Get(int id)
        {
            return _context.PostComments.Where(pc => pc.Id == id).FirstOrDefault();
        }

        public void Update(PostComment postComment)
        {
            _context.Update(postComment);
            _context.SaveChanges();
        }
    }
}
