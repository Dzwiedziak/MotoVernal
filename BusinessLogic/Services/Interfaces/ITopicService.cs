using BusinessLogic.DTO.Topic;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITopicService
    {
        Result<int?, TopicErrorCode> Add(AddTopicDTO topic);
        Result<GetTopicDTO, TopicErrorCode> Get(int id);
        Topic GetOne(int id);
        List<GetTopicDTO> GetAllInSections(int sectionId);
        TopicErrorCode? Update(UpdateTopicDTO user);
    }
}
