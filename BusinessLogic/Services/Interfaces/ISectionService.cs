using BusinessLogic.DTO.Section;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface ISectionService
    {
        Result<int?, SectionErrorCode> Add(AddSectionDTO section);
        Result<GetSectionDTO, SectionErrorCode> Get(int id);
        Section GetOne(int id);
        Result<List<GetSectionDTO>, SectionErrorCode> GetChildrenSections(int id);
        Result<List<GetSectionDTO>, SectionErrorCode> GetParentSections(int id);
        Result<GetSectionDTO, SectionErrorCode> GetRootSection();
        SectionErrorCode? Update(UpdateSectionDTO section);
    }
}
