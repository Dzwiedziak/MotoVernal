using DB.Enums;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.User
{
    public class GetUserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserGender? Gender { get; set; }
        public int? Age { get; set; }
        public DateTime CreationTime { get; set; }
        public string Description { get; set; }
        public Entities.File? ProfileImage { get; set; }

        public GetUserDTO(string id,string userName, string email, UserGender? gender, int? age, DateTime creationTime, string description, Entities.File? profileImage)
        {
            Id = id;
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
