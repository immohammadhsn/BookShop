using BookShop.Shared;
using Microsoft.AspNetCore.Components;
using static BookShop.Client.StatusMessage;

namespace BookShop.Client
{
    public partial class Register
    {
        [SupplyParameterFromForm]
        public RegisterDTO RegisteredUser { get; set; } = new();

        [Inject]
        public StatusMessage _RegisterMessage { get; set; }

        public async Task RegisterUser()
        {
            var response = await AccountService.CreateAccount(RegisteredUser);

            if (response.Succeeded)
                await _RegisterMessage.Info(response.Message);
            else
                await _RegisterMessage.Error(response.Message);
            StateHasChanged();

            if(response.Succeeded)
                NavManager.NavigateTo($"Account/Login?returnUrl={Uri.EscapeDataString(NavManager.Uri)}");
        }
    }
}