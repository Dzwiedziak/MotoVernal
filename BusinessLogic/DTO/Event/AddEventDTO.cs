using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Event
{
    public class AddEventDTO
    {
        public Entities.User Publisher { get; set; }
        public EventType EventType { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Description { get; set; }

        public AddEventDTO(Entities.User publisher, EventType eventType, DateTime timeFrom, DateTime timeTo, string description)
        {
            Publisher = publisher;
            EventType = eventType;
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Description = description;
        }
    }
}
