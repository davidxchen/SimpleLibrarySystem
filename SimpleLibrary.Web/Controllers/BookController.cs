using SimpleLibrary.Service.ModelBuilders;
using SimpleLibrary.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SimpleLibrary.Service.Commands;
using SimpleLibrary.Common;

namespace SimpleLibrary.Web.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            var bookBuilder = new BookModelBuilder();
            var model = bookBuilder.CreateModelList(b => !string.IsNullOrEmpty(b.BookName), 0, 20);

            return View(model);
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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool existed = CheckISBNExisted(model.ISBN);
                if (existed)
                {
                    ModelState.AddModelError("", "The ISBN exists!");
                    return View();
                }

                var bookCommand = new BookCommand();
                bool result = await bookCommand.ExecuteAsync(model);
                if (!result)
                {
                    ModelState.AddModelError("", "");
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public bool CheckISBNExisted(string ISBN)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(ISBN))
            {
                var bookBuilder = new BookModelBuilder();
                var books = bookBuilder.CreateModelList(b => b.ISBN.CompareTo(ISBN) == 0, 0, 1);

                result = books.Any();
            }

            return result;
        }

        // id == ISBN
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var model = new BookViewModel();
            var bookBuilder = new BookModelBuilder();
            var modelList = bookBuilder.CreateModelList(b => b.ISBN.StartsWith(id), 0, 1);

            if (modelList.Any())
            {
                model = modelList.FirstOrDefault();
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var bookCommand = new BookCommand();
                bool result = await bookCommand.ExecuteAsync(model);
                if (!result)
                {
                    ModelState.AddModelError("", "");
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // id == ISBN
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            var model = new BookViewModel();
            var bookBuilder = new BookModelBuilder();
            var modelList = bookBuilder.CreateModelList(b => b.ISBN.StartsWith(id), 0, 1);

            if (modelList.Any())
            {
                model = modelList.FirstOrDefault();
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var bookCommand = new BookCommand();
                bool result = await bookCommand.RemoveAsync(id);
                if (!result)
                {
                    ModelState.AddModelError("", "");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}