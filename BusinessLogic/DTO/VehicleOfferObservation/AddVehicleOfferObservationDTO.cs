using Entities = DB.Entities;

namespace BusinessLogic.DTO.VehicleOfferObservation
{
    public class AddVehicleOfferObservationDTO
    {
        public Entities.User Observer { get; set; }
        public Entities.VehicleOffer VehicleOffer { get; set; }

        public AddVehicleOfferObservationDTO() { }
        public AddVehicleOfferObservationDTO(Entities.User observer, Entities.VehicleOffer vehicleOffer)
        {
            Observer = observer;
            VehicleOffer = vehicleOffer;
        }
    }
}
