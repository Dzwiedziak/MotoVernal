using DB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class PostCommentReaction
    {
        public int Id { get; set; }
        public ReactionType ReactionType { get; set; }
        public User User { get; set; }
        public PostComment PostComment { get; set; }

        public PostCommentReaction() { }

        public PostCommentReaction(int id, ReactionType reactionType, User user, PostComment postComment)
        {
            Id = id;
            ReactionType = reactionType;
            User = user;
            PostComment = postComment;
        }
    }
}
