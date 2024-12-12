using BusinessLogic.DTO.Event;
using BusinessLogic.DTO.Section;
using BusinessLogic.Errors;
using BusinessLogic.Repositories;
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
        public Section GetOne(int id)
        {
            return _sectionRepository.GetOne(id);
        }
        public SectionErrorCode? Update(UpdateSectionDTO section)
        {
            Section? dbSection = _sectionRepository.GetOne(section.Id);
            if (dbSection is null)
                return SectionErrorCode.SectionNotFound;

            UpdateSection(ref dbSection, section);
            _sectionRepository.Update(dbSection);
            return null;
        }

        public Result<List<GetSectionDTO>, SectionErrorCode> GetChildrenSections(int id)
        {
            if (!CheckExistance(id))
                return SectionErrorCode.SectionNotFound;

            List<Section> sections = _sectionRepository.GetAll();
            List<Section> childSections = sections.FindAll(s => s.Parent != null && s.Parent.Id == id);
            return childSections.Select(s => CreateGetSectionDTO(s)).ToList();
        }
        public Result<List<GetSectionDTO>, SectionErrorCode> GetParentSections(int id)
        {
            if (!CheckExistance(id))
                return SectionErrorCode.SectionNotFound;

            Section currentSection = _sectionRepository.GetOne(id);
            if (currentSection == null)
                return SectionErrorCode.SectionNotFound;

            List<GetSectionDTO> parentSections = new List<GetSectionDTO>();

            while (currentSection.Parent != null)
            {
                currentSection = currentSection.Parent;
                parentSections.Add(CreateGetSectionDTO(currentSection));
            }

            parentSections.Reverse();

            return parentSections;
        }
        public Result<GetSectionDTO, SectionErrorCode> GetRootSection()
        {
            var rootSection = _sectionRepository.GetAll().FirstOrDefault(s => s.Parent == null);
            if (rootSection == null)
            {
                return SectionErrorCode.SectionNotFound;
            }

            return CreateGetSectionDTO(rootSection);
        }

        public void UpdateSection(ref Section oldSection, UpdateSectionDTO @section)
        {
            oldSection.Title = @section.Title;
            oldSection.Image = @section.Image;
        }

        private bool CheckExistance(int id) =>
            _sectionRepository.GetOne(id) != null;

        private Section CreateNewSection(AddSectionDTO section) =>
            new(section.Title, section.Parent, section.Image);

        private GetSectionDTO CreateGetSectionDTO(Section section) =>
            new(section.Id,section.Title, section.Parent, section.Image);
    }
}
