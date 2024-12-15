using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Event
{
    public class EventDTO
    {
        public int Id { get; set; }
        public Entities.User Publisher { get; set; }
        public EventType EventType { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public Entities.File? Image { get; set; }
        public List<Entities.EventInterest> InterestedList { get; set; }
    }

}
