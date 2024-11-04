using BusinessLogic.DTO.Topic;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITopicService
    {
        Result<int?, TopicErrorCode> Add(AddTopicDTO topic);
        Result<GetTopicDTO, TopicErrorCode> Get(int id);
        TopicErrorCode? Update(int id, UpdateTopicDTO user);
    }
}
