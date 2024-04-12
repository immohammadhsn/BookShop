using BookShop.Shared;
using Microsoft.AspNetCore.Components;
using static BookShop.Client.StatusMessage;

namespace BookShop.Client
{
    public partial class Register
    {
        [SupplyParameterFromForm]
        public UserDTO RegesteredUser { get; set; } = new();

        private MessageType messageType;
        private string? message;

        private DateTime dateTime = DateTime.Now;

        public async Task RegisterUser()
        {
            var response = await AccountService.CreateAccount(RegesteredUser);
            message = response.Message;
            dateTime = DateTime.Now;
            StateHasChanged();
            messageType = response.Succeeded ? MessageType.Success : MessageType.Error;

            if(response.Succeeded)
                NavManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(NavManager.Uri)}", forceLoad: true);


        }
    }
}