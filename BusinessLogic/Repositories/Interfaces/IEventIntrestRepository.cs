using BusinessLogic.DTO.EventInterest;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IEventIntrestRepository
    {
        List<EventInterest> GetAll();
        EventInterest? GetOne(int id);
        void Add(AddEventInterestDTO eventInterest);
    }
}
