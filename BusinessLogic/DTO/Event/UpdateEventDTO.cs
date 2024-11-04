namespace BusinessLogic.DTO.Event
{
    public class UpdateEventDTO
    {
        public DateTime TimeFrom { get; set; }
        public DateTime TimeTo { get; set; }
        public string Description { get; set; }

        public UpdateEventDTO(DateTime timeFrom, DateTime timeTo, string description)
        {
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Description = description;
        }
    }
}
