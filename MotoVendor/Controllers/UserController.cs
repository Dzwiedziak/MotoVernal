using BusinessLogic.DTO.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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
                //result.Match(
                //successValue => Result.Success("User verified successfully"),
                //errorValue => Result.Failure($"Verification failed: {errorValue}")
                //);
                return RedirectToAction("IntroductionPage", "Home");
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserDTO user)
        {
            if (ModelState.IsValid)
            {
                //logowanie

                return RedirectToAction("IntroductionPage", "Home");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult ProfilesList()
        {
            List<GetUserDTO> dbUsers = _userService.GetAll();
            return View(dbUsers);
        }
    }
}
