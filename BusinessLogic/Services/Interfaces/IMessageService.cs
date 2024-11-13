using BusinessLogic.DTO.Message;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IMessageService
    {
        Result<int?, MessageErrorCode> Add(SendMessageDTO message);
    }
}
