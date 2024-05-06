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
    Book? Book = new();

    private bool isShowBuyForm = false;
    private bool isShowBorrowForm = false;
    private int quantity;


    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Book = await _bookService.GetById(Guid.Parse(BookId));
            StateHasChanged();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void ShowBuyForm() { isShowBuyForm = true; isShowBorrowForm = false; }
    private void ShowBorrowForm() { isShowBuyForm = false; isShowBorrowForm = true; }

    private async void AddToCart()
    {
        if (isShowBuyForm && Book?.AvailableQuantity >= quantity)
        {
            await SaveIntoLocal(Book);
        }
        else if (isShowBorrowForm && Book.AvailableQuantity >= quantity + 5)
        {
            await SaveIntoLocal(Book);
        }
    }

    private async Task SaveIntoLocal(Book bookToSave)
    {
        List<Book> exestingBooks=new();
        var jsonBooks = await localStorage.GetItemAsStringAsync(ConstSettings.LocalStoredBooks) ?? string.Empty;

        if (!string.IsNullOrEmpty(jsonBooks))
            exestingBooks = JsonSerializer.Deserialize<List<Book>>(jsonBooks) ?? new();

        exestingBooks.Add(bookToSave);

        var serializedBooks = JsonSerializer.Serialize(exestingBooks);
        await localStorage.SetItemAsStringAsync("BooksInCart", serializedBooks);
    }

}