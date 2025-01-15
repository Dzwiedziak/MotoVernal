using BusinessLogic.DTO.Event;

namespace MotoVernal.ViewModels
{
    public class EventDetailsViewModel
    {
        public GetEventDTO EventDetails { get; set; }
        public List<DB.Entities.EventInterest> InterestedUsers { get; set; }
    }
}
