using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission6Assignment.Models;

namespace Mission6Assignment.Controllers
{
    public class HomeController : Controller
    {
        private MovieInputContext _context;

        public HomeController(MovieInputContext temp) //for connecting to the DB
        {
            _context = temp;
        }

        public IActionResult Index() // these three are all needed to pull up the initial webpage.
        {
            return View();
        }

        public IActionResult GetToKnowJoel()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(MovieApplication response)
        {
            if (ModelState.IsValid)
            {
                _context.MovieList.Add(response); // this is adding the record to the DB
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Thank you for your submission.";

                return RedirectToAction("AddMovie");
            }
            return View(response);
        }

    }
}
