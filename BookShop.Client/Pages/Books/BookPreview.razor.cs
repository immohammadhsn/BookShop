using Blazored.LocalStorage;
using BookShop.Client.Services;
using BookShop.Shared;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BookShop.Client.Pages.Books;

public partial class BookPreview
{
    [Inject]
    public ILocalStorageService localStorage { get; set; }

    [Inject]
    public StatusMessage _StatusMessage { get; set; }

    [Parameter]
    public string BookId { get; set; }

    BookInCart? BookInCart;

    private bool isShowBuyForm = false;
    private bool isShowBorrowForm = false;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var book = await _bookService.GetById(Guid.Parse(BookId));
            string serBook = JsonSerializer.Serialize(book);
            BookInCart = JsonSerializer.Deserialize<BookInCart>(serBook);

            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void ShowBuyForm() { isShowBuyForm = true; isShowBorrowForm = false; }
    private void ShowBorrowForm() { isShowBuyForm = false; isShowBorrowForm = true; }

    private async void AddToCart()
    {
        if (isShowBuyForm)
        {
            if (BookInCart?.AvailableQuantity >= BookInCart.Quantity)
            {
                BookInCart.BookStatus = BookStatus.Buyed;
                await SaveIntoLocal(BookInCart);

                NavManager.NavigateTo("/Cart");
            }
            else
            {
                await _StatusMessage.Warning($"Only {BookInCart.AvailableQuantity} copies available to buy");
            }
        }
        else if (isShowBorrowForm)
        {
            if (BookInCart?.AvailableQuantity >= BookInCart.Quantity + 5)
            {

                BookInCart.BookStatus = BookStatus.Borrowed;
                await SaveIntoLocal(BookInCart);

                NavManager.NavigateTo("/Cart");
            }

            else
                await _StatusMessage.Warning($"Only {BookInCart.AvailableQuantity-5} copies available to borrow");
        }
    }

    private async Task SaveIntoLocal(BookInCart bookToSave)
    {
        List<BookInCart> exestingBooks = new();
        var jsonBooks = await localStorage.GetItemAsStringAsync(ConstSettings.LocalStoredBooks) ?? string.Empty;

        if (!string.IsNullOrEmpty(jsonBooks))
            exestingBooks = JsonSerializer.Deserialize<List<BookInCart>>(jsonBooks) ?? new();

        exestingBooks.Add(bookToSave);

        var serializedBooks = JsonSerializer.Serialize(exestingBooks);
        await localStorage.SetItemAsStringAsync("BooksInCart", serializedBooks);
    }

}

public class BookInCart : Book
{
    public BookStatus BookStatus { get; set; }
    public int BorrowingPeriod { get; set; }
    public double BorrowingPrice { get { return Price*0.25; } set { } }
    public int Quantity { get; set; }
    public double TotalPrice {get => BookStatus.Equals(BookStatus.Buyed) ? (Price * Quantity) + 30 : (BorrowingPrice +BorrowingPeriod * 0.25); set { } }
}

public enum BookStatus { Borrowed, Buyed }