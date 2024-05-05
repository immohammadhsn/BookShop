using BookShop.Shared.Entities;
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
        async Task Search(string Key = nameof(Book.Title))
        {
            var books = await _bookService.Find(Key, searchValue);
            AvailableBooks = books;
            StateHasChanged();
        }


    }
}