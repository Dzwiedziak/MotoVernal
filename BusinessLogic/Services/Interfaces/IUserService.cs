using BusinessLogic.DTO.User;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        Result<string?, UserErrorCode> Add(AddUserDTO user);
        Result<GetUserDTO, UserErrorCode> Get(string id);
        Task<User?> GetCurrentUser();
        List<GetUserDTO> GetAll();
        User GetUser(string id);
        UserErrorCode? Update(string id, UpdateUserDTO user);
        bool CheckExistance(string id);

    }
}
