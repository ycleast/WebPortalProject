using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class JokeController : Controller
    {
        private readonly IJokeService _jokeService;
        private readonly ILogger<JokeController> _logger;

        public JokeController(IJokeService jokeService, ILogger<JokeController> logger)
        {
            _jokeService = jokeService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var types = await _jokeService.GetJokeTypesAsync();
                ViewBag.JokeTypes = types;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des types de blagues");
                ViewBag.Error = "Impossible de charger les types de blagues pour le moment.";
                return View("Error");
            }
        }

        public async Task<IActionResult> Random()
        {
            try
            {
                var joke = await _jokeService.GetRandomJokeAsync();
                if (joke == null)
                {
                    _logger.LogWarning("Aucune blague aléatoire retournée par l'API");
                    ViewBag.Error = "Impossible de récupérer une blague pour le moment. Veuillez réessayer.";
                    return View("Error");
                }
                return View(joke);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération d'une blague aléatoire");
                ViewBag.Error = "Une erreur s'est produite lors de la récupération de la blague.";
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ByType(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                return await Task.FromResult(RedirectToAction("Index"));
            }

            try
            {
                var joke = await _jokeService.GetJokeByTypeAsync(type);
                if (joke == null)
                {
                    _logger.LogWarning("Aucune blague retournée pour le type: {Type}", type);
                    ViewBag.Error = $"Impossible de récupérer une blague de type '{type}'. Ce type n'existe peut-être pas.";
                    return View("Error");
                }

                ViewBag.JokeType = type;
                return View("Random", joke);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération d'une blague de type: {Type}", type);
                ViewBag.Error = $"Une erreur s'est produite lors de la récupération d'une blague de type '{type}'.";
                return View("Error");
            }
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

            try
            {
                var jokes = await _jokeService.GetMultipleRandomJokesAsync(count);
                if (jokes == null || !jokes.Any())
                {
                    _logger.LogWarning("Aucune blague retournée pour Multiple avec count: {Count}", count);
                    ViewBag.Error = $"Impossible de récupérer {count} blagues pour le moment.";
                    return View("Error");
                }

                ViewBag.Count = count;
                return View(jokes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de {Count} blagues", count);
                ViewBag.Error = $"Une erreur s'est produite lors de la récupération de {count} blagues.";
                return View("Error");
            }
        }
    }
}