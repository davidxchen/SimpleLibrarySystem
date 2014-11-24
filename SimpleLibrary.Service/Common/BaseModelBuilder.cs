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
        public SimpLibSysContainer LibraryContext
        {
            get;
            set;
        }

        public BaseModelBuilder()
        {
            this.LibraryContext = new SimpLibSysContainer();
        }

        public void Dispose()
        {
            this.LibraryContext.Dispose();
        }
    }
}
