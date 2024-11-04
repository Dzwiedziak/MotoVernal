namespace DB.Entities
{
    public class Offer
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
        public bool IsReserved { get; set; }
        public string Email { get; set; }
        public Phone Phone { get; set; }
        public Price Price { get; set; }

        public Offer() { }

        public Offer(int id, string description, DateTime creationTime, Location location, User user, bool isReserved, string email, Phone phone, Price price)
        {
            Id = id;
            Description = description;
            CreationTime = creationTime;
            Location = location;
            User = user;
            IsReserved = isReserved;
            Email = email;
            Phone = phone;
            Price = price;
        }

        public Offer(string description, DateTime creationTime, Location location, User user, bool isReserved, string email, Phone phone, Price price)
            : this(0, description, creationTime, location, user, isReserved, email, phone, price) { }
    }
}
