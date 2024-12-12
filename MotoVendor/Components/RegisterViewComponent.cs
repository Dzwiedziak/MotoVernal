using BusinessLogic.DTO.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Components
{
    public class RegisterViewComponent : ViewComponent
    {
        readonly IUserService _userService;
        public RegisterViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public IViewComponentResult Invoke()
        {
            var model = new AddUserDTO("", "", "");
            return View(model);
        }
    }
}
