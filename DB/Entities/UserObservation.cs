namespace DB.Entities
{
    public class UserObservation
    {
        public int Id { get; set; }
        public User Observer { get; set; }
        public User Observed { get; set; }

        public UserObservation() { }
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
