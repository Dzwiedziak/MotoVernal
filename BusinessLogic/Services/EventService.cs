using BusinessLogic.DTO.Event;
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
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public Result<int?, EventErrorCode> Add(AddEventDTO @event)
        {
            var newEvent = CreateNewEvent(@event);
            _eventRepository.Add(newEvent);
            return newEvent.Id;
        }

        public Result<GetEventDTO, EventErrorCode> Get(int id)
        {
            Event? dbEvent = _eventRepository.GetOne(id);
            if (dbEvent is null)
                return EventErrorCode.EventNotFound;

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

        public Event CreateNewEvent(AddEventDTO @event) =>
            new(@event.Publisher, @event.EventType, @event.TimeFrom, @event.TimeTo, @event.Description);

        public GetEventDTO CreateGetEventDTO(Event @event) =>
            new(@event.Publisher, @event.EventType, @event.TimeFrom, @event.TimeTo, @event.Description);

        public void UpdateEvent(ref Event oldEvent, UpdateEventDTO @event)
        {
            oldEvent.TimeFrom = @event.TimeFrom;
            oldEvent.TimeTo = @event.TimeTo;
            oldEvent.Description = @event.Description;
        }
    }
}
