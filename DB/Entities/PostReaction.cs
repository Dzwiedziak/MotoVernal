using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class PostReaction
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

        public PostReaction() { }
        public PostReaction(int id, User user, Post @post)
        {
            Id = id;
            User = user;
            Post = @post;
        }
        public PostReaction(User user, Post @post) : this(0, user, @post) { }
    }
}
