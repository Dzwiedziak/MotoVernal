namespace DB.Entities
{
    public class VehicleOfferObservation
    {
        public int Id { get; set; }

        public string ObserverId { get; set; } 
        public User Observer { get; set; }

        public int VehicleOfferId { get; set; } 
        public VehicleOffer VehicleOffer { get; set; }

        public VehicleOfferObservation() { }

        public VehicleOfferObservation(int id, User observer, VehicleOffer vehicleOffer)
        {
            Id = id;
            Observer = observer;
            VehicleOffer = vehicleOffer;
        }
    }
}
