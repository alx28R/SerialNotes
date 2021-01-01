using SerialNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.ViewModels
{
    public class SerialsVM
    {
        public NotesSQL Notes { get; set; }
        public SerialsSQL Serial { get; set; }
        public List<SerialsSQL> Serials { get; set; }
    }
}
