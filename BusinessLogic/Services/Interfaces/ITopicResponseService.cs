using BusinessLogic.DTO.TopicResponse;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITopicResponseService
    {
        Result<int?, TopicErrorCode> Add(AddTopicResponseDTO topicResponse);
        Result<GetTopicResponseDTO, TopicErrorCode> Get(int id);
        TopicResponse GetOne(int Id);
        List<GetTopicResponseDTO> GetAllResponsesInTopic(int id);
        TopicErrorCode? Update(int id, UpdateTopicResponseDTO topicResponse);
        void Delete(int Id);
    }
}
