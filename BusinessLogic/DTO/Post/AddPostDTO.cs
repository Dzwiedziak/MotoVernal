using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class AddPostDTO
    {
        public Entities.User Publisher { get; set; }
        public string Content { get; set; }

        public AddPostDTO(Entities.User publisher, string content)
        {
            Publisher = publisher;
            Content = content;
        }
    }
}
