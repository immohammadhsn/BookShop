using Blazored.LocalStorage;
using BookShop.Shared;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BookShop.Client.Pages.Books
{
    public partial class Cart
    {
        [Inject]
        public ILocalStorageService localStorage { get; set; }

        public List<Book> Books = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Books = JsonSerializer.Deserialize<List<Book>>(await localStorage.GetItemAsStringAsync(ConstSettings.LocalStoredBooks) ?? string.Empty) ?? new();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task DeleteBook(Book deletedBook)
        {

            Books.Remove(deletedBook);
            StateHasChanged();

            var serialisedBook = JsonSerializer.Serialize(Books);
            await localStorage.SetItemAsStringAsync(ConstSettings.LocalStoredBooks, serialisedBook);
        }
    }
}