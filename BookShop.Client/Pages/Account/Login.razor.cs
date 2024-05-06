using BookShop.Client.Services;
using BookShop.Shared;
using Microsoft.AspNetCore.Components;
using static BookShop.Client.StatusMessage;

namespace BookShop.Client
{
    public partial class Login
    {

        private string? message;

        [SupplyParameterFromForm]
        private LoginDTO LoggedUser { get; set; } = new();

        private MessageType messageType;

        private DateTime dateTime;
        CustomAuthenticationStateProvider? customAuthenticationStateProvider;

        public async Task LoginUser()
        {

            var response = await AccountService.LoginAccount(LoggedUser);
            message = response.Message;
            messageType = response.Succeeded ? MessageType.Success : MessageType.Error;
            dateTime = DateTime.Now;
            StateHasChanged();

            if (response.Succeeded)
            {
                StateHasChanged();

                NavManager.NavigateTo("/", true);
            }
        }
    }
}