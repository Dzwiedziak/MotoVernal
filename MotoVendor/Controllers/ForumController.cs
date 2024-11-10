using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult SectionsAndTopicsList()
        {
            return View();
        }

        public IActionResult AddSection()
        { 
            return View(); 
        }
        public IActionResult EditSection()
        {
            return View();
        }
        public IActionResult AddTopic()
        {
            return View();
        }
        public IActionResult EditTopic()
        {
            return View();
        }
        public IActionResult DetailsTopic() 
        {
            return View();
        }
    }
}
