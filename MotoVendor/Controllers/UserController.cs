using BusinessLogic.DTO.User;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public UserController(IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(AddUserDTO user)
        {
            if (ModelState.IsValid)
            {
                var result = _userService.Add(user);
                if (result.IsSuccess)
                {
                    return RedirectToAction("IntroductionPage", "Home");
                }
                switch (result.Error)
                {
                    case UserErrorCode.UserWithNickNameExists:
                        user.Nickname = "";
                        ModelState.AddModelError("Nickname", "User with this nickname already exists");
                        break;

                    case UserErrorCode.UserWithEmailExists:
                        ModelState.AddModelError("Email", "User with this email already exists");
                        break;

                    case UserErrorCode.UserCreationFailed:
                        ModelState.AddModelError(string.Empty, "Unexpected error happened during registering");
                        break;
                }
                return View(user);
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if(user is not null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("IntroductionPage", "Home");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return View(model);
                }
                ModelState.AddModelError(string.Empty, "User not found");
                return View(model);
            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
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
        public IActionResult ProfileView(string id)
        {
            var result = _userService.Get(id);

            if (result.Error == UserErrorCode.UserNotFound)
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Error", "Home");
            }
            return View(result.Value);
        }
    }
}
