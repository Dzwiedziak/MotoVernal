using BusinessLogic.DTO.User;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Result<string?, UserErrorCode> Add(AddUserDTO user);
        Result<GetUserDTO, UserErrorCode> Get(string id);
        List<GetUserDTO> GetAll();
        UserErrorCode? Update(string id, UpdateUserDTO user);
        bool CheckExistance(string id);
    }
}
