using BookShop.Client.Services;
using BookShop.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using static BookShop.Client.StatusMessage;

namespace BookShop.Client;

public partial class AddBooks
{
    private MessageType messageType;
    private string? message;

    private DateTime dateTime = DateTime.Now;

    [SupplyParameterFromForm]
    public BookDTO AddedBook { get; set; } = new();

    private List<Author>? authors;
    IBrowserFile? file;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        authors = await _authorService.GetAll();
        StateHasChanged();
        await base.OnAfterRenderAsync(firstRender);
    }
    public async Task AddBook()
    {
        await UploadBookPhoto();
        var response = await _bookService.Add(AddedBook);

        dateTime = DateTime.Now;

        StateHasChanged();

        if (response.IsSuccessStatusCode)
        {
            messageType = MessageType.Success;
            message = "Book added Successfully";
            NavManager.NavigateTo($"/");
        }
        else
        {
            messageType = MessageType.Error;
            message = "Error while adding book";
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