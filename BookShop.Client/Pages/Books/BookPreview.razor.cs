using Blazored.LocalStorage;
using BookShop.Shared;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace BookShop.Client.Pages.Books;

public partial class BookPreview
{
    [Inject]
    public ILocalStorageService localStorage { get; set; }
    [Parameter]
    public string BookId { get; set; }

    BookInCart? BookInCart;

    private bool isShowBuyForm = false;
    private bool isShowBorrowForm = false;
    private int quantity;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var book = await _bookService.GetById(Guid.Parse(BookId));
            string serBook = JsonSerializer.Serialize(book);
            BookInCart= JsonSerializer.Deserialize<BookInCart>(serBook);

            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void ShowBuyForm() { isShowBuyForm = true; isShowBorrowForm = false; }
    private void ShowBorrowForm() { isShowBuyForm = false; isShowBorrowForm = true; }

    private async void AddToCart()
    {
        if (isShowBuyForm && BookInCart?.AvailableQuantity >= quantity)
        {
            BookInCart.BookStatus = BookStatus.Buyed;
            await SaveIntoLocal(BookInCart);
        }
        else if (isShowBorrowForm && BookInCart?.AvailableQuantity >= quantity + 5)
        {
            BookInCart.BookStatus = BookStatus.Borrowed;
            await SaveIntoLocal(BookInCart);
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
    public double BorrowingPrice { get { return Price / 2; } set { } }
}

public enum BookStatus { Borrowed, Buyed }