using BusinessLogic.DTO.Section;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public Result<int?, SectionErrorCode> Add(AddSectionDTO section)
        {
            var newSection = CreateNewSection(section);
            _sectionRepository.Add(newSection);
            return newSection.Id;
        }

        public Result<GetSectionDTO, SectionErrorCode> Get(int id)
        {
            Section? dbSection = _sectionRepository.GetOne(id);
            if (dbSection is null)
                return SectionErrorCode.SectionNotFound;

            return CreateGetSectionDTO(dbSection);
        }

        public Result<List<GetSectionDTO>, SectionErrorCode> GetChildrenSections(int id)
        {
            if(!CheckExistance(id))
                return SectionErrorCode.SectionNotFound;

            List<Section> sections = _sectionRepository.GetAll();
            List<Section> childSections = sections.FindAll(s => s.Parent != null && s.Parent.Id == id);
            return childSections.Select(s => CreateGetSectionDTO(s)).ToList();
        }

        private bool CheckExistance(int id) =>
            _sectionRepository.GetOne(id) != null;
            
        private Section CreateNewSection(AddSectionDTO section) =>
            new(section.Title, section.Parent, section.Image);

        private GetSectionDTO CreateGetSectionDTO(Section section) =>
            new(section.Title, section.Parent, section.Image);
    }
}
