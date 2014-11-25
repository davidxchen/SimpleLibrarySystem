using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Models
{
    public class BookRentHistoryViewModel
    {
        public int Id {get;set;}
        
        public string ISBN {get;set;}

        public string UserId {get;set;}

        public string UserName {get;set;}

        public DateTime RentOn {get;set;}

        public DateTime ReturnedOn {get;set;}
    }
}
