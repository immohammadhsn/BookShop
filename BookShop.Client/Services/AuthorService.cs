using BookShop.Shared;
using BookShop.Shared.Entities;
using System.Net.Http.Json;

namespace BookShop.Client.Services
{
    public class AuthorService(HttpClient _httpClient)
    {
        public async Task Add(AuthorDTO author)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Author", author);
        }

        public async Task<List<Author>?> GetAll() => await _httpClient.GetFromJsonAsync<List<Author>>("api/Author/GetAll");
        public async Task<Author?> GetById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Author>($"api/Author/{id}");
        }
    }
}
