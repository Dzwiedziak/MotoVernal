using DB.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.Bug
{
    public class GetReportBugDTO
    {
        public int Id { get; set; }
        public DB.Entities.User Reporter { get; set; }
        public string Description { get; set; }
        public DB.Entities.File? Image { get; set; }
        public BugType BugType { get; set; }
        public BugState BugState { get; set; }

        public GetReportBugDTO() { }
        public GetReportBugDTO(int id, DB.Entities.User reporter, string description, DB.Entities.File? image, BugType bugType, BugState bugState)
        {
            Id = id;
            Reporter = reporter;
            Description = description;
            Image = image;
            BugType = bugType;
            BugState = bugState;
        }
    }
}
