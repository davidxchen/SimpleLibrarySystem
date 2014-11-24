using SimpleLibrary.Common;
using SimpleLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service
{
    public class BaseModelBuilder : IDisposable
    {
        public SimpLibSysContext LibraryContext
        {
            get;
            set;
        }

        public BaseModelBuilder()
        {
            this.LibraryContext = new SimpLibSysContext();
        }

        public void Dispose()
        {
            this.LibraryContext.Dispose();
        }
    }

    public class SimpLibSysContext : DbContext
    {
        public SimpLibSysContext()
            : base(Config.DBConnectionString)
        {
        }

        public DbSet<LibraryUsers> LibraryUsers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookRentHistory> BookRentHistories { get; set; }
    }
}
