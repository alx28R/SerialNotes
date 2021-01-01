using SerialNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.ViewModels
{
    public class NotesVM
    {
        public Search Search { get; set; }
        public IEnumerable<NotesSQL> ListNotes { get; set; }
        public IEnumerable<SerialsSQL> Serials { get; set; }

    }
}
