using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Models
{
    public class BookRentHistoryViewModel
    {
        public int Id {get;set;}
        
        public string ISBN {get;set;}

        public int UserId {get;set;}

        public string UserName {get;set;}

        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy h:mm tt}")]
        public DateTime RentOn {get;set;}

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy h:mm tt}")]
        public DateTime ReturnedOn {get;set;}
    }
}
