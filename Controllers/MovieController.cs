using Microsoft.AspNetCore.Mvc;
using MoviesRental.IRepository;
using MoviesRental.Models;

namespace MoviesRental.Controllers;

public class MovieController : Controller
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var movies = await _movieRepository.GetAll();
            return View(movies);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }        
    }

    public IActionResult Add()
    {
        return View("AddUpdate");
    }

    [HttpPost]
    public async Task<IActionResult> Add(Movie movie)
    {
        try
        {
            if(!ModelState.IsValid)
            {
                return View(movie);
            }

            var movieExists = await _movieRepository.CheckIfExists(movie.Name);
            if (movieExists)
            {
                ModelState.AddModelError("Name", "Movie name already exists");
            }

            movie.DateAdded = DateTime.UtcNow;
            await _movieRepository.Add(movie);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }

    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return NotFound();

            TempData["DateAdded"] = movie.DateAdded;
            return View("AddUpdate", movie);
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(Movie movie)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            var movieExists = await _movieRepository.CheckIfExists(movie.Name);
            if (movieExists)
            {
                ModelState.AddModelError("Name", "Movie name already exists");
            }

            movie.DateAdded = TempData["DateAdded"] as DateTime? ?? default(DateTime);
            await _movieRepository.Update(movie);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null) return NotFound();

            await _movieRepository.Delete(movie);

            return NoContent();
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            return View("Error");
        }
    }
}