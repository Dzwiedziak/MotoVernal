using BusinessLogic.DTO.Ban;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBanService
    {
        Result<int?, BanErrorCode> BanUser(BanUserDTO ban);
    }
}
