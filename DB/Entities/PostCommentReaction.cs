using DB.Enums;

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
