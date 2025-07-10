using System.Text.Json;
using WebPortal.Models;

namespace WebPortal.Services
{
    public interface IJokeService
    {
        Task<Joke?> GetRandomJokeAsync();
        Task<Joke?> GetJokeByTypeAsync(string type);
        Task<List<string>> GetJokeTypesAsync();
        Task<List<Joke>> GetMultipleRandomJokesAsync(int count);
    }

    public class JokeService : IJokeService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<JokeService> _logger;
        private readonly string _baseUrl = "https://official-joke-api.appspot.com";

        public JokeService(HttpClient httpClient, ILogger<JokeService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.Timeout = TimeSpan.FromSeconds(10); // Timeout de 10 secondes
        }

        public async Task<Joke?> GetRandomJokeAsync()
        {
            try
            {
                _logger.LogInformation("Récupération d'une blague aléatoire");
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/random_joke");

                var joke = JsonSerializer.Deserialize<Joke>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _logger.LogInformation("Blague aléatoire récupérée avec succès: ID {JokeId}", joke?.Id);
                return joke;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erreur HTTP lors de la récupération d'une blague aléatoire");
                return null;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout lors de la récupération d'une blague aléatoire");
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erreur de désérialisation JSON pour une blague aléatoire");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la récupération d'une blague aléatoire");
                return null;
            }
        }

        public async Task<Joke?> GetJokeByTypeAsync(string type)
        {
            try
            {
                _logger.LogInformation("Récupération d'une blague de type: {Type}", type);
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/jokes/{type}/random");

                var jokes = JsonSerializer.Deserialize<List<Joke>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var joke = jokes?.FirstOrDefault();
                _logger.LogInformation("Blague de type {Type} récupérée avec succès: ID {JokeId}", type, joke?.Id);
                return joke;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erreur HTTP lors de la récupération d'une blague de type: {Type}", type);
                return null;
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout lors de la récupération d'une blague de type: {Type}", type);
                return null;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erreur de désérialisation JSON pour une blague de type: {Type}", type);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la récupération d'une blague de type: {Type}", type);
                return null;
            }
        }

        public async Task<List<string>> GetJokeTypesAsync()
        {
            try
            {
                _logger.LogInformation("Récupération des types de blagues");
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/types");

                var types = JsonSerializer.Deserialize<List<string>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<string>();

                _logger.LogInformation("Types de blagues récupérés avec succès: {Count} types", types.Count);
                return types;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération des types de blagues, utilisation des types par défaut");
                return new List<string> { "general", "programming", "knock-knock", "dad" };
            }
        }

        public async Task<List<Joke>> GetMultipleRandomJokesAsync(int count)
        {
            try
            {
                _logger.LogInformation("Récupération de {Count} blagues aléatoires", count);
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/jokes/random/{count}");

                var jokes = JsonSerializer.Deserialize<List<Joke>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Joke>();

                _logger.LogInformation("{Count} blagues aléatoires récupérées avec succès", jokes.Count);
                return jokes;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erreur HTTP lors de la récupération de {Count} blagues", count);
                return new List<Joke>();
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Timeout lors de la récupération de {Count} blagues", count);
                return new List<Joke>();
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erreur de désérialisation JSON pour {Count} blagues", count);
                return new List<Joke>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la récupération de {Count} blagues", count);
                return new List<Joke>();
            }
        }
    }
}