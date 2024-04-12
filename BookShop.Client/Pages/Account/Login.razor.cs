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

        public async Task LoginUser()
        {
            var customAuthenticationStateProvider = (CustomAuthenticationStateProvider)AuthenticationStateProvider;

            var response = await AccountService.LoginAccount(LoggedUser);
            message = response.Message;
            messageType = response.Succeeded ? MessageType.Success : MessageType.Error;
            dateTime = DateTime.Now;
            StateHasChanged();

            if (response.Succeeded)
            {
                await customAuthenticationStateProvider.UpdateAuthenticationState(response.Token);
                StateHasChanged();

                NavManager.NavigateTo("/", true);
            }
        }
    }
}