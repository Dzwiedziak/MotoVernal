using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class GetPostDTO
    {
        public Entities.User Publisher { get; set; }
        public string Content { get; set; }
        public DateTime PublicationTime { get; set; }
        public Entities.File? Image { get; set; }

        public GetPostDTO(Entities.User publisher, string content, DateTime publicationTime, Entities.File? image)
        {
            Publisher = publisher;
            Content = content;
            PublicationTime = publicationTime;
            Image = image;
        }
    }
}
