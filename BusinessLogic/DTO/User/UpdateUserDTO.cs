using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.User
{
    public class UpdateUserDTO
    {
        public UserGender? Gender { get; set; }
        public int? Age { get; set; }
        public string Description { get; set; }
        public Entities.File? ProfileImage { get; set; }

        public UpdateUserDTO(UserGender? gender, int? age, string description, Entities.File? profileImage)
        {
            Gender = gender;
            Age = age;
            Description = description;
            ProfileImage = profileImage;
        }
    }
}
