namespace DB.Entities
{
    public class TopicResponse
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
        public User Owner { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public File? Image { get; set; }

        public TopicResponse() { }

        public TopicResponse(int id, Topic topic, User owner, string description, DateTime creationTime)
        {
            Id = id;
            Topic = topic;
            Owner = owner;
            Description = description;
            CreationTime = creationTime;
        }

        public TopicResponse(Topic topic, User owner, string description, DateTime creationTime)
            : this(0, topic, owner, description, creationTime) { }
    }
}
