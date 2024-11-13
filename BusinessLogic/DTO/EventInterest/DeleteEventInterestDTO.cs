using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = DB.Entities;

namespace BusinessLogic.DTO.EventInterest
{
    public class DeleteEventInterestDTO
    {
        public Entities.User User { get; set; }
        public Entities.Event Event { get; set; }

        public DeleteEventInterestDTO(Entities.User user, Entities.Event @event)
        {
            User = user;
            Event = @event;
        }
    }
}
