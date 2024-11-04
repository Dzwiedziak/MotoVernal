namespace DB.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public User Reporter { get; set; }
        public User Reported { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        public Report(int id, User reporter, User reported, string description, DateTime creationTime)
        {
            Id = id;
            Reporter = reporter;
            Reported = reported;
            Description = description;
            CreationTime = creationTime;
        }

        public Report(User reporter, User reported, string description, DateTime creationTime)
            : this (0, reporter, reported, description, creationTime) { }
    }
}
