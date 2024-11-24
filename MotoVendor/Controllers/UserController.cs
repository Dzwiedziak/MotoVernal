using BusinessLogic.DTO.Ban;
using BusinessLogic.DTO.User;
using BusinessLogic.Errors;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;


namespace MotoVendor.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBanService _banService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserController(IUserService userService, IBanService banService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userService = userService;
            _banService = banService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var referer = Request.Headers["Referer"].ToString();
            TempData["AuthState"] = "Register";

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserDTO user)
        {
            var referer = Request.Headers["Referer"].ToString();
            var errors = new Dictionary<string, List<string>>();
            TempData["AuthState"] = "None";

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(user.UserName);
                if (existingUser != null)
                {
                    errors["UserName"] = new List<string> { "User with this nickname already exists" };
                    TempData["AuthState"] = "Register";
                }
                else
                {
                    var existingEmail = await _userManager.FindByEmailAsync(user.Email);
                    if (existingEmail != null)
                    {
                        errors["Email"] = new List<string> { "User with this email already exists" };
                        TempData["AuthState"] = "Register";
                    }
                    else
                    {
                        var newUser = new User(user.UserName, user.Email, null, null, DateTime.Now, "");

                        var result = await _userManager.CreateAsync(newUser, user.Password);
                        if (result.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(referer))
                            {
                                return Redirect(referer);
                            }

                            return RedirectToAction("IntroductionPage", "Home");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                errors["General"] = errors.GetValueOrDefault("General", new List<string>());
                                errors["General"].Add(error.Description);
                                TempData["AuthState"] = "Register";
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var fieldErrors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToList();
                    if (fieldErrors.Any())
                    {
                        errors[key] = fieldErrors;
                    }
                }
                TempData["AuthState"] = "Register";
            }

            TempData["RegisterModel"] = JsonConvert.SerializeObject(user);
            TempData["RegisterErrors"] = JsonConvert.SerializeObject(errors);

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var referer = Request.Headers["Referer"].ToString();
            TempData["AuthState"] = "Login";

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            TempData["AuthState"] = "None";
            var referer = Request.Headers["Referer"].ToString();
            var errors = new Dictionary<string, List<string>>();

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(referer))
                        {
                            return Redirect(referer);
                        }
                        return RedirectToAction("IntroductionPage", "Home");
                    }

                    errors["UserName"] = new List<string> { "Invalid login attempt" };
                    TempData["AuthState"] = "Login";
                }
                else
                {
                    errors["UserName"] = new List<string> { "User not found" };
                    TempData["AuthState"] = "Login";
                }
            }
            else
            {
                foreach (var key in ModelState.Keys)
                {
                    var fieldErrors = ModelState[key].Errors.Select(e => e.ErrorMessage).ToList();
                    if (fieldErrors.Any())
                    {
                        errors[key] = fieldErrors;
                    }
                }
                TempData["AuthState"] = "Login";
            }
            TempData["LoginModel"] = JsonConvert.SerializeObject(model);
            TempData["LoginErrors"] = JsonConvert.SerializeObject(errors);

            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            var referer = Request.Headers["Referer"].ToString();
            await _signInManager.SignOutAsync();
            if (!string.IsNullOrEmpty(referer))
            {
                return Redirect(referer);
            }
            return RedirectToAction("IntroductionPage", "Home");
        }

        [HttpGet]
        public IActionResult ProfilesList()
        {
            List<GetUserDTO> dbUsers = _userService.GetAll();
            return View(dbUsers);
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("IntroductionPage", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult ProfileView(string id)
        {
            var result = _userService.Get(id);

            if (result.Error == UserErrorCode.UserNotFound)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction("Error", "Home");
            }
            var currentUser = User.Identity.Name;
            var isCurrentUser = result.Value.UserName == currentUser;
            bool isAdmin = User.IsInRole("Admin");

            var activeBan = _banService.GetActiveBan(id);
            var isBanned = activeBan != null;

            ViewBag.IsCurrentUser = isCurrentUser;
            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsBanned = isBanned;
            ViewBag.BanExpiration = activeBan?.ExpirationTime;

            return View(result.Value);
        }
      
        [HttpGet]
        [Authorize]
        public IActionResult EditProfile(string id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != id)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this profile.";
                return RedirectToAction("Error", "Home");
            }
            var result = _userService.Get(id);
            if (result.Error == UserErrorCode.UserNotFound)
            {
                TempData["ErrorMessage"] = "User not found";
                return RedirectToAction("Error", "Home");
            }
            var updateUserDTO = new UpdateUserDTO(
                id,
                result.Value.Gender,
                result.Value.Age,
                result.Value.Description,
                result.Value.ProfileImage
            ); ;

            return View(updateUserDTO);
        }
        [Authorize]
        [HttpPost]
        public IActionResult EditProfile(UpdateUserDTO updateUserDTO)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (currentUserId != updateUserDTO.Id)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this profile.";
                return RedirectToAction("Error", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View(updateUserDTO);
            }
            _userService.Update(updateUserDTO.Id, updateUserDTO);
            return RedirectToAction("ProfileView", new { id = updateUserDTO.Id });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> BanAccount(string id)
        {
            var bannedUser =  await _userManager.FindByIdAsync(id);
            var bannerUser = await _userManager.GetUserAsync(User);

            var model = new BanUserDTO
            {
                Banned = bannedUser,
                Banner = bannerUser, 
                ExpirationTime = DateTime.Today.AddDays(1),
                Reason = string.Empty, 
                Image = null  
            };
            return View(model);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> BanAccount(BanUserDTO model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid input. Please check the form and try again.";
                return RedirectToAction("BanAccount", new { id = model.Banned.Id });
            }
            var bannedUser = await _userManager.FindByIdAsync(model.Banned.Id);
            var bannerUser = await _userManager.GetUserAsync(User);

            model.Banned = bannedUser;
            model.Banner = bannerUser;

            var result = _banService.BanUser(model);
            if (result.Error == BanErrorCode.UserAlreadyBanned)
            {
                TempData["ErrorMessage"] = "User is already banned";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("ProfileView", new {id = model.Banned.Id});
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveBan(string id)
        {
            var activeBan = _banService.GetActiveBan(id);

            if (activeBan == null)
            {
                TempData["ErrorMessage"] = "No active ban found for the user.";
                return RedirectToAction("Error", "Home");
            }

            ViewBag.UserId = id;
            ViewBag.UserName = activeBan.Banned.UserName;
            ViewBag.BanExpiration = activeBan.ExpirationTime;

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult RemoveBanConfirmed(string id)
        {
            var result = _banService.UnbanUser(id);
            if(result.Error == BanErrorCode.NoActiveBan)
            {
                TempData["ErrorMessage"] = "No active ban found for the user.";
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("ProfileView", new { id });
        }

    }
}
