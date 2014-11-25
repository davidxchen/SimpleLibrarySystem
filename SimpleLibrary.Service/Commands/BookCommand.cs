using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLibrary.Service.Interfaces;
using SimpleLibrary.Service.Models;

namespace SimpleLibrary.Service.Commands
{
    public class BookCommand : BaseService, IModelCommand<BookViewModel>
    {
        public BookCommand()
            : base()
        {

        }

        public bool Execute(BookViewModel model)
        {
            bool result = false, isNew = false;

            var book = base.LibraryContext.Books.Create();
            var bookList = base.LibraryContext.Books.Where(b => b.ISBN.CompareTo(model.ISBN) == 0);
            if (bookList.Any())
            {
                book = bookList.FirstOrDefault();
            }
            else
            {
                isNew = true;

                book.ISBN = model.ISBN;
                book.LastRentOn = new DateTime(1900, 1, 1);
            }

            book.BookName = model.BookName;
            book.Description = model.Description;

            if (isNew)
            {
                book.CreatedOn = DateTime.Now;
                book.ModifiedOn = DateTime.Now;

                base.LibraryContext.Books.Add(book);
            }
            else
            {
                book.ModifiedOn = DateTime.Now;
            }

            result = base.LibraryContext.SaveChanges() >= 0;

            return result;
        }

        public async Task<bool> ExecuteAsync(BookViewModel model)
        {
            bool result = false;

            await Task.Run(() =>
                {
                    result = Execute(model);
                });

            return result;
        }

        public bool Remove(int id)
        {
            bool result = false;

            var entity = base.LibraryContext.Books.Single(u => u.Id == id);
            if (entity != null)
            {
                var removedItem = base.LibraryContext.Books.Remove(entity);
                result = removedItem.Id == id;

                if (result)
                {
                    RemoveHistory(removedItem.ISBN);
                }

                base.LibraryContext.SaveChanges();
            }

            return result;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            bool result = false;

            await Task.Run(() =>
            {
                result = Remove(id);
            });

            return result;
        }

        private void RemoveHistory(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                return;
            }

            var historyList = base.LibraryContext.BookRentHistories.Where(h => h.ISBN.CompareTo(isbn) == 0);
            if (historyList.Any())
            {
                base.LibraryContext.BookRentHistories.RemoveRange(historyList);
            }
        }
    }
}
