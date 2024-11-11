using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class UpdatePostDTO
    {
        public string Content { get; set; }
        public Entities.File? Image { get; set; }

        public UpdatePostDTO(string content, Entities.File? image)
        {
            Content = content;
            Image = image;
        }
    }
}
