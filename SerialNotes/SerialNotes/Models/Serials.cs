using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.Models
{
    public class Serials
    {
        [Key]
        public int SerialId { get; set; }
        public string SerialName { get; set; }
        public string Country { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Producer { get; set; }
        public string SerialDescription { get; set; }
    }
}
