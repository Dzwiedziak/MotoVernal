namespace DB.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public User Broadcaster { get; set; }
        public User Reciever { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }

        public Message() { }

        public Message(int id, User broadcaster, User reciever, string content, DateTime creationTime)
        {
            Id = id;
            Broadcaster = broadcaster;
            Reciever = reciever;
            Content = content;
            CreationTime = creationTime;
        }

        public Message(User broadcaster, User reciever, string content, DateTime creationTime)
            : this(0, broadcaster, reciever, content, creationTime) { }

    }
}
