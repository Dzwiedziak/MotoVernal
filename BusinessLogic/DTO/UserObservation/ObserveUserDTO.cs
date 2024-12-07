using Entities = DB.Entities;

namespace BusinessLogic.DTO.UserObservation
{
    public class ObserveUserDTO
    {
        public Entities.User Observer { get; set; }
        public Entities.User Observed { get; set; }

        public ObserveUserDTO() { }
        public ObserveUserDTO(Entities.User observer, Entities.User observed)
        {
            Observer = observer;
            Observed = observed;
        }
    }
}
