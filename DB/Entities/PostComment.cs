using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class PostComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public User Publisher { get; set; }
        public DateTime CreationTime { get; set; }  
        public List<PostCommentReaction> Reactions { get; set; }

        public PostComment() { }

        public PostComment(int id, string content, Post post, User publisher, DateTime creationTime, List<PostCommentReaction> reactions)
        {
            Id = id;
            Content = content;
            Post = post;
            Publisher = publisher;
            CreationTime = creationTime;
            Reactions = reactions;
        }
    }
}
