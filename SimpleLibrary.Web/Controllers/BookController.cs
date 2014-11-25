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

                var userName = User.Identity.Name;

                var userBuilder = new LibraryUserModelBuilder();
                var userModel = userBuilder.BuildModelFrom(userName);

                ViewBag.IsBookRentByMe = userModel.RentBooks != null && userModel.RentBooks.Where(r => r.ISBN.CompareTo(model.ISBN) == 0).Any();
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
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
        [Authorize]
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

        [Authorize]
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
        [Authorize]
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

        [Authorize]
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

        // id == ISBN
        [Authorize]
        public ActionResult Rent(string id)
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

        // id == ISBN
        [Authorize]
        [HttpPost, ActionName("Rent")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RentConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                var rentModel = new BookRentHistoryViewModel();
                rentModel.ISBN = id;
                rentModel.RentOn = DateTime.Now;
                rentModel.ReturnedOn = new DateTime(1900, 1, 1);
                rentModel.UserName = User.Identity.Name;

                var rentCommand = new RentCommand();
                bool result = await rentCommand.ExecuteAsync(rentModel);
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

        // id == ISBN
        [Authorize]
        public ActionResult Return(string id)
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

                var userName = User.Identity.Name;

                var userBuilder = new LibraryUserModelBuilder();
                var userModel = userBuilder.BuildModelFrom(userName);

                ViewBag.IsBookRentByMe = userModel.RentBooks != null && userModel.RentBooks.Where(r => r.ISBN.CompareTo(model.ISBN) == 0).Any();
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // id == ISBN
        [Authorize]
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReturnConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                var rentModel = new BookRentHistoryViewModel();
                rentModel.ISBN = id;
                //rentModel.RentOn = DateTime.Now;
                rentModel.ReturnedOn = DateTime.Now;
                rentModel.UserName = User.Identity.Name;

                var rentCommand = new RentCommand();
                bool result = await rentCommand.ExecuteAsync(rentModel);
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