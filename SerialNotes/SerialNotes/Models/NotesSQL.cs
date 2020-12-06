using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.Models
{
    public class NotesSQL
    {
        [Key]
        public int NoteId { get; set; }

        [Display(Name = "заголовок")]
        public string NoteTitle { get; set; }
        public int SerialId { get; set; }
        [Display(Name = "название сериала")]
        public string SerialName { get; set; }
        public int PartId { get; set; }
        [Display(Name = "часть")]
        public int Part { get; set; }
        [Display(Name = "сезон")]
        public int Season { get; set; }
        [Display(Name = "дата добавления")]
        public DateTime DateAdding { get; set; }
        [Display(Name = "заметка")]
	    public string Comment { get; set; }
        [Display(Name = "оценка")]
        public double Rating { get; set; }
    }
}
