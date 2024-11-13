namespace DB.Entities
{
    public class BugReport
    {
        public int Id { get; set; }
        public User Reporter { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public File? Image { get; set; }

        public BugReport() { }
        public BugReport(int id, User reporter, string description, DateTime creationTime, File? image)
        {
            Id = id;
            Reporter = reporter;
            Description = description;
            CreationTime = creationTime;
            Image = image;
        }

        public BugReport(User reporter, string description, DateTime creationTime, File? image)
            : this(0, reporter, description, creationTime, image) { }
    }
}
