using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IBanRepository
    {
        List<Ban> GetAll();
        Ban? GetOne(int id);
        void Add(Ban ban);
        void Update(Ban ban);
    }
}
