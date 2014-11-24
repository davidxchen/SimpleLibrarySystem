using SimpleLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserRole { get; set; }

        public Enums.UserStatus Status { get; set; }

        public List<BookViewModel> RentBooks { get; set; }
    }
}
