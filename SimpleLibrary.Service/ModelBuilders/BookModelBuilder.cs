using SimpleLibrary.Common;
using SimpleLibrary.Data;
using SimpleLibrary.Service.Interfaces;
using SimpleLibrary.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Service.ModelBuilders
{
    public class BookModelBuilder : BaseModelBuilder, IModelBuilder<BookViewModel, Books>
    {
        public BookModelBuilder()
            : base()
        {

        }

        private BookViewModel ModelMapping(Books entity)
        {
            var model = new BookViewModel();

            if (entity != null)
            {
                model.ISBN = entity.ISBN;
                model.BookName = entity.BookName;
                model.Cover = entity.Cover;
                model.Description = entity.Description;
                model.Status = (Enums.BookStatus)Enum.Parse(typeof(Enums.BookStatus), entity.Status.ToString(), true);
                model.LastRentOn = entity.LastRentOn;
            }

            return model;
        }

        public BookViewModel BuildModelFrom(int id)
        {
            var entity = base.LibraryContext.Books.Single(b => b.Id == id);

            var model = ModelMapping(entity);

            return model;
        }

        public async Task<BookViewModel> BuildModelFromAsync(int id)
        {
            var model = new BookViewModel();

            await Task.Run(() =>
                {
                    model = BuildModelFrom(id);
                });

            return model;
        }

        public BookViewModel Rebuild(BookViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BookViewModel> RebuildAsync(BookViewModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookViewModel> CreateModelList(Func<Books, bool> predicate, int pageNumber, int pageSize)
        {
            if (pageSize <= 0)
            {
                pageSize = 20;
            }

            pageSize = pageNumber <= 0 ? pageSize : pageSize * pageNumber;

            var bookList = base.LibraryContext.Books.Where(predicate);

            this.RecordCount = bookList.Count();

            bookList = bookList.OrderBy(b => b.BookName).Skip(pageNumber).Take(pageSize);

            var modelList = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                modelList.Add(ModelMapping(book));
            }

            return modelList;
        }

        public async Task<IEnumerable<BookViewModel>> CreateModelListAsync(Func<Books, bool> predicate, int pageNumber, int pageSize)
        {
            IEnumerable<BookViewModel> modelList = new List<BookViewModel>();

            await Task.Run(() =>
            {
                modelList = CreateModelList(predicate, pageNumber, pageSize);
            });

            return modelList;
        }

        public int RecordCount
        {
            get;
            set;
        }
    }
}
