using BusinessLogic.DTO.Event;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IEventService
    {
        Result<int?, EventErrorCode> Add(AddEventDTO @event);
        Result<GetEventDTO, EventErrorCode> Get(int id);
        EventErrorCode? Update(int id, UpdateEventDTO offer);
    }
}
