using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class AddPostDTO
    {
        public Entities.User Publisher { get; set; }
        public string Content { get; set; }
        public Entities.File? Image { get; set; }

        public AddPostDTO(Entities.User publisher, string content, Entities.File? image)
        {
            Publisher = publisher;
            Content = content;
            Image = image;
        }
    }
}
