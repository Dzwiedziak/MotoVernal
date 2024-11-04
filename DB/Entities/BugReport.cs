using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class BugReport
    {
        public int Id { get; set; }
        public User Reporter {  get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }

        public BugReport(int id, User reporter, string description, DateTime creationTime)
        {
            Id = id;
            Reporter = reporter;
            Description = description;
            CreationTime = creationTime;
        }

        public BugReport(User reporter, string description, DateTime creationTime)
            : this(0, reporter, description, creationTime) { }
    }
}
