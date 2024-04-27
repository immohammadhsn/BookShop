using BookShop.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using static BookShop.Shared.ServiceResponses;

namespace BookShop.Client.Services;
public class AccountService(HttpClient _httpClient, AuthenticationStateProvider _authenticationStateProvider) : IAccountService
{
    CustomAuthenticationStateProvider customAuthenticationStateProvider = (CustomAuthenticationStateProvider)_authenticationStateProvider;

    public async Task<GeneralResponse> CreateAccount(RegisterDTO userDTO)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("api/Account/Register", userDTO);
        GeneralResponse? result;

        result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

        return result!;
    }

    public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("api/Account/Login", loginDTO);
        LoginResponse? result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result!.Succeeded)
                await customAuthenticationStateProvider.UpdateAuthenticationState(result.Token);

        return result!;
    }

    public async Task LogOut() => await customAuthenticationStateProvider.UpdateAuthenticationState();

    public async Task<GeneralResponse> UpdateUserData(RegisterDTO updatedUser)
    {
        HttpResponseMessage? response = await _httpClient.PostAsJsonAsync("api/Account/EditUser", updatedUser);
        GeneralResponse? result = await response.Content.ReadFromJsonAsync<GeneralResponse>();

        return result!;
    }
}
