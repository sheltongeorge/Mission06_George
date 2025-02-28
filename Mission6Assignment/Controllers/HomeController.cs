using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mission6Assignment.Models;
using static System.Net.Mime.MediaTypeNames;

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

        public IActionResult GetToKnowJoel() // returns page
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMovie() //returns page for the addmovies, 
        {
            ViewBag.Categories = _context.Categories //this passes the categories into the page so the dropdown works
                .OrderBy(x => x.CategoryName)
                .ToList();
            return View(new MovieApplication()); // Ensures Model is not null
        }

        [HttpPost]
        public IActionResult AddMovie(MovieApplication response) // sends the new form information to the database
        {
            if (ModelState.IsValid) // if everything is valid in the input
            {
                _context.Movies.Add(response); // these two lines bring in the response (an object from MovieApplication) to the DB
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Thank you for your submission."; // confirmation message at top of page
                return RedirectToAction("AddMovie");  //returns you to page so that the information is blank in the inputs
            }

            // Repopulate ViewBag if there’s an error
            ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
            return View(response); // if there is an error, it returns to the page with the current input still there.
        }


        public IActionResult TableView() // view with the table showing
        {
            ViewBag.Categories = _context.Categories // brings in the link between CategoryId and CategoryName to show category name
                .OrderBy(x => x.CategoryName)
                .ToList();
            var movielist = _context.Movies
                .Include(m => m.Category) // ✅ Ensures category data is loaded
                .OrderBy(x => x.Title)
                .ToList();
            return View(movielist); // returns the view with all the rows
        }

        [HttpGet]
        public IActionResult Edit(int id) // when you click edit in the table view, it passes the id of the row you selected
        {
            ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();
            var recordToEdit = _context.Movies.SingleOrDefault(x => x.MovieId == id); // variable with the changes to that row

            if (recordToEdit == null)
            {
                return NotFound(); // error handling
            }

            return View("AddMovie", recordToEdit); // returns the "addMovie" page, but with the row of data you selected based on the id
        }
        [HttpPost]
        public IActionResult Edit(MovieApplication updatedInfo) // inputs row in the db, updatedInfo holds the row object
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.OrderBy(x => x.CategoryName).ToList();  //brings in the categories
                return View("AddMovie", updatedInfo);
            }

            var existingRecord = _context.Movies.SingleOrDefault(x => x.MovieId == updatedInfo.MovieId);

            if (existingRecord != null)
            {
                existingRecord.Title = updatedInfo.Title;
                existingRecord.Year = updatedInfo.Year;
                existingRecord.Director = updatedInfo.Director;
                existingRecord.Rating = updatedInfo.Rating;
                existingRecord.Edited = updatedInfo.Edited;
                existingRecord.LentTo = updatedInfo.LentTo;
                existingRecord.CopiedToPlex = updatedInfo.CopiedToPlex;
                existingRecord.Notes = updatedInfo.Notes;
                existingRecord.CategoryId = updatedInfo.CategoryId; // Ensures this is updated in the database

                _context.SaveChanges();
            }

            return RedirectToAction("TableView"); // brings you back to the view of the table page
        }




        [HttpGet]
        public IActionResult Delete(int id) //deletes, takes in the id of the row you select
        {
            var recordToDelete = _context.Movies
                .Single(x => x.MovieId == id); // passes the information into the page of that row, to be used in displaying confirmation of action

            return View(recordToDelete); //brings you to delete.cshtml with that object row you selected
        } 
        [HttpPost]
        public IActionResult Delete(MovieApplication movie)
        {
            _context.Movies.Remove(movie); // deletes that row and saves it.
            _context.SaveChanges();

            return RedirectToAction("TableView"); // back to the view of the table.
        }
    }
}
// m