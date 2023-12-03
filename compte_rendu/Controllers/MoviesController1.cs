using System;
using compte_rendu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using compte_rendu.Models;
using compte_rendu.Services;
using Compte_rendu.Services.ServicesContracts;

namespace TP3.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMoviesService _moviesService;

        public MoviesController(ApplicationDbContext db, IMoviesService _movieService)
        {
            _db = db;
            _moviesService = _movieService;

        }
        public IActionResult Index()
        {

            return View(_moviesService.GetAllMovies());
        }

        public IActionResult Create()
        {
            ViewData["Genres"] = new SelectList(_db.Set<Genre>(), "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMovie(Movie movie)
        {

            if (ModelState.IsValid)
            {
                _moviesService.CreateMovie(movie);
                return RedirectToAction("Index");
            }
            ViewData["Genres"] = new SelectList(_db.Set<Genre>(), "Id", "Id");
            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View("Create", movie);
        }

        public IActionResult Edit(int id)
        {
            var movie = _moviesService.GetMovieById(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["Genres"] = new SelectList(_db.Set<Genre>(), "Id", "Id");



            return View(movie);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                if (movie.ImageFile != null && movie.ImageFile.Length > 0)
                {
                    if (!string.IsNullOrEmpty(movie.Photo))
                    {
                        var filename = Path.Combine("wwwroot", movie.Photo.TrimStart('/'));

                        if (System.IO.File.Exists(filename))
                        {
                            System.IO.File.Delete(filename);
                        }
                    }


                    var imagePath = Path.Combine("wwwroot/images", movie.ImageFile.FileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        movie.ImageFile.CopyTo(stream);
                    }

                    movie.Photo = $"/images/{movie.ImageFile.FileName}";
                }
                _moviesService.Edit(movie);
                return RedirectToAction("Index");
            }
            ViewData["Genres"] = new SelectList(_db.Set<Genre>(), "Id", "Id");
            return View(movie);
        }

        public IActionResult Delete(int id)
        {
            var movie = _moviesService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _db.Movie.Find(id);


            if (movie == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(movie.Photo))
            {
                var imagePath = Path.Combine("wwwroot", movie.Photo.TrimStart('/'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }


            _moviesService.Delete(id);

            return RedirectToAction("Index");
        }


        public IActionResult Details(int? id)
        {
            if (id == null) return Content("unable to find Id");
            var c = _moviesService.GetMovieById(id.Value);
            return View(c);
        }


        public IActionResult MoviesByGenre(int id)
        {
            var movies = _moviesService.GetMoviesByGenre(id);
            return View("Index", movies);
        }


        public IActionResult MoviesOrderedAscending()
        {
            var movies = _moviesService.GetAllMoviesOrderedAscending();
            return View("Index", movies);
        }

        public IActionResult MoviesByUserDefinedGenre(string name)
        {
            var movies = _moviesService.GetMoviesByUserDefinedGenre(name);
            return View("Index", movies);
        }
    }
}