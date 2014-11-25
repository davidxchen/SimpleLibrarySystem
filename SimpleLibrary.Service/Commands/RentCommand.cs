using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLibrary.Service.Interfaces;
using SimpleLibrary.Service.Models;
using SimpleLibrary.Common;

namespace SimpleLibrary.Service.Commands
{
    public class RentCommand: BaseService, IModelCommand<BookRentHistoryViewModel>
    {
        public RentCommand()
            :base()
        {

        }

        public bool Execute(BookRentHistoryViewModel model)
        {
            bool result = false;

            var userId = GetUserId(model.UserName);
            if (userId == 0)
            {
                return result;
            }

            var rent = base.LibraryContext.BookRentHistories.Create();

            rent.ISBN = model.ISBN;
            rent.RentOn = model.RentOn;
            rent.ReturnedOn = model.ReturnedOn;
            rent.UserId = model.UserId;

            if (rent.RentOn.Year > 1900)
            {
                UpdateBook(model.ISBN, model.RentOn);
            }
            if (rent.ReturnedOn.Year > 1900)
            {
                rent.RentOn = ReturnBook(model.ISBN);
            }

            base.LibraryContext.BookRentHistories.Add(rent);
            result = base.LibraryContext.SaveChanges() >= 0;

            return result;
        }

        private int GetUserId(string userName)
        {
            int userId = 0;

            dynamic entity;
            var entityList = base.LibraryContext.LibraryUsers.Where(u => u.UserName.CompareTo(userName) == 0);
            if (entityList.Any())
            {
                entity = entityList.FirstOrDefault();
                userId = entity.Id;
            }

            return userId;
        }

        private bool UpdateBook(string ISBN, DateTime lastRentOn)
        {
            bool result = false;

            dynamic book;
            var bookList = base.LibraryContext.Books.Where(b => b.ISBN.CompareTo(ISBN) == 0);
            if (bookList.Any())
            {
                book = bookList.FirstOrDefault();
                book.LastRentOn = lastRentOn;
                book.Status = (short)Enums.BookStatus.Rent;
                book.ModifiedOn = DateTime.Now;

                result = base.LibraryContext.SaveChanges() >= 0;
            }

            return result;
        }

        private DateTime ReturnBook(string ISBN)
        {
            DateTime rentOn = new DateTime(1900, 1, 1);

            dynamic book;
            var bookList = base.LibraryContext.Books.Where(b => b.ISBN.CompareTo(ISBN) == 0);
            if (bookList.Any())
            {
                book = bookList.FirstOrDefault();
                book.Status = (short)Enums.BookStatus.Returned;
                book.ModifiedOn = DateTime.Now;

                base.LibraryContext.SaveChanges();

                rentOn = book.LastRentOn;
            }

            return rentOn;
        }

        public async Task<bool> ExecuteAsync(BookRentHistoryViewModel model)
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
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
