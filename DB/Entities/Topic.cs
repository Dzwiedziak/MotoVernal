namespace DB.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public User Publisher { get; set; }
        public Section Section { get; set; }

        public Topic() { }

        public Topic(int id, string title, string description, DateTime creationTime, User publisher, Section section)
        {
            Id = id;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Publisher = publisher;
            Section = section;
        }

        public Topic(string title, string description, DateTime creationTime, User publisher, Section section)
            : this(0, title, description, creationTime, publisher, section) { }

        public Topic(Topic source)
            : this(source.Id, source.Title, source.Description, source.CreationTime, source.Publisher, source.Section) { }
    }
}
