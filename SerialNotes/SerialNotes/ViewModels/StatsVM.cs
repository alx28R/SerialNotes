using SerialNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes.ViewModels
{
    public class StatsVM
    {
        public Response Response { get; set; }
        public List<Stats> StatsListSerial { get; set; }
        public List<StatsAvg> StatsListAvg { get; set; }
    }
}
