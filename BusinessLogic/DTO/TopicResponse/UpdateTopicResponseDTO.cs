using Entities = DB.Entities;

namespace BusinessLogic.DTO.TopicResponse
{
    public class UpdateTopicResponseDTO
    {
        public string Description { get; set; }
        public Entities.File? Image { get; set; }

        public UpdateTopicResponseDTO() { }
        public UpdateTopicResponseDTO(string description, Entities.File? image)
        {
            Description = description;
            Image = image;
        }
    }
}
