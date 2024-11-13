using BusinessLogic.DTO.Ban;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class BanService : IBanService
    {
        private readonly BanRepository _banRepository;
        public BanService(BanRepository banRepository)
        {
            _banRepository = banRepository;
        }

        public Result<int?, BanErrorCode> BanUser(BanUserDTO ban)
        {
            var newBan = CreateNewBan(ban);
            _banRepository.Add(newBan);
            return newBan.Id;
        }

        private Ban CreateNewBan(BanUserDTO ban) =>
            new(ban.Banned, ban.Banner, DateTime.Now, ban.ExpirationTime, ban.Reason, ban.Image);
    }
}
