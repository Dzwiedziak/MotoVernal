using BusinessLogic.DTO.TopicResponseReaction;
using BusinessLogic.Errors;
using BusinessLogic.Services.Response;
using DB.Enums;

namespace BusinessLogic.Services.Interfaces
{
    public interface ITopicResponseReactionService
    {
        Result<int?, TopicResponseReactionErrorCode> Add(AddTopicResponseReactionDTO addDTO);
        TopicResponseReactionErrorCode? UpdateReactionType(int id, ReactionType reactionType);
    }
}
