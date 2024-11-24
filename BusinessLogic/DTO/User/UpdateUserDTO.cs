using DB.Enums;
using System.ComponentModel.DataAnnotations;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.User
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        public UserGender? Gender { get; set; }
        [Range(1, 150, ErrorMessage = "Age must be between 1 and 150.")]
        public int? Age { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }
        public Entities.File? ProfileImage { get; set; }

        public UpdateUserDTO() { }
        public UpdateUserDTO(string id, UserGender? gender, int? age, string description, Entities.File? profileImage)
        {
            Id = id;
            Gender = gender;
            Age = age;
            Description = description;
            ProfileImage = profileImage;
        }
    }
}
