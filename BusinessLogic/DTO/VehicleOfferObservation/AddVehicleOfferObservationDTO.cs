namespace BusinessLogic.DTO.VehicleOfferObservation
{
    public class AddVehicleOfferObservationDTO
    {
        public string UserId { get; set; }
        public int OfferId { get; set; }

        public AddVehicleOfferObservationDTO() { }

        public AddVehicleOfferObservationDTO(string userId, int offerId)
        {
            UserId = userId;
            OfferId = offerId;
        }
    }
}
