using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class Ban
    {
        public int Id { get; set; }
        public User Banned { get; set; }
        public User Banner { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string Reason { get; set; }
        public File? Image { get; set; }

        public Ban(){ }
        public Ban(int id, User banned, User banner, DateTime creationTime, DateTime expirationTime, string reason, File? image)
        {
            Id = id;
            Banned = banned;
            Banner = banner;
            CreationTime = creationTime;
            ExpirationTime = expirationTime;
            Reason = reason;
            Image = image;
        }

        public Ban(User banned, User banner, DateTime creationTime, DateTime expirationTime, string reason, File? image)
            : this(0, banned, banner, creationTime, expirationTime, reason, image) { }
    }
}
