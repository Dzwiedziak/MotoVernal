using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.EventInterest;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventIntrestRepository _eventIntrestRepository;
        public EventService(IEventRepository eventRepository, IEventIntrestRepository eventIntrestRepository)
        {
            _eventRepository = eventRepository;
            _eventIntrestRepository = eventIntrestRepository;
        }

        public Result<int?, EventErrorCode> Add(AddEventDTO @event)
        {
            var newEvent = CreateNewEvent(@event);
            _eventRepository.Add(newEvent);
            return newEvent.Id;
        }

        public List<Event> GetEvents()
        {
            return _eventRepository.GetAll().ToList();
        }
        public Result<GetEventDTO, EventErrorCode> Get(int id)
        {
            Event? dbEvent = _eventRepository.GetOne(id);
            if (dbEvent is null)
            {
                return EventErrorCode.EventNotFound;
            }

            return CreateGetEventDTO(dbEvent);
        }

        public EventErrorCode? Update(int id, UpdateEventDTO offer)
        {
            Event? dbEvent = _eventRepository.GetOne(id);
            if (dbEvent is null)
                return EventErrorCode.EventNotFound;

            UpdateEvent(ref dbEvent, offer);
            _eventRepository.Update(dbEvent);
            return null;
        }

        public EventInterestErrorCode? InterestUser(AddEventInterestDTO eventInterest)
        {
            EventInterest? dbEventInterest = _eventIntrestRepository.GetOne(eventInterest.User.Id, eventInterest.Event.Id);
            if (dbEventInterest is not null)
                return EventInterestErrorCode.AlreadyInterested;

            _eventIntrestRepository.Add(CreateEventInterest(eventInterest));
            return null;
        }

        public EventInterestErrorCode? StopInterestUser(DeleteEventInterestDTO eventInterest)
        {
            EventInterest? dbEventInterest = _eventIntrestRepository.GetOne(eventInterest.User.Id, eventInterest.Event.Id);
            if (dbEventInterest is null)
                return EventInterestErrorCode.AlreadyNotInterested;

            _eventIntrestRepository.Delete(dbEventInterest.Id);
            return null;
        }

        public Event CreateNewEvent(AddEventDTO @event) =>
            new(@event.Publisher, @event.EventType, @event.TimeFrom, @event.TimeTo, @event.Title, @event.Location, @event.Description, @event.Image);

        public GetEventDTO CreateGetEventDTO(Event @event) =>
            new(@event.Publisher, @event.EventType, @event.TimeFrom, @event.TimeTo, @event.Title, @event.Location, @event.Description, @event.Image);

        public void UpdateEvent(ref Event oldEvent, UpdateEventDTO @event)
        {
            oldEvent.EventType = @event.EventType;
            oldEvent.TimeFrom = @event.TimeFrom;
            oldEvent.TimeTo = @event.TimeTo;
            oldEvent.Title = @event.Title;
            oldEvent.Location = @event.Location;  
            oldEvent.Description = @event.Description;
            oldEvent.Image = @event.Image;
        }

        public EventInterest CreateEventInterest(AddEventInterestDTO eventInterest)
        {
            return new EventInterest(eventInterest.User, eventInterest.Event);
        }
    }
}
