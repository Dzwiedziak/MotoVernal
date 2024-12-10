namespace DB.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public User Publisher { get; set; }
        public string Content { get; set; }
        public DateTime PublicationTime { get; set; }
        public File? Image { get; set; }
        public List<PostComment> PostComments { get; set; }

        public Post() { }

        public Post(int id, User publisher, string content, DateTime publicationTime, File? image)
        {
            Id = id;
            Publisher = publisher;
            Content = content;
            PublicationTime = publicationTime;
            Image = image;
        }

        public Post(User publisher, string content, DateTime publicationTime, File? image)
            : this(0, publisher, content, publicationTime, image) { }
    }
}
