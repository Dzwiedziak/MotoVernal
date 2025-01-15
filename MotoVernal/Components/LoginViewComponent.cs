using BusinessLogic.DTO.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MotoVernal.Components
{
    public class LoginViewComponent : ViewComponent
    {
        readonly IUserService _userService;
        public LoginViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public IViewComponentResult Invoke()
        {
            var model = new LoginUserDTO("", "");
            return View(model);
        }
    }
}
