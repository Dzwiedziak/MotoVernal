using BusinessLogic.DTO.Section;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface ISectionService
    {
        Result<int?, SectionErrorCode> Add(AddSectionDTO section);
        Result<GetSectionDTO, SectionErrorCode> Get(int id);
        Result<List<GetSectionDTO>, SectionErrorCode> GetChildrenSections(int id);
    }
}
