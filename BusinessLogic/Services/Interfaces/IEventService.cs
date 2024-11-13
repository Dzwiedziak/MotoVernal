using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.EventInterest;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEventService
    {
        Result<int?, EventErrorCode> Add(AddEventDTO @event);
        Result<GetEventDTO, EventErrorCode> Get(int id);
        EventErrorCode? Update(int id, UpdateEventDTO offer);
        EventInterestErrorCode? InterestUser(AddEventInterestDTO eventInterest);
        EventInterestErrorCode? StopInterestUser(DeleteEventInterestDTO eventInterest);
    }
}
