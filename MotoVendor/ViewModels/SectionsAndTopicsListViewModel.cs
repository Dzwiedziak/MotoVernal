using BusinessLogic.DTO.Section;
using BusinessLogic.DTO.Topic;

namespace MotoVendor.ViewModels
{
    public class SectionsAndTopicsListViewModel
    {
        public GetSectionDTO SectionInfo { get; set; }
        public List<GetSectionDTO> ChildSections { get; set; }
        public List<GetSectionDTO> ParentSections { get; set; }
        public List<GetTopicDTO> TopicsList { get; set; }
    }
}
