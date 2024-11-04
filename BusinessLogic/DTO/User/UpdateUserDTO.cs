using DB.Enums;

namespace BusinessLogic.DTO.User
{
    public class UpdateUserDTO
    {
        public UserGender? Gender { get; set; }
        public int? Age { get; set; }
        public string Description { get; set; }
    }
}
