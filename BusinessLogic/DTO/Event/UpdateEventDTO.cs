using Entities = DB.Entities;

namespace BusinessLogic.DTO.Event
{
    public class UpdateEventDTO
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Description { get; set; }
        public Entities.File? Image { get; set; }

        public UpdateEventDTO(DateTime timeFrom, DateTime timeTo, string description, Entities.File? image)
        {
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Description = description;
            Image = image;
        }
    }
}
