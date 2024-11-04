namespace DB.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public User Publisher { get; set; }
        public string Content { get; set; }
        public DateTime PublicationTime { get; set; }

        public Post() { }

        public Post(int id, User publisher, string content, DateTime publicationTime)
        {
            Id = id;
            Publisher = publisher;
            Content = content;
            PublicationTime = publicationTime;
        }

        public Post(User publisher, string content, DateTime publicationTime)
            : this(0, publisher, content, publicationTime) { }

        public Post(Post source)
        {
            Id = source.Id;
            Publisher = source.Publisher;
            Content = source.Content;
            PublicationTime = source.PublicationTime;
        }
    }
}
