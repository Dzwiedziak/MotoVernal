using BusinessLogic.DTO.TopicResponse;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class TopicResponseService : ITopicResponseService
    {
        private readonly ITopicResponseRepository _topicResponseRepository;
        private readonly ITopicResponseReactionRepository _reactionRepository;

        public TopicResponseService(ITopicResponseRepository responseRepository)
        {
            _topicResponseRepository = responseRepository;
        }

        public Result<int?, TopicErrorCode> Add(AddTopicResponseDTO topicResponse)
        {
            var newTopicResponse = CreateNewTopicResponse(topicResponse);
            _topicResponseRepository.Add(newTopicResponse);
            return newTopicResponse.Id;
        }

        public Result<GetTopicResponseDTO, TopicErrorCode> Get(int id)
        {
            TopicResponse? dbTopicResponse = _topicResponseRepository.GetOne(id);
            if (dbTopicResponse is null)
                return TopicErrorCode.TopicNotFound;

            return CreateGetTopicResponseDTO(dbTopicResponse);
        }
        public TopicResponse GetOne(int Id)
        {
            return _topicResponseRepository.GetOne(Id);
        }
        public void Delete(int Id)
        {
            TopicResponse topicR = _topicResponseRepository.GetOne(Id);
            _topicResponseRepository.Delete(topicR);
        }
        public List<GetTopicResponseDTO> GetAllResponsesInTopic(int id)
        {

            List<TopicResponse> allResponses = _topicResponseRepository.GetAll();
            List<TopicResponse> topicResponses = allResponses.Where(r => r.Topic.Id == id).ToList();

            return topicResponses.Select(r => CreateGetTopicResponseDTO(r)).ToList();
        }

        public TopicErrorCode? Update(int id, UpdateTopicResponseDTO topicResponse)
        {
            TopicResponse? dbTopicResponse = _topicResponseRepository.GetOne(id);
            if (dbTopicResponse is null)
                return TopicErrorCode.TopicNotFound;

            UpdateTopicResponse(ref dbTopicResponse, topicResponse);
            _topicResponseRepository.Update(dbTopicResponse);
            return null;
        }

        private TopicResponse CreateNewTopicResponse(AddTopicResponseDTO topicResponse) =>
            new(topicResponse.Topic, topicResponse.Owner, topicResponse.Description, DateTime.Now, topicResponse.Image);

        private GetTopicResponseDTO CreateGetTopicResponseDTO(TopicResponse topicResponse) =>
            new(topicResponse.Id, topicResponse.Topic, topicResponse.Owner, topicResponse.Description, topicResponse.CreationTime, topicResponse.Image, null, null, null);

        private void UpdateTopicResponse(ref TopicResponse oldTopicResponse, UpdateTopicResponseDTO topicResponse)
        {
            oldTopicResponse.Description = topicResponse.Description;
            oldTopicResponse.Image = topicResponse.Image;
        }
    }
}
