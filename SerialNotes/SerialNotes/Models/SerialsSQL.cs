using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.Models
{
    [Table("Serials")]
    public class SerialsSQL
    {
        [Key][Column("SerialId")]
        public int SerialId { get; set; }
        [Display(Name = "название")][Column("SerialName")]
        public string SerialName { get; set; }
        [Display(Name = "страна")][Column("Country")]
        public string Country { get; set; }
        [Display(Name = "Премьера")][Column("ReleaseDate")]
        public DateTime? ReleaseDate { get; set; }
        [Display(Name = "продюсер")][Column("Producer")]
        public string Producer { get; set; }
        [Display(Name = "описание")][Column("SerialDescription")]
        public string SerialDescription { get; set; }

        [BindNever]
        public List<PartsSerial> PartsSerials { get; set; } = new List<PartsSerial>();
    }
}
