using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO.User
{
    public class LoginUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public LoginUserDTO(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
