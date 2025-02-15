using Microsoft.AspNetCore.Mvc;
using Mission06_Stephenson.Models;
using System.Linq;

namespace Mission06_Stephenson.Controllers
{
    public class HomeController : Controller
    {
        private readonly MoviesContext _context;

        public HomeController(MoviesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult MovieCollection()
        {
            var movies = _context.Movies.ToList(); // Get all movies from the database
            return View(movies);
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(movie); // Add the new movie to the database
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("MovieCollection"); // Redirect to the movie list
            }
            return View(movie); // Show the form again if input is invalid
        }
    }
}