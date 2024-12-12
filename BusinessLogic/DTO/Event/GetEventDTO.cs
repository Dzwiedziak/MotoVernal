using DB.Enums;
using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Event
{
    public class GetEventDTO
    {
        public Entities.User Publisher { get; set; }
        public EventType EventType { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public Entities.File? Image { get; set; }

        public GetEventDTO(Entities.User publisher, EventType eventType, DateTime timeFrom, DateTime timeTo, string title, string location, string description, Entities.File? image)
        {
            Publisher = publisher;
            EventType = eventType;
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Title = title;
            Location = location;
            Description = description;
            Image = image;
        }
    }
}
