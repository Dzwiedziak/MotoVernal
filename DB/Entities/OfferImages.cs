using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Entities
{
    public class OfferImages
    {
        public int Id { get; set; }
        public Offer Offer { get; set; }
        public File Image { get; set; }
    }
}
