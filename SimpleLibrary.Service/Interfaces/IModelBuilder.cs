using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.Interfaces
{
    public interface IModelBuilder<TViewModel, TEntity>
    {
        int RecordCount { get; set; }

        TViewModel BuildModelFrom(int id);
        Task<TViewModel> BuildModelFromAsync(int id);
        TViewModel Rebuild(TViewModel model);
        Task<TViewModel> RebuildAsync(TViewModel model);
        IEnumerable<TViewModel> CreateModelList(Func<TEntity, int, bool> predicate, int pageNumber, int pageSize);
        Task<IEnumerable<TViewModel>> CreateModelListAsync(Func<TEntity, int, bool> predicate, int pageNumber, int pageSize);
    }
}
