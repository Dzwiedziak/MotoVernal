using BusinessLogic.Decorators;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Post
{
    public class GetPostDTO
    {
        public int Id { get; set; }
        public Entities.User Publisher { get; set; }
        public string Content { get; set; }
        public DateTime PublicationTime { get; set; }
        public Entities.File? Image { get; set; }
        public List<Entities.PostComment> PostComments { get; set; }


        public GetPostDTO(int id,Entities.User publisher, string content, DateTime publicationTime, Entities.File? image, List<Entities.PostComment> postComments)
        {
            Id = id;
            Publisher = publisher;
            Content = content;
            PublicationTime = publicationTime;
            Image = image;
            PostComments = postComments;
        }
    }
}
