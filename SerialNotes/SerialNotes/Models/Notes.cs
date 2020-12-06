using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.Models
{
    public class Notes
    {
        [Key]
        public int NoteId { get; set; }
        public string NoteTitle { get; set; }
        public int PartId { get; set; }
        public DateTime DateAdding { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
    }
}
