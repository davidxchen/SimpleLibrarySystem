using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Common
{
    public class Enums
    {
        public enum UserStatus
        {
            Inactive,
            Active
        }

        public enum BookStatus
        {
            Added,
            Rent,
            Returned
        }
    }
}
