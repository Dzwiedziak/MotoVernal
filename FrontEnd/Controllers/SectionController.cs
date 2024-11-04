using BusinessLogic.DTO.Section;
using BusinessLogic.Services;
using BusinessLogic.Services.Interfaces;
using DB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Http.Controllers
{
    public class SectionController : Controller
    {
        private readonly ISectionService _sectionService;
        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }
        public ActionResult<List<GetSectionDTO>> Childrens(int id)
        {
            

            return NotFound();     
        }
    }
}
