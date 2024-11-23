using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.User
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }
        public UserGender? Gender { get; set; }
        public int? Age { get; set; }
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
