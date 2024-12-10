using BusinessLogic.DTO.Event;

namespace MotoVendor.ViewModels
{
    public class EventDetailsViewModel
    {
        public GetEventDTO EventDetails { get; set; }
        public List<DB.Entities.EventInterest> InterestedUsers { get; set; }
    }
}
