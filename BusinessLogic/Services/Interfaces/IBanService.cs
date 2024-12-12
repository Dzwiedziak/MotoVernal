using BusinessLogic.DTO.Ban;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface IBanService
    {
        Result<int?, BanErrorCode> BanUser(BanUserDTO ban);
        Result<bool, BanErrorCode> UnbanUser(string userId);
        Ban? GetActiveBan(string userId);
        List<Ban> GetBansByUser(string userId);
        Ban GetBanById(int banId);
        List<Ban> GetAllBans ();
    }
}
