using BusinessLogic.DTO.User;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserDTO user)
        {
            var referer = Request.Headers["Referer"].ToString();
            var errors = new Dictionary<string, List<string>>();
            TempData["RegisterSuccessful"] = true;

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(user.UserName);
                if (existingUser != null)
                {
                    errors["UserName"] = new List<string> { "User with this nickname already exists" };
                    TempData["RegisterSuccessful"] = false;
                }
                else
                {
                    var existingEmail = await _userManager.FindByEmailAsync(user.Email);
                    if (existingEmail != null)
                    {
                        errors["Email"] = new List<string> { "User with this email already exists" };
                        TempData["RegisterSuccessful"] = false;
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
                                TempData["RegisterSuccessful"] = false;
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
                TempData["RegisterSuccessful"] = false;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDTO model)
        {
            TempData["LoginSuccessful"] = true;
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
                    TempData["LoginSuccessful"] = false;
                }
                else
                {
                    errors["UserName"] = new List<string> { "User not found" };
                    TempData["LoginSuccessful"] = false;
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
                TempData["LoginSuccessful"] = false;
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
            ViewBag.IsCurrentUser = isCurrentUser;
            ViewBag.IsAdmin = isAdmin;

            return View(result.Value);
        }
    }
}
