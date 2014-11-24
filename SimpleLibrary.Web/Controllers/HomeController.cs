using SimpleLibrary.Service.ModelBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var bookBuilder = new BookModelBuilder();
            var model = bookBuilder.CreateModelList(b => !string.IsNullOrEmpty(b.BookName), 0, 20);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}