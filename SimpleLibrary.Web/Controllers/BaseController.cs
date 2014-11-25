using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SimpleLibrary.Service.ModelBuilders;
using SimpleLibrary.Service.Models;

namespace SimpleLibrary.Web.Controllers
{
    public class BaseController : Controller
    {
        public async Task<LibraryUserViewModel> CurrentLogonUserAsync()
        {
            var userName = User.Identity.Name;

            var userBuilder = new LibraryUserModelBuilder();
            var user = await userBuilder.BuildModelFromAsync(userName);

            return user;
        }
    }
}