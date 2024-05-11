using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BookShop.Client;

public partial class AddBooks
{
    [Parameter] public string? BookId { get; set; }

    private StatusMessage addStatusMessage = new();

    [SupplyParameterFromForm]
    public BookDTO AddedBook { get; set; } = new();

    private List<Author>? authors;
    IBrowserFile? file;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (BookId is not null)
            {
                AddedBook = await _bookService.GetDtoById(Guid.Parse(BookId)) ?? new();
            }
            authors = await _authorService.GetAll();
            StateHasChanged();
        }
        await base.OnAfterRenderAsync(firstRender);
    }
    public async Task AddBook()
    {
        await UploadBookPhoto();
        if (BookId is null)
        {

            var response = await _bookService.Add(AddedBook);

            StateHasChanged();

            if (response.IsSuccessStatusCode)
            {
                await addStatusMessage.Info("Book added Successfully");
                NavManager.NavigateTo($"/");
            }
            else
            {
                await addStatusMessage.Info("Error while Adding Book");
            }
        }
        else
        {
            var response = await _bookService.Edit(Guid.Parse(BookId), AddedBook);

            StateHasChanged();

            if (response.IsSuccessStatusCode)
            {
                await addStatusMessage.Info("Book edited Successfully");
                NavManager.NavigateTo($"/");
            }
            else
            {
                await addStatusMessage.Info("Error while editing Book");
            }
        }

    }

    private async Task HandleFileChange(InputFileChangeEventArgs e)
    {
        file = e.File;
    }

    async Task UploadBookPhoto()
    {
        if (file != null)
        {

            if (file == null || file.Size == 0)
                throw new ArgumentNullException("File is empty");


            var filePath = await __fileUploader.UploadFile(file);

            AddedBook.Img = filePath;
        }
    }
}