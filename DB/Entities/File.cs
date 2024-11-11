using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class File
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Base64 { get; set; }
        public string Extension { get; set; }
    }
}
