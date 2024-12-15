using DB.Enums;

namespace DB.Entities
{
    public class BugReport
    {
        public int Id { get; set; }
        public User Reporter { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public File? Image { get; set; }
        public BugType BugType { get; set; }
        public BugState BugState { get; set; }

        public BugReport() { }
        public BugReport(int id, User reporter, string description, DateTime creationTime, File? image, BugType bugType, BugState bugState)
        {
            Id = id;
            Reporter = reporter;
            Description = description;
            CreationTime = creationTime;
            Image = image;
            BugType = bugType;
            BugState = bugState;
        }

        public BugReport(User reporter, string description, DateTime creationTime, File? image, BugType bugType, BugState bugState)
            : this(0, reporter, description, creationTime, image, bugType, bugState) { }
    }
}
