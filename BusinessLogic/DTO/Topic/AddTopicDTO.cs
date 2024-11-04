using Entities = DB.Entities;

namespace BusinessLogic.DTO.Topic
{
    public class AddTopicDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Entities.User Publisher { get; set; }
        public Entities.Section Section { get; set; }
    }
}
