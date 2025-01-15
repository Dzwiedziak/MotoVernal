using BusinessLogic.DTO.User;
using BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MotoVernal.Components
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
