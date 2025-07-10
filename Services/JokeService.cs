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
        private readonly string _baseUrl = "https://official-joke-api.appspot.com";

        public JokeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Joke?> GetRandomJokeAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/random_joke");
                return JsonSerializer.Deserialize<Joke>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Joke?> GetJokeByTypeAsync(string type)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/jokes/{type}/random");
                var jokes = JsonSerializer.Deserialize<List<Joke>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return jokes?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<string>> GetJokeTypesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/types");
                return JsonSerializer.Deserialize<List<string>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<string>();
            }
            catch (Exception)
            {
                return new List<string> { "general", "programming", "knock-knock", "dad" };
            }
        }

        public async Task<List<Joke>> GetMultipleRandomJokesAsync(int count)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}/jokes/random/{count}");
                return JsonSerializer.Deserialize<List<Joke>>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Joke>();
            }
            catch (Exception)
            {
                return new List<Joke>();
            }
        }
    }
}