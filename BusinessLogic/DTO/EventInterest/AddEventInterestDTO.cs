using Entities = DB.Entities;

namespace BusinessLogic.DTO.EventInterest
{
    public class AddEventInterestDTO
    {
        public Entities.User User { get; set; }
        public Entities.Event Event { get; set; }

        public AddEventInterestDTO() { }
        public AddEventInterestDTO(Entities.User user, Entities.Event @event)
        {
            User = user;
            Event = @event;
        }
    }
}
