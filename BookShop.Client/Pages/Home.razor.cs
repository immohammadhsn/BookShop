using BookShop.Shared.Entities;
using static System.Reflection.Metadata.BlobBuilder;
namespace BookShop.Client.Pages
{
    public partial class Home
    {
        List<Book> AvailableBooks = new();
        string searchValue = "";

        private StatusMessage statusMessage = new();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                AvailableBooks = await _bookService.GetAll();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        async Task Search(string Key = nameof(Book.Title))
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
                await statusMessage.Info("Book Deleted Successfully");
                AvailableBooks = await _bookService.GetAll();
                StateHasChanged();
            }
            else
                await statusMessage.Error("Error while Deleting book");
        }

    }
}