using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class EventInterest 
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Event Event { get; set; }

        public EventInterest() { }
        public EventInterest(int id, User user, Event @event)
        {
            Id = id;
            User = user;
            Event = @event;
        }
        public EventInterest(User user, Event @event) : this(0, user, @event) { }
    }
}
