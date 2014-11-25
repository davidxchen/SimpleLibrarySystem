using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLibrary.Service.Interfaces;
using SimpleLibrary.Service.Models;
using SimpleLibrary.Data;

namespace SimpleLibrary.Service.ModelBuilders
{
    public class LibraryUserModelBuilder : BaseService, IModelBuilder<LibraryUserViewModel, LibraryUsers>
    {
        public int RecordCount
        {
            get;
            set;
        }

        public LibraryUserModelBuilder()
            : base()
        {

        }

        private LibraryUserViewModel ModelMapping(LibraryUsers entity)
        {
            var model = new LibraryUserViewModel();

            if (entity != null)
            {
                model.Id = entity.Id;
                model.UserName = entity.UserName;
                model.RentBooks = entity.BookRentHistories.Where(h => h.ReturnedOn.Year == 1900).Select(h => new BookViewModel
                {
                    ISBN = h.Book.ISBN,
                    BookName = h.Book.BookName
                }).ToList();
            }
            else
            {
                model.RentBooks = new List<BookViewModel>();
            }

            return model;
        }

        public LibraryUserViewModel BuildModelFrom(int id)
        {
            dynamic entity = null;
            var entityList = base.LibraryContext.LibraryUsers.Where(u => u.Id == id);
            if (entityList.Any())
            {
                entity = entityList.FirstOrDefault();
            }

            var model = ModelMapping(entity);

            return model;
        }

        public async Task<LibraryUserViewModel> BuildModelFromAsync(int id)
        {
            var model = new LibraryUserViewModel();

            await Task.Run(() =>
            {
                model = BuildModelFrom(id);
            });

            return model;
        }

        public LibraryUserViewModel BuildModelFrom(string userName)
        {
            var entity = base.LibraryContext.LibraryUsers.Create();
            var entityList = base.LibraryContext.LibraryUsers.Where(u => u.UserName.CompareTo(userName) == 0);
            if (entityList.Any())
            {
                entity = entityList.FirstOrDefault();
            }

            var model = ModelMapping(entity);

            return model;
        }

        public async Task<LibraryUserViewModel> BuildModelFromAsync(string userName)
        {
            var model = new LibraryUserViewModel();

            await Task.Run(() =>
            {
                model = BuildModelFrom(userName);
            });

            return model;
        }

        public LibraryUserViewModel Rebuild(LibraryUserViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<LibraryUserViewModel> RebuildAsync(LibraryUserViewModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LibraryUserViewModel> CreateModelList(Func<LibraryUsers, bool> predicate, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LibraryUserViewModel>> CreateModelListAsync(Func<LibraryUsers, bool> predicate, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
