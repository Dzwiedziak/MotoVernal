using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.User
{
    public class GetUserDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserGender? Gender { get; set; }
        public int? Age { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public Entities.File? ProfileImage { get; set; }

        public GetUserDTO(string userName, string email, UserGender? gender, int? age, DateTime creationTime, string description, Entities.File? profileImage)
        {
            UserName = userName;
            Email = email;
            Gender = gender;
            Age = age;
            CreationTime = creationTime;
            Description = description;
            ProfileImage = profileImage;
        }
    }
}
