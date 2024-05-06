using BookShop.Shared.Entities;
using System.Net.Http.Json;

namespace BookShop.Client.Services
{
    public class BookService(HttpClient _httpClient)
    {
        public async Task<HttpResponseMessage> Add(BookDTO book) => await _httpClient.PostAsJsonAsync("/api/Book", book);

        public async Task<List<Book>?> GetAll() => await _httpClient.GetFromJsonAsync<List<Book>>("api/Book/GetAll");
        public async Task<Book?> GetById(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"api/Book/WithIncludes/{id}?Includes={nameof(Book.Author)}");
        }

        public async Task<List<Book>?> Find(string key, string value) => await _httpClient.GetFromJsonAsync<List<Book>?>($"api/Book/Find?Key={key}&Value={value}");
    }
}
