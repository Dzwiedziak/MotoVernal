using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO.User
{
    public class AddUserDTO
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }

        public AddUserDTO() { }

        public AddUserDTO(string nickname, string email, string password)
        {
            UserName = nickname;
            Email = email;
            Password = password;
        }
    }
}
