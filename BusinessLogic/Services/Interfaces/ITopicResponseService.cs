using BusinessLogic.DTO.TopicResponse;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITopicResponseService
    {
        Result<int?, TopicErrorCode> Add(AddTopicResponseDTO topicResponse);
        Result<GetTopicResponseDTO, TopicErrorCode> Get(int id);
        TopicErrorCode? Update(int id, UpdateTopicResponseDTO topicResponse);
    }
}
