using Microsoft.AspNetCore.Mvc;
using Entities = DB.Entities;

namespace MotoVendor.Components
{
    public class FileViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new Entities.File()
            {
                Base64 = "",
                Extension = ""
            };
            return View(model);
        }

        public IViewComponentResult Invoke(Entities.File file)
        {
            return View(file);
        }
    }
}
