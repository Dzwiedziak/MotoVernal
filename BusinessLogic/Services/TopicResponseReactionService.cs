using BusinessLogic.DTO.TopicResponseReaction;
using BusinessLogic.Errors;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogic.Services.Response;
using DB.Entities;
using DB.Enums;


namespace BusinessLogic.Services
{
    public class TopicResponseReactionService : ITopicResponseReactionService
    {
        readonly ITopicResponseReactionRepository _repository;
        readonly ITopicResponseRepository _topicResponseRepository;
        readonly IUserRepository _userRepository;

        public TopicResponseReactionService(ITopicResponseReactionRepository repository, ITopicResponseRepository topicResponseRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _topicResponseRepository = topicResponseRepository;
            _userRepository = userRepository;
        }

        public Result<int?, TopicResponseReactionErrorCode> Add(AddTopicResponseReactionDTO addDTO)
        {
            var user = _userRepository.GetOne(addDTO.UserId);
            if (user == null)
                return TopicResponseReactionErrorCode.UserNotFound;

            var topicResponse = _topicResponseRepository.GetOne(addDTO.TopicResponseId);
            if (topicResponse == null)
                return TopicResponseReactionErrorCode.TopicResponseNotFound;

            var dbEntitiy = _repository.FindWhereUserAndTopicResponse(user, topicResponse);
            if (dbEntitiy != null)
                return TopicResponseReactionErrorCode.RelationAlreadyExists;

            var topicResponseReaction = new TopicResponseReaction(0, user, topicResponse, addDTO.ReactionType);
            _repository.Add(topicResponseReaction);
            return topicResponseReaction.Id;
        }

        public Result<TopicResponseReaction?, TopicResponseReactionErrorCode> FindWhere(string userId, int topicResponseId)
        {
            var user = _userRepository.GetOne(userId);
            if (user == null)
                return TopicResponseReactionErrorCode.UserNotFound;

            var topicResponse = _topicResponseRepository.GetOne(topicResponseId);
            if (topicResponse == null)
                return TopicResponseReactionErrorCode.TopicResponseNotFound;

            return _repository.FindWhereUserAndTopicResponse(user, topicResponse);
        }

        public int GetDisLikeCount(int topicResponseId)
        {
            var dbEntities = _repository.GetAll();
            var resultList = dbEntities.Where(e => e.TopicResponse.Id == topicResponseId && e.ReactionType == ReactionType.Dislike).ToList();
            return resultList.Count;
        }

        public int GetLikeCount(int topicResponseId)
        {
            var dbEntities = _repository.GetAll();
            var resultList = dbEntities.Where(e => e.TopicResponse.Id == topicResponseId && e.ReactionType == ReactionType.Like).ToList();
            return resultList.Count;
        }

        public TopicResponseReactionErrorCode? UpdateReactionType(int id, ReactionType reactionType)
        {
            var dbEntity = _repository.Get(id);
            if (dbEntity == null)
                return TopicResponseReactionErrorCode.TopicResponseNotFound;
            dbEntity.ReactionType = reactionType;
            _repository.Update(dbEntity);
            return null;
        }
    }
}
