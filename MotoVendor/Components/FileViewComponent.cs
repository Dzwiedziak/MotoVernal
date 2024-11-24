using Microsoft.AspNetCore.Mvc;
using Entities = DB.Entities;

namespace MotoVendor.Components
{
    public class FileViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(Entities.File? file = null)
        {
            var model = file ?? new Entities.File
            {
                Base64 = "",
                Extension = ""
            };
            return View(model);
        }
    }
}
