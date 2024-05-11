using Blazored.LocalStorage;
using BookShop.Shared;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace BookShop.Client.Pages.Books
{
    public partial class Cart
    {
        [Inject]
        public ILocalStorageService localStorage { get; set; }
        public List<BookInCart> BooksInCart = new();
        private double TotalOrderPrice;
        private bool isOrderSubmitted = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var JsonBooks = await localStorage.GetItemAsStringAsync(ConstSettings.LocalStoredBooks);

                if (string.IsNullOrEmpty(JsonBooks)) return;

                BooksInCart = JsonSerializer.Deserialize<List<BookInCart>>(JsonBooks) ?? new();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task DeleteBook(BookInCart deletedBook)
        {

            BooksInCart.Remove(deletedBook);
            StateHasChanged();

            var serialisedBook = JsonSerializer.Serialize(BooksInCart);
            await localStorage.SetItemAsStringAsync(ConstSettings.LocalStoredBooks, serialisedBook);
        }

        private double CalculateTotalPrice() => TotalOrderPrice = BooksInCart.Sum(e => e.TotalPrice);

    }

}

