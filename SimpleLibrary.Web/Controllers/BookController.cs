using SimpleLibrary.Service.ModelBuilders;
using SimpleLibrary.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        // id == ISBN
        public ActionResult Detail(string id)
        {
            var model = new BookViewModel();
            var bookBuilder = new BookModelBuilder();
            var modelList = bookBuilder.CreateModelList(b => b.ISBN.StartsWith(id), 0, 1);

            if (modelList.Any())
            {
                model = modelList.FirstOrDefault();
            }

            return View(model);
        }
    }
}