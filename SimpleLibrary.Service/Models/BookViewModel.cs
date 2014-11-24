using SimpleLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Models
{
    public class BookViewModel
    {
        public string ISBN { get; set; }

        public string BookName { get; set; }

        public byte[] Cover { get; set; }

        public string Description { get; set; }

        public Enums.BookStatus Status { get; set; }

        public DateTime LastRentOn { get; set; }
    }
}
