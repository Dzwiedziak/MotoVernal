using DB.Enums;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Decorators;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.Event
{
    public class AddEventDTO
    {
        [Required(ErrorMessage = "Publisher is required.")]
        public Entities.User Publisher { get; set; }
        [Required(ErrorMessage = "Event type is required.")]
        [EnumDataType(typeof(EventType), ErrorMessage = "Invalid event type.")]
        public EventType EventType { get; set; }
        [Required(ErrorMessage = "Start date is required.")]
        [FutureDate(ErrorMessage = "Start date must be in the future.")]
        public DateTime TimeFrom { get; set; }
        [Required(ErrorMessage = "End date is required.")]
        [FutureDate(ErrorMessage = "End date must be in the future.")]
        [DateGreaterThan(nameof(TimeFrom), ErrorMessage = "End date must be after start date.")]
        public DateTime TimeTo { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Description must be between 10 and 1000 characters.")]
        public string Description { get; set; }
        public Entities.File? Image { get; set; }

        public AddEventDTO() { }
        public AddEventDTO(Entities.User publisher, EventType eventType, DateTime timeFrom, DateTime timeTo, string description, Entities.File? image)
        {
            Publisher = publisher;
            EventType = eventType;
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Description = description;
            Image = image;
        }
    }
}
