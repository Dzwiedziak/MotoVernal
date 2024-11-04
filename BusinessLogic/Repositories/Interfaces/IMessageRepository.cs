using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        List<Message> GetAll();
        Message? GetOne(int id);
        void Add(Message message);
        void Update(Message message);
    }
}
