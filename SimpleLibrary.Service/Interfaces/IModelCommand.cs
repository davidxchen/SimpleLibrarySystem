using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Interfaces
{
    public interface IModelCommand<TInput>
    {
        bool Execute(TInput model);
        Task<bool> ExecuteAsync(TInput model);
        bool Remove(int id);
        Task<bool> RemoveAsync(int id);
    }
}
