using Blazored.LocalStorage;
using BookShop.Shared;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace BookShop.Client.Pages.Books;

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

    private async Task SubmitOrder()
    {
        HttpResponseMessage response = new();
        foreach (var book in BooksInCart)
        {

            response = await _bookService.AddSoldBook(book);
            if (!response.IsSuccessStatusCode)
                break;
        }
        if (response.IsSuccessStatusCode)
        {
            isOrderSubmitted = true;
            await _StatusMessage.Info("order submitted Successfully");
            await localStorage.RemoveItemAsync(ConstSettings.LocalStoredBooks);
        }
        else
            await _StatusMessage.Error("Somthing went wrong while submitting the order");
    }


}

