using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class JokeController : Controller
    {
        private readonly IJokeService _jokeService;

        public JokeController(IJokeService jokeService)
        {
            _jokeService = jokeService;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _jokeService.GetJokeTypesAsync();
            ViewBag.JokeTypes = types;
            return View();
        }

        public async Task<IActionResult> Random()
        {
            var joke = await _jokeService.GetRandomJokeAsync();
            if (joke == null)
            {
                ViewBag.Error = "Impossible de récupérer une blague pour le moment.";
                return View("Error");
            }
            return View(joke);
        }

        [HttpGet]
        public async Task<IActionResult> ByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return await Task.FromResult(RedirectToAction("Index"));
            }

            var joke = await _jokeService.GetJokeByTypeAsync(type);
            if (joke == null)
            {
                ViewBag.Error = $"Impossible de récupérer une blague de type \'{type}\'";
                return View("Error");
            }

            ViewBag.JokeType = type;
            return View("Random", joke);
        }

        [HttpPost]
        public async Task<IActionResult> GetJokeByType(string selectedType)
        {
            if (string.IsNullOrEmpty(selectedType))
            {
                return await Task.FromResult(RedirectToAction("Index"));
            }

            return await Task.FromResult(RedirectToAction("ByType", new { type = selectedType }));
        }

        public async Task<IActionResult> Multiple(int count = 5)
        {
            if (count < 1 || count > 10)
            {
                count = 5;
            }

            var jokes = await _jokeService.GetMultipleRandomJokesAsync(count);
            ViewBag.Count = count;
            return View(jokes);
        }
    }
}