using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SerialNotes
{
    interface IResponse
    {
        public int Status { get; set; }
        public string RenderHtml { get; set; }
    }
}
