using BusinessLogic.DTO.Ban;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class BanService : IBanService
    {
        private readonly IBanRepository _banRepository;
        public BanService(IBanRepository banRepository)
        {
            _banRepository = banRepository;
        }

        public Result<int?, BanErrorCode> BanUser(BanUserDTO ban)
        {
            if (IsUserBanned(ban.Banned.Id))
            {
                return BanErrorCode.UserAlreadyBanned;
            }
            var newBan = CreateNewBan(ban);
            _banRepository.Add(newBan);
            return newBan.Id;
        }
        public Ban? GetActiveBan(string userId)
        {
            return _banRepository.GetAll()
            .FirstOrDefault(ban => ban.Banned != null && ban.Banned.Id == userId && ban.ExpirationTime > DateTime.Now);
        }

        public Result<bool, BanErrorCode> UnbanUser(string userId)
        {
            var activeBan = _banRepository.GetAll()
                .FirstOrDefault(b => b.Banned != null && b.Banned.Id == userId && b.ExpirationTime > DateTime.Now);

            if (activeBan == null)
            {
                return BanErrorCode.NoActiveBan;
            }

            activeBan.ExpirationTime = DateTime.Now;
            _banRepository.Update(activeBan);

            return true;
        }

        private bool IsUserBanned(string userId)
        {
            return GetActiveBan(userId) != null;
        }

        private Ban CreateNewBan(BanUserDTO ban) =>
            new(ban.Banned, ban.Banner, DateTime.Now, ban.ExpirationTime, ban.Reason, ban.Image);
    }
}
