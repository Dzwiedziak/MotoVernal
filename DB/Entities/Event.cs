using DB.Enums;

namespace DB.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public User Publisher { get; set; }
        public EventType EventType { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public File? Image { get; set; }

        public Event() { }

        public Event(int id, User publisher, EventType eventType, DateTime timeFrom, DateTime timeTo, string title, string location, string description, File? image)
        {
            Id = id;
            Publisher = publisher;
            EventType = eventType;
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Title = title;
            Location = location;
            Description = description;
            Image = image;
        }

        public Event(User publisher, EventType eventType, DateTime timeFrom, DateTime timeTo, string title, string location, string description, File? image)
            : this(0, publisher, eventType, timeFrom, timeTo, title, location, description, image) { }

        public Event(Event source)
        {
            Id = source.Id;
            Publisher = source.Publisher;
            EventType = source.EventType;
            TimeFrom = source.TimeFrom;
            TimeTo = source.TimeTo;
            Title = source.Title;
            Location = source.Location;
            Description = source.Description;
        }
    }
}
