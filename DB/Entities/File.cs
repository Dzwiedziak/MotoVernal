﻿using System.ComponentModel.DataAnnotations.Schema;

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
