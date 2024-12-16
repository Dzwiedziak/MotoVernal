using Entities = DB.Entities;

namespace BusinessLogic.DTO.PostReaction
{
    public class PostReactionDTO
    {
        public Entities.User User { get; set; }
        public Entities.Post Post { get; set; }

        public PostReactionDTO() { }
        public PostReactionDTO(Entities.User user, Entities.Post @post)
        {
            User = user;
            Post = @post;
        }
    }
}
