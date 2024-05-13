using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using static System.Reflection.Metadata.BlobBuilder;
namespace BookShop.Client.Pages
{
    public partial class Home
    {
        List<Book> AvailableBooks = new();
        string searchValue = "";
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                AvailableBooks = await _bookService.GetAll();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task Search(string value)
        {
            searchValue = value;
            await PerformSearch();
        }
        async Task PerformSearch( string Key = nameof(Book.Title))
        {
            var books = await _bookService.Find(Key, searchValue);
            AvailableBooks = books;
            StateHasChanged();
        }
        async Task DeleteBook(Guid bookId)
        {
            var response = await _bookService.Delete(bookId);
            if (response.IsSuccessStatusCode)
            {
                await _StatusMessage.Info("Book Deleted Successfully");
                AvailableBooks = await _bookService.GetAll();
                StateHasChanged();
            }
            else
                await _StatusMessage.Error("Error while Deleting book");
        }

    }
}