using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleLibrary.Service.Interfaces;
using SimpleLibrary.Data;
using SimpleLibrary.Service.Models;

namespace SimpleLibrary.Service.Commands
{
    public class LibraryUserCommand : BaseService, IModelCommand<LibraryUserViewModel>
    {
        public LibraryUserCommand()
            : base()
        {

        }

        public bool Execute(LibraryUserViewModel model)
        {
            bool result = false, isNew = false;

            var entity = base.LibraryContext.LibraryUsers.Create();
            var entityList = base.LibraryContext.LibraryUsers.Where(u => u.UserName.CompareTo(model.UserName) == 0);
            if (entityList.Any())
            {
                entity = entityList.FirstOrDefault();
            }
            else
            {
                isNew = true;
            }

            entity.Status = (short)model.Status;

            if (isNew)
            {
                entity.UserName = model.UserName;
                entity.UserRole = model.UserRole;
                base.LibraryContext.LibraryUsers.Add(entity);
            }

            result = base.LibraryContext.SaveChanges() >= 0;

            return result;
        }

        public async Task<bool> ExecuteAsync(LibraryUserViewModel model)
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

            var entity = base.LibraryContext.LibraryUsers.Single(u => u.Id == id);
            if (entity != null)
            {
                var removedItem = base.LibraryContext.LibraryUsers.Remove(entity);
                result = removedItem.Id == id;
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
    }
}
