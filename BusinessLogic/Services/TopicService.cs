using BusinessLogic.DTO.Topic;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;

namespace BusinessLogic.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }

        public Result<int?, TopicErrorCode> Add(AddTopicDTO topic)
        {
            var newTopic = CreateNewTopic(topic);
            _topicRepository.Add(newTopic);
            return newTopic.Id;
        }

        public Result<GetTopicDTO, TopicErrorCode> Get(int id)
        {
            Topic? dbTopic = _topicRepository.GetOne(id);
            if (dbTopic is null)
                return TopicErrorCode.TopicNotFound;

            return CreateGetTopicDTO(dbTopic);
        }

        public TopicErrorCode? Update(int id, UpdateTopicDTO topic)
        {
            Topic? dbTopic = _topicRepository.GetOne(id);
            if (dbTopic is null)
                return TopicErrorCode.TopicNotFound;

            UpdateTopic(ref dbTopic, topic);
            _topicRepository.Update(dbTopic);
            return null;
        }

        private Topic CreateNewTopic(AddTopicDTO topic) =>
            new(topic.Title, topic.Description, DateTime.Now, topic.Publisher, topic.Section);

        private GetTopicDTO CreateGetTopicDTO(Topic topic) =>
            new(topic.Title, topic.Description, topic.CreationTime, topic.Publisher, topic.Section);

        private void UpdateTopic(ref Topic oldTopic, UpdateTopicDTO topic)
        {
            oldTopic.Title = topic.Title;
            oldTopic.Description = topic.Description;
        }
    }
}
