using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class UserObservation
    {
        public int Id { get; set; }
        public User Observer { get; set; }
        public User Observed { get; set; }

        public UserObservation(int id, User observer, User observed)
        {
            Id = id;
            Observer = observer;
            Observed = observed;
        }

        public UserObservation(User observer, User observed)
            : this(0, observer, observed) { }
    }
}
