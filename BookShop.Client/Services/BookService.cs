using BookShop.Client.Pages.Books;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BookShop.Client.Services
{
    public class BookService(HttpClient _httpClient)
    {
        public async Task<HttpResponseMessage> Add(BookDTO book) => await _httpClient.PostAsJsonAsync("/api/Book", book);

        public async Task<List<Book>?> GetAll() => await _httpClient.GetFromJsonAsync<List<Book>>("api/Book/GetAll");
        public async Task<Book?> GetById(Guid id) => await _httpClient.GetFromJsonAsync<Book>($"api/Book/WithIncludes/{id}?Includes={nameof(Book.Author)}");
        public async Task<HttpResponseMessage?> Delete(Guid id) => await _httpClient.DeleteAsync($"api/Book/{id}");
        public async Task<BookDTO?> GetDtoById(Guid id) => await _httpClient.GetFromJsonAsync<BookDTO>($"api/Book/{id}");
        public async Task<List<Book>?> Find(string key, string value) => await _httpClient.GetFromJsonAsync<List<Book>?>($"api/Book/Find?Key={key}&Value={value}");
        public async Task<HttpResponseMessage> Edit(Guid bookId, BookDTO editedBook) => await _httpClient.PutAsJsonAsync($"/api/Book/{bookId}", editedBook);

        public async Task<HttpResponseMessage> AddSoldBook(BookInCart soldBook)
        {
            var jsonBook = JsonSerializer.Serialize(soldBook);
            BookDTO? book = JsonSerializer.Deserialize<BookDTO>(jsonBook);

            if (book is null) return new() { StatusCode = System.Net.HttpStatusCode.NotFound };

            book.AvailableQuantity -= (soldBook.SoldQuantity + soldBook.BorrowedQuantity);
            var response = await Edit(soldBook.Id, book);

            if (response.IsSuccessStatusCode)
            {
                SoldBookDTO sold = new() { BookId = soldBook.Id, BookStatus = soldBook.BookStatus, Date = DateTime.Now, Quantity = soldBook.SoldQuantity + soldBook.BorrowedQuantity, Profit = soldBook.TotalPrice };
                response = await _httpClient.PostAsJsonAsync("/api/SoldBook", sold);
            }

            return response;
        }

        public async Task<List<SoldBook>?> GetSoldBooks() => await _httpClient.GetFromJsonAsync<List<SoldBook>>($"api/SoldBook/GetAllWithIncludes?includes={nameof(SoldBook.Book)}");
    }

}
