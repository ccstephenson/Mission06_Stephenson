using Microsoft.AspNetCore.Mvc;
using Mission06_Stephenson.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            _context.Movies.Add(movie); // Add the new movie to the database
            _context.SaveChanges(); // Save changes to the database
            return RedirectToAction("Index");
        }

        public IActionResult MovieCollection()
        {
            var movies = _context.Movies.Include(m => m.Category).ToList();
            return View(movies);
        }

        public IActionResult EditMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);

        }
        [HttpPost]
        public IActionResult EditMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
            return RedirectToAction("MovieCollection");
        }
        
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        
        [HttpPost, ActionName("DeleteMovie")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction("MovieCollection");
        }

    }
}