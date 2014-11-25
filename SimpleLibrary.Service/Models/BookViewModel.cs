using SimpleLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Models
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ISBN { get; set; }

        public string BookName { get; set; }

        public byte[] Cover { get; set; }

        public string Description { get; set; }

        public Enums.BookStatus Status { get; set; }

        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public DateTime LastRentOn { get; set; }

        public List<BookRentHistoryViewModel> RentHistoryList { get; set; }
    }
}
