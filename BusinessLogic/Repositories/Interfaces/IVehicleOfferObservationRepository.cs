using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IVehicleOfferObservationRepository
    {
        void Add(VehicleOfferObservation vehicleOfferObservation);
        void Delete(VehicleOfferObservation vehicleOfferObservation);
        List<VehicleOfferObservation> GetAll();
    }
}
