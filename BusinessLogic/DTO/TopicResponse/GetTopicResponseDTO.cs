using Entities = DB.Entities;

namespace BusinessLogic.DTO.TopicResponse
{
    public class GetTopicResponseDTO
    {
        public int Id { get; set; }
        public Entities.Topic Topic { get; set; }
        public Entities.User Owner { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public Entities.File? Image { get; set; }
        public Entities.TopicResponseReaction? UserResponse { get; set; }
        public int? LikeCount { get; set; }
        public int? DisLikeCount { get; set; }   

        public GetTopicResponseDTO(int id, Entities.Topic topic, Entities.User owner, string description, DateTime creationTime, Entities.File? image, Entities.TopicResponseReaction userResponse, int? likeCount, int? disLikeCount)
        {
            Id = id;
            Topic = topic;
            Owner = owner;
            Description = description;
            CreationTime = creationTime;
            Image = image;
            UserResponse = userResponse;
            LikeCount = likeCount;
            DisLikeCount = disLikeCount;
        }
    }
}
