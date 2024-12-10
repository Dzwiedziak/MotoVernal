using DB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.PostCommentReaction
{
    public class PostCommentReactionDTO
    {
        public ReactionType ReactionType { get; set; }
        public DB.Entities.User User { get; set; }
        public DB.Entities.PostComment PostComment { get; set; }

        public PostCommentReactionDTO()
        {
        }

        public PostCommentReactionDTO(ReactionType reactionType, DB.Entities.User user, DB.Entities.PostComment postComment)
        {
            ReactionType = reactionType;
            User = user;
            PostComment = postComment;
        }
    }
}
