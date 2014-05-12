using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieEntityApp.Models;

namespace MovieEntityApp.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        MoviesDBEntities _db;

        public HomeController()
        {
            _db = new MoviesDBEntities();
        }

        public ActionResult Index()
        {
            ViewData.Model = _db.MovieSet.ToList();
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(FormCollection form)
        {
            var movieToAdd = new Movie();

            TryUpdateModel(movieToAdd, new string[] { "Title", "Director" }, form.ToValueProvider());

            if (String.IsNullOrEmpty(movieToAdd.Title))
                ModelState.AddModelError("Title", "Title is required");

            if (String.IsNullOrEmpty(movieToAdd.Director))
                ModelState.AddModelError("Director", "Director is required");

            if (ModelState.IsValid)
            {
                _db.MovieSet.Add(movieToAdd);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(movieToAdd);
        }

    }
}
