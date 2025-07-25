﻿using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

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
            return _context.PostComments.Where(pc => pc.Id == id)
                .Include(pc => pc.Publisher)
                .FirstOrDefault();
        }
        public void Delete(PostComment postComment)
        {
            _context.PostComments.Remove(postComment);
            _context.SaveChanges();
        }

        public void Update(PostComment postComment)
        {
            _context.Update(postComment);
            _context.SaveChanges();
        }
    }
}
