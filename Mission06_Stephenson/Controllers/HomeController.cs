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
            return View("Movie");
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
            var movie = _context.Movies
                .Include(m => m.Category)  // Include the related Category
                .FirstOrDefault(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            // Pass all categories to the view for the dropdown
            ViewBag.Categories = _context.Categories.ToList();

            return View(movie);
        }
        
        [HttpPost]
        public IActionResult EditMovie(Movie movie)
        {
            // Convert StringValues to a regular string using camelCase
            var categoryName = Request.Form["CategoryName"].ToString();

            var existingMovie = _context.Movies
                .FirstOrDefault(m => m.MovieId == movie.MovieId);

            if (existingMovie == null)
            {
                return NotFound();
            }

            // Find or create the category by name
            var category = _context.Categories.FirstOrDefault(c => c.CategoryName == categoryName);
            if (category == null)
            {
                category = new Category { CategoryName = categoryName };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            // Update movie properties
            existingMovie.Title = movie.Title;
            existingMovie.Director = movie.Director;
            existingMovie.Year = movie.Year;
            existingMovie.Rating = movie.Rating;
            existingMovie.Edited = movie.Edited;
            existingMovie.CopiedToPlex = movie.CopiedToPlex;
            existingMovie.Notes = movie.Notes;
            existingMovie.CategoryId = category.CategoryId;

            // Save changes to the database
            _context.SaveChanges();

            // Redirect after saving
            return RedirectToAction("MovieCollection", "Home");
        }
        
        [HttpPost]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            // Redirect back to the Movie Collection after deletion
            return RedirectToAction("MovieCollection", "Home");
        }
        
    }
}