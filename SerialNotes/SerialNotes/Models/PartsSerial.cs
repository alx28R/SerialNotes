using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.Models
{
    [Table("PartsSerial")]
    public class PartsSerial
    {
        [Key]
        public int PartId { get; set; }
        public int Part { get; set; }
        public int Season { get; set; }
        public bool IsViewed { get; set; }

        [Column("SerialId")]
        public int SerialId { get; set; }
        public SerialsSQL Serial { get; set; }

    }
}
