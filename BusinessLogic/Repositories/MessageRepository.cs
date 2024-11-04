using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MVAppContext _context;
        public MessageRepository(MVAppContext context)
        {
            _context = context;
        }

        public void Add(Message message) { _context.Add(message); _context.SaveChanges(); }

        public List<Message> GetAll() => _context.Messages.ToList();

        public Message? GetOne(int id) => _context.Messages.FirstOrDefault(m => m.Id == id);

        public void Update(Message message) => _context.Messages.Update(message);
    }
}
