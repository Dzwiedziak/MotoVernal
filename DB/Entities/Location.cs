namespace DB.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Location() { }

        public Location(int id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Location(double latitude, double longitude)
            : this(0, latitude, longitude) { }
    }
}
