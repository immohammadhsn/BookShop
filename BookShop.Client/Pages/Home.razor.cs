using BookShop.Shared.Entities;

namespace BookShop.Client.Pages
{
    public partial class Home
    {
        List<Book> AvailableBooks=new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            AvailableBooks = await _bookService.GetAll();
            StateHasChanged();
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}