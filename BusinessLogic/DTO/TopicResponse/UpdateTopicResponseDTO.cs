namespace BusinessLogic.DTO.TopicResponse
{
    public class UpdateTopicResponseDTO
    {
        public string Description { get; set; }

        public UpdateTopicResponseDTO(string description)
        {
            Description = description;
        }
    }
}
