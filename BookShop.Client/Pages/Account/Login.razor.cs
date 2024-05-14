using BookShop.Client.Services;
using BookShop.Shared;
using Microsoft.AspNetCore.Components;
using static BookShop.Client.StatusMessage;

namespace BookShop.Client
{
    public partial class Login
    {
        [Inject]
        public StatusMessage _LoginMessage { get; set; }

        [SupplyParameterFromForm]
        private LoginDTO LoggedUser { get; set; } = new();

        CustomAuthenticationStateProvider? customAuthenticationStateProvider;

        public async Task LoginUser()
        {

            var response = await AccountService.LoginAccount(LoggedUser);

            if (response.Succeeded)
                await _LoginMessage.Info(response.Message);
            else
                await _LoginMessage.Error(response.Message);
            StateHasChanged();

            if (response.Succeeded)
            {
                StateHasChanged();

                NavManager.NavigateTo("/", false);
            }
        }
    }
}