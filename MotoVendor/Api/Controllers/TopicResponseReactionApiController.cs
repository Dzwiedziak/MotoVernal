using BusinessLogic.DTO.TopicResponseReaction;
using BusinessLogic.Errors;
using BusinessLogic.Services.Interfaces;
using DB.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MotoVendor.Api.Controllers
{
    [Route("api/topics/responses/reactions")]
    [ApiController]
    public class TopicResponseReactionApiController : ControllerBase
    {
        readonly ITopicResponseReactionService _topicResponseReactionService;

        public TopicResponseReactionApiController(ITopicResponseReactionService topicResponseReactionService)
        {
            _topicResponseReactionService = topicResponseReactionService;
        }

        [HttpPost]
        public IActionResult Add(AddTopicResponseReactionDTO addDto)
        {
            var result = _topicResponseReactionService.Add(addDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            switch (result.Error)
            {
                case TopicResponseReactionErrorCode.UserNotFound:
                    return StatusCode(409, "User not found");
                case TopicResponseReactionErrorCode.TopicResponseNotFound:
                    return StatusCode(409, "TopicResponseNotFound");
                case TopicResponseReactionErrorCode.RelationAlreadyExists:
                    return StatusCode(409, "Relation Already Exists");
                default:
                    return StatusCode(500, "Unknown error happened");
            }
        }
        [HttpPatch]
        [Route("/{id}/type")]
        public IActionResult UpdateReactionType([FromRoute] int id, ReactionType reaction)
        {
            var result = _topicResponseReactionService.UpdateReactionType(id, reaction);
            switch (result)
            {
                case null:
                    return Ok(result);
                case TopicResponseReactionErrorCode.TopicResponseNotFound:
                    return NotFound();
                default:
                    return StatusCode(500, "Unknown error happened");
            }
        }
    }
}
