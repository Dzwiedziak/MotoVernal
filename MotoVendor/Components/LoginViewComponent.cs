using BusinessLogic.DTO.User;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MotoVendor.Controllers;

namespace MotoVendor.Components
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
