using Library.Models;
using Library.Models.DbLayer;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        DBcontext db;

        public HomeController()
        {
            db = new DBcontext();
        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "Author desc" : "Author";
            ViewBag.YearSortParm = sortOrder == "Year" ? "Year desc" : "Year";
            ViewBag.ISBNSortParm = sortOrder == "ISBN" ? "ISBN desc" : "ISBN";
            ViewBag.PublisherSortParm = sortOrder == "Publisher" ? "Publisher desc" : "Publisher";
            ViewBag.CountrySortParm = sortOrder == "Country" ? "Country desc" : "Country";

            if (Request.HttpMethod == "GET")
            {
                searchString = currentFilter;
            }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var books = from b in db.Books
                           select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Book_Name.ToUpper().Contains(searchString.ToUpper())
                                                || b.Author.ToUpper().Contains(searchString.ToUpper())
                                                || b.Country.ToUpper().Contains(searchString.ToUpper())
                                                || b.Publisher.ToUpper().Contains(searchString.ToUpper())
                                                || b.ISBN.ToString().Contains(searchString)
                                                || b.Year.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "Name desc":
                    books = books.OrderByDescending(n => n.Book_Name);
                    break;
                case "Author":
                    books = books.OrderBy(a => a.Author);
                    break;
                case "Author desc":
                    books = books.OrderByDescending(a => a.Author);
                    break;
                case "ISBN":
                    books = books.OrderBy(i => i.ISBN);
                    break;
                case "ISBN desc":
                    books = books.OrderByDescending(i => i.ISBN);
                    break;
                case "Publisher":
                    books = books.OrderBy(p => p.Publisher);
                    break;
                case "Publisher desc":
                    books = books.OrderByDescending(p => p.Publisher);
                    break;
                case "Country":
                    books = books.OrderBy(c => c.Country);
                    break;
                case "Country desc":
                    books = books.OrderByDescending(c => c.Country);
                    break;
                case "Year":
                    books = books.OrderBy(y => y.Year);
                    break;
                case "Year desc":
                    books = books.OrderByDescending(y => y.Year);
                    break;
                default:
                    books = books.OrderBy(b => b.Book_Name);
                    break;
            }

            int pageSize = 5;
            int pageIndex = (page ?? 1);
            return View(books.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult Edit(int id = 0)
        {
            var model = id == 0 ? new Book() : db.Books.Find(id);
            ViewModelYears years = new ViewModelYears();
            ViewModelCountries countries = new ViewModelCountries();
            ViewBag.YearsList = new SelectList(years.ListYears, "Id", "Year", model.Year);
            ViewBag.CountriesList = new SelectList(countries.ListCountries, "Id", "Country", model.Country);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Book obj)
        {
            ViewModelYears model = new ViewModelYears();
            if (ModelState.IsValid)
            {
                db.Books.AddOrUpdate(obj);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = obj.Book_Id });
            }
            return View(obj);
        }

        public ActionResult Delete(int id)
        {
            db.Books.Remove(db.Books.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}