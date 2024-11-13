using BusinessLogic.DTO.User;
using BusinessLogic.DTO.UserObservation;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserObservationRepository _userObservationRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUserRepository userRepository,
                           IUserObservationRepository userObservationRepository,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _userObservationRepository = userObservationRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Result<string?, UserErrorCode> Add(AddUserDTO user)
        {
            List<User> dbUsers = _userRepository.GetAll();

            if (FindUserWithSameEmail(user.Email, dbUsers) is not null)
                return UserErrorCode.UserWithEmailExists;

            if (FindUserWithSameNickName(user.Nickname, dbUsers) is not null)
                return UserErrorCode.UserWithNickNameExists;

            User newUser = CreateNewUser(user);
            var result = _userManager.CreateAsync(newUser, user.Password).Result;
            if (result.Succeeded)
            {
                _signInManager.SignInAsync(newUser, isPersistent: false).Wait();
                return newUser.Id;
            }
            return UserErrorCode.UserCreationFailed;
        }

        public Result<GetUserDTO, UserErrorCode> Get(string id)
        {
            User? dbUser = _userRepository.GetOne(id);
            if (dbUser is null)
                return UserErrorCode.UserNotFound;

            return CreateGetUserDTO(dbUser);
        }

        public UserErrorCode? Update(string id, UpdateUserDTO user)
        {
            User? dbUser = _userRepository.GetOne(id);
            if (dbUser is null)
                return UserErrorCode.UserNotFound;

            UpdateUser(ref dbUser, user);
            _userRepository.Update(dbUser);
            return null;
        }

        public Result<int?, UserObservationErrorCode> ObserveUser(ObserveUserDTO userObservation)
        {
            _userObservationRepository.Get(userObservation.Observed.Id, userObservation.Observer.Id);
            if (userObservation.Observer is not null)
                return UserObservationErrorCode.UserAlreadyFollowing;

            UserObservation newUserObservation = CreateUserObservation(userObservation);
            _userObservationRepository.Add(newUserObservation);
            return newUserObservation.Id;
        }

        public UserObservationErrorCode? StopObservingUser(int id)
        {
            UserObservation? userObservation = _userObservationRepository.GetOne(id);
            if (userObservation is null)
            {
                return UserObservationErrorCode.UserObservationNotFound;
            }

            _userObservationRepository.Delete(userObservation.Id);
            return null;
        }

        public List<UserObservation> GetAllUserObservations(string id)
        {
            return _userObservationRepository.Get(id);
        }

        private UserObservation CreateUserObservation(ObserveUserDTO observeUser) =>
            new(observer: observeUser.Observer, observed: observeUser.Observed);

        public bool CheckExistance(string id) =>
            _userRepository.GetOne(id) != null;

        private User? FindUserWithSameNickName(string userName, List<User> users) =>
            users.FirstOrDefault(u => String.Equals(u.UserName, userName));

        private User? FindUserWithSameEmail(string email, List<User> users) =>
            users.FirstOrDefault(u => String.Equals(u.Email, email));

        private User CreateNewUser(AddUserDTO user) =>
            new(user.Nickname, user.Email, null, null, DateTime.Now, "");

        private GetUserDTO CreateGetUserDTO(User user) =>
            new(user.Id!,user.UserName!, user.Email!, user.Gender, user.Age, user.CreationTime, user.Description, user.ProfileImage);

        private bool IsUserObserving(string ObserverId, string ObservedId)
        {
            return _userObservationRepository.Get(ObserverId, ObservedId) != null;
        }

        private void UpdateUser(ref User oldUser, UpdateUserDTO user)
        {
            oldUser.Gender = user.Gender;
            oldUser.Age = user.Age;
            oldUser.Description = user.Description;
            oldUser.ProfileImage = user.ProfileImage;
        }

        public List<GetUserDTO> GetAll()
        {
            List<User> dbUsers = _userRepository.GetAll();
            return dbUsers.Select(u => CreateGetUserDTO(u)).ToList();
        }
    }
}
