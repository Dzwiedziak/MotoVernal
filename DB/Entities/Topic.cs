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
        public File? Image { get; set; }

        public Topic() { }

        public Topic(int id, string title, string description, DateTime creationTime, User publisher, Section section, File? image)
        {
            Id = id;
            Title = title;
            Description = description;
            CreationTime = creationTime;
            Publisher = publisher;
            Section = section;
            Image = image;
        }

        public Topic(string title, string description, DateTime creationTime, User publisher, Section section, File? image)
            : this(0, title, description, creationTime, publisher, section, image) { }

    }
}
