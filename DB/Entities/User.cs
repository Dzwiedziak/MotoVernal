using DB.Enums;
using Microsoft.AspNetCore.Identity;

namespace DB.Entities
{
    public class User : IdentityUser
    {
        public UserGender? Gender { get; set; }
        public int? Age { get; set; }
        public DateTime CreationTime { get; set; }
        public string? Description { get; set; }
        public File? ProfileImage { get; set; }

        public User() { }

        public User(string userName, string email, UserGender? gender, int? age, DateTime creationTime, string? description)
        {
            UserName = userName;
            Email = email;
            Gender = gender;
            Age = age;
            CreationTime = creationTime;
            Description = description;
        }
    }
}
